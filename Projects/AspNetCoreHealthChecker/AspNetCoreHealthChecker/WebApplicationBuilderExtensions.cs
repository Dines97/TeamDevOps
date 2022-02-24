using System.Data;
using AspNetCoreHealthChecker.Config;
using AspNetCoreHealthChecker.Plugins;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Reflection;
using AspNetCoreHealthChecker.Plugins.Http;

namespace AspNetCoreHealthChecker
{
  public static class WebApplicationBuilderExtensions
  {
    public static WebApplicationBuilder ConfigureHealthCheck(this WebApplicationBuilder builder)
    {
      var healthSection = builder.Configuration.GetSection(nameof(HealthCheck));
      builder.Services.Configure<HealthCheck>(healthSection);
      var healthConfig = healthSection.Get<HealthCheck>();

      var healthCheckBuilder = builder.Services.AddHealthChecks();

      var plugins = new List<IPlugin>() {new HttpRequestPlugin()};
      var supportedProbes = new List<IProbeBuilder>();
      var probeDelegates = new List<ProbeDelegate>();

      foreach (var plugin in healthConfig.Plugins)
      {
        var a = Assembly.Load(plugin);
        if (a != null)
        {
          var classes = a.GetExportedTypes()
            .Where(t => t.IsClass && !t.IsAbstract && t.GetInterfaces().Contains(typeof(IPlugin)));
          foreach (var t in classes)
          {
            var p = Activator.CreateInstance(t) as IPlugin;
            if (p != null) plugins.Add(p);
          }
        }
      }

      foreach (var plugin in plugins)
      {
        supportedProbes.AddRange(plugin.GetProbeTypes());
      }

      foreach (var probe in healthConfig.Probes)
      {
        bool find = false;
        foreach (var supportedProbe in supportedProbes)
        {
          if (supportedProbe.Check(probe.Type))
          {
            find = true;

            var probeDelegate = new ProbeDelegate(supportedProbe, probe);
            probeDelegates.Add(probeDelegate);
          }
        }

        if (!find && !healthConfig.IgnoreUnsupportedProbes)
          throw new InvalidExpressionException("Unsupported probe type: " + probe.Type);
      }

      builder.Services.AddSingleton(new ProbeRunner(probeDelegates));

      return builder;
    }
  }

  public static class WebApplicationExtensions
  {
    public static WebApplication UseAspNetHealthChecks(this WebApplication app)
    {
      var h = app.Services.GetService<IOptions<HealthCheck>>();
      var probeRunner = app.Services.GetService<ProbeRunner>();

      foreach (var endpoint in h.Value.Endpoints)
      {
        if (endpoint.ResponseType == ResponseType.PlainText)
        {
          app.UseHealthChecks(endpoint.Uri);
        }
        else if (endpoint.ResponseType == ResponseType.Json)
        {
          app.UseHealthChecks(endpoint.Uri, new HealthCheckOptions
          {
            ResponseWriter = async (c, r) =>
            {
              c.Response.ContentType = "application/json";

              await c.Response.WriteAsync(probeRunner.RunJson());
            }
          });
        }
      }

      return app;
    }
  }
}
