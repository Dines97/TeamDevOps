using AspNetCoreHealthChecker.Config;
using HealthChecks.Network;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AspNetCoreHealthChecker.Network;

public class DnsResolveProbe : IProbe
{
  class Properties
  {
    public string Host { get; set; }
    public string TargetIP { get; set; }
  }


  public bool Check(string name)
  {
    return String.Compare(name, "DnsResolve", StringComparison.OrdinalIgnoreCase) == 0;
  }

  public void Configure(IHealthChecksBuilder builder, Probe probeConfig)
  {
    var p = probeConfig.Properties.ToObject<Properties>();

    builder.AddDnsResolveHealthCheck(setup =>
    {
      setup.ResolveHost(p.Host).To(p.TargetIP);
    }, name: probeConfig.Name);
  }
}
