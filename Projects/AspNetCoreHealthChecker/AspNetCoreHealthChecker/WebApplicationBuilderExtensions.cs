using System.Data;
using System.Reflection;
using AspNetCoreHealthChecker.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace AspNetCoreHealthChecker;

public static class WebApplicationBuilderExtensions
{
  public static WebApplicationBuilder ConfigureHealthCheck(this WebApplicationBuilder builder)
  {
    var healthSection = builder.Configuration.GetSection(nameof(HealthCheck));
    var probesSection = healthSection.GetSection("Probes");
    builder.Services.Configure<HealthCheck>(healthSection);
    var healthConfig = healthSection.Get<HealthCheck>();

    var healthCheckBuilder = builder.Services.AddHealthChecks();

    var plugins = new List<IPlugin>();
    var supportedProbes = new List<IProbe>();

    // Load plugins
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
          if (p != null)
          {
            plugins.Add(p);
          }
        }
      }
    }

    // Load supported probes from plugins
    foreach (var plugin in plugins)
    {
      supportedProbes.AddRange(plugin.GetProbeTypes());
    }

    var index = -1;

    // Iterate over probes and configure our probes
    foreach (var probe in healthConfig.Probes)
    {
      index++;
      var selectedSection = probesSection.GetSection(index.ToString());

      var find = false;
      foreach (var supportedProbe in supportedProbes)
      {
        if (supportedProbe.Check(probe.Type))
        {
          var pConfig = selectedSection.Get(supportedProbe.ConfigType) as Properties;

          find = true;

          supportedProbe.Configure(healthCheckBuilder, pConfig);
        }
      }

      if (!find && !healthConfig.IgnoreUnsupportedProbes)
      {
        throw new InvalidExpressionException("Unsupported probe type: " + probe.Type);
      }
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
              components = r.Entries.Select(e => new {key = e.Key, value = e.Value.Status.ToString()})
            });
            await c.Response.WriteAsync(result);
          }
        });
      }
    }

    return app;
  }
}
