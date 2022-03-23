using AspNetCoreHealthChecker.Config;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreHealthChecker.Network;

public class HttpProbe : IProbe
{
  public Type ConfigType => typeof(HttpRequestProbe);

  public bool Check(string name)
  {
    return String.Compare(name, "HttpRequest", StringComparison.OrdinalIgnoreCase) == 0;
  }

  public void Configure(IHealthChecksBuilder builder, Probe probeConfigProperties)
  {
    
  }
}

class HttpRequestProbe : Config.Probe
{
  public string Path { get; set; }

  public int ReturnCode { get; set; }
}
