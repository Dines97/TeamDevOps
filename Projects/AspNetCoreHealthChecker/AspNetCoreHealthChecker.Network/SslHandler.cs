using AspNetCoreHealthChecker.Config;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreHealthChecker.Network;

public class SslHandler : IProbe
{
  public Type ConfigType => typeof(SslProperties);

  public bool Check(string name)
  {
    return String.Compare(name, "ssl", StringComparison.OrdinalIgnoreCase) == 0;
  }

  public void Configure(IHealthChecksBuilder builder, Properties properties)
  {
    var p = properties as SslProperties;

    builder.AddSslHealthCheck(s =>
        s.AddHost(p.Host, p.Port, p.Expiration),
      p.Name,
      timeout: TimeSpan.FromDays(p.Expiration)
    );
  }

  private class SslProperties : Properties
  {
    public string Host { get; set; }

    public ushort Port { get; set; }

    public int Expiration { get; set; }
  }
}
