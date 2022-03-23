using AspNetCoreHealthChecker.Config;
using HealthChecks.Network;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AspNetCoreHealthChecker.Network;

public class DnsResolveProbe : IProbe
{
  public Type ConfigType { get; }

  public bool Check(string name)
  {
    return String.Compare(name, "DnsResolve", StringComparison.OrdinalIgnoreCase) == 0;
  }

  public void Configure(IHealthChecksBuilder builder, Probe probeConfig)
  {
    builder.AddDnsResolveHealthCheck(setup =>
    {
      setup.ResolveHost(probeConfig.Properties["Host"] as string).To(probeConfig.Properties["TargetIP"] as string);
    }, name: probeConfig.Name);
    
  }
}
