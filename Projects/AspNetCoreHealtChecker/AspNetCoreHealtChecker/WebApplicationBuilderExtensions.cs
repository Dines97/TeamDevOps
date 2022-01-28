﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace AspNetCoreHealtChecker
{
  public static class WebApplicationBuilderExtensions
  {
    public static WebApplicationBuilder ConfigureHealthCheck(this WebApplicationBuilder builder)
    {
      var healthSection = builder.Configuration.GetSection(nameof(Config.HealthCheck));
      builder.Services.Configure<Config.HealthCheck>(healthSection);
      var healthConfig = healthSection.Get<Config.HealthCheck>();

      var healthCheckBuilder = builder.Services.AddHealthChecks();

      foreach (var probe in healthConfig.Probes)
      {

        if (probe.Type == "HttpRequest")
        {
          healthCheckBuilder.AddUrlGroup(new Uri(probe.Properties["Path"].ToString()),
                                         probe.Name,
                                         timeout: TimeSpan.FromSeconds(probe.Timeout));
        }
        else if (probe.Type == "RabbitMQ")
        {
          //
        }
      }

      return builder;
    }
  }

  public static class WebApplicationExtensions
  {
    public static WebApplication UseAspNetHealthChecks(this WebApplication app)
    {
      var h = app.Services.GetService<IOptions<Config.HealthCheck>>();

      foreach (var endpoint in h.Value.Endpoints)
      {
        if (endpoint.ResponseType == Config.ResponseType.PlainText)
        {
          app.UseHealthChecks(endpoint.Uri);
        }
        else if (endpoint.ResponseType == Config.ResponseType.Json)
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