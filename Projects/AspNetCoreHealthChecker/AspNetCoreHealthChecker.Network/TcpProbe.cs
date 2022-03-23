using AspNetCoreHealthChecker.Config;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreHealthChecker.Network;

public class TcpProbe : IProbe
{
  public Type ConfigType { get; }

  public bool Check(string name)
  {
    return String.Compare(name, "Tcp", StringComparison.OrdinalIgnoreCase) == 0;
  }

  public void Configure(IHealthChecksBuilder builder, Probe probeConfigProperties)
  {
    var host = probeConfigProperties.Properties["Host"] as string;
    var port = int.Parse((string)probeConfigProperties.Properties["Port"]);

    builder.AddTcpHealthCheck(s =>
        s.AddHost(host: host, port: port),
      name: probeConfigProperties.Name,
      timeout: TimeSpan.FromSeconds(probeConfigProperties.Timeout));
  }
}
