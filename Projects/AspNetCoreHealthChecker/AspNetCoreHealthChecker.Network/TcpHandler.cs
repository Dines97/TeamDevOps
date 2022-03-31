using AspNetCoreHealthChecker.Config;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreHealthChecker.Network;

public class TcpHandler : IProbe
{
  public Type ConfigType => typeof(TcpProperties);

  public bool Check(string name)
  {
    return String.Compare(name, "Tcp", StringComparison.OrdinalIgnoreCase) == 0;
  }

  public void Configure(IHealthChecksBuilder builder, Properties properties)
  {
    var p = properties as TcpProperties;

    builder.AddTcpHealthCheck(s =>
        s.AddHost(p.Host, p.Port),
      p.Name,
      timeout: TimeSpan.FromSeconds(p.Timeout));
  }

  private class TcpProperties : Properties
  {
    public string Host { get; set; }

    public ushort Port { get; set; }

    public int Timeout { get; set; }
  }
}
