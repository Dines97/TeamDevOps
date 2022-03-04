using AspNetCoreHealthChecker.Config;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreHealthChecker.Network;

public class SslProbe : IProbe
{
  public bool Check(string name)
  {
    return String.Compare(name, "ssl", StringComparison.OrdinalIgnoreCase) == 0;
  }

  public void Configure(IHealthChecksBuilder builder, Probe probeConfigProperties)
  {
    
    var host = probeConfigProperties.Properties["Host"] as string;
    var port = int.Parse((string)probeConfigProperties.Properties["Port"]);
    var expiration = int.Parse((string)probeConfigProperties.Properties["Expiration"]);


    builder.AddSslHealthCheck(s =>
        s.AddHost(host: host, port: port, checkLeftDays: expiration),
      name: probeConfigProperties.Name,
      timeout: TimeSpan.FromDays(expiration)
    );
  }
}
