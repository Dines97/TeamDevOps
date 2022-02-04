using AspNetCoreHealthChecker.Config;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreHealthChecker.Plugins
{
  public class HttpRequestPlugin : IPlugin
  {
    public bool Check(string text)
    {
      return string.Compare(text, "HttpRequest", true) == 0;
    }

    public void Run(IHealthChecksBuilder healthChecksBuilder, Probe probe)
    {
      healthChecksBuilder.AddUrlGroup(new Uri(probe.Properties["Path"].ToString()),
                                         probe.Name,
                                         timeout: TimeSpan.FromSeconds(probe.Timeout));
    }
  }
}