using AspNetCoreHealthChecker.Config;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreHealthChecker.Network;

public class DnsResolveHandler : IProbe
{
  public Type ConfigType => typeof(DnsResolveProperties);

  public bool Check(string name)
  {
    return String.Compare(name, "DnsResolve", StringComparison.OrdinalIgnoreCase) == 0;
  }

  public void Configure(IHealthChecksBuilder builder, Properties properties)
  {
    var p = properties as DnsResolveProperties;

    builder.AddDnsResolveHealthCheck(setup =>
    {
      setup.ResolveHost(p.Host).To(p.Registrations);
    }, p.Name);
  }

  private class DnsResolveProperties : Properties
  {
    public string Host { get; set; }

    public string[] Registrations { get; set; }
  }
}
