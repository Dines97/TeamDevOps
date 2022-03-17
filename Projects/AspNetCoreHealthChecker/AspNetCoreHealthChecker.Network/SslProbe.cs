using AspNetCoreHealthChecker.Config;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreHealthChecker.Network;

public class SslProbe : IProbe
{
  class Properties
  {
    public string Host { get; set; }
    public ushort Port { get; set; }
    public int Expiration { get; set; }
  }

  public bool Check(string name)
  {
    return String.Compare(name, "ssl", StringComparison.OrdinalIgnoreCase) == 0;
  }

  public void Configure(IHealthChecksBuilder builder, Probe probeConfigProperties)
  {
    var p = probeConfigProperties.Properties.ToObject<Properties>();

    builder.AddSslHealthCheck(s =>
        s.AddHost(host: p.Host, port: p.Port, checkLeftDays: p.Expiration),
      name: probeConfigProperties.Name);
  }
}
