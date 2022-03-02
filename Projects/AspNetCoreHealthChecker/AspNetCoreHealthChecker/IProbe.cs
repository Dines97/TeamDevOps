using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreHealthChecker;

public interface IProbe
{
  bool Check(string name);

  void Configure(IHealthChecksBuilder builder, Config.Probe probeConfigProperties);
}
