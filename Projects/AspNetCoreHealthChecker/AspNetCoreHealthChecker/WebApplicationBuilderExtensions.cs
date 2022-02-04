using AspNetCoreHealthChecker.Config;
using AspNetCoreHealthChecker.Plugins;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Reflection;

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

      List<IPlugin> plugins = new List<IPlugin>()
      {
        new HttpRequestPlugin()
      };


      foreach (var plugin in healthConfig.Plugins)
      {
        var a = Assembly.Load(plugin);
        if (a != null)
        {
          var classes = a.GetExportedTypes().Where(t => t.IsClass && !t.IsAbstract && t.GetInterfaces().Contains(typeof(IPlugin)));
          foreach (var t in classes)
          {
            var p = Activator.CreateInstance(t) as IPlugin;
            if (p != null) plugins.Add(p);
          }
        }
      }

      foreach (var probe in healthConfig.Probes)
      {
        var p = plugins.FirstOrDefault(x => x.Check(probe.Type));
        if (p == null) continue;
        p.Run(healthCheckBuilder, probe);
      }

      return builder;
    }
  }

  public static class WebApplicationExtensions
  {
    public static WebApplication UseAspNetHealthChecks(this WebApplication app)
    {
      var h = app.Services.GetService<IOptions<HealthCheck>>();

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

              var result = JsonConvert.SerializeObject(new
              {
                status = r.Status.ToString(),
                components = r.Entries.Select(e => new { key = e.Key, value = e.Value.Status.ToString() })
              });
              await c.Response.WriteAsync(result);
            }
          });
        }
      }

      return app;
    }
  }

}