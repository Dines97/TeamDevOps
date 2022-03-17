using AspNetCoreHealthChecker.Config;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace AspNetCoreHealthChecker.Network;

public class TcpProbe : IProbe
{
  class Properties
  {
    public string Host { get; set; }

    public ushort Port { get; set; }
  }


  public bool Check(string name)
  {
    return String.Compare(name, "Tcp", StringComparison.OrdinalIgnoreCase) == 0;
  }

  public void Configure(IHealthChecksBuilder builder, Probe probeConfigProperties)
  {
    var p = probeConfigProperties.Properties.ToObject<Properties>();

    
    builder.AddTcpHealthCheck(s =>
        s.AddHost(host: p.Host, port: p.Port),
      name: probeConfigProperties.Name,
      timeout: TimeSpan.FromSeconds(probeConfigProperties.Timeout));
  }
}
