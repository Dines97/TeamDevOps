using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreHealthChecker;

public interface IProbe
{
  Type ConfigType { get; }

  bool Check(string type);

  void Configure(IHealthChecksBuilder builder, Config.Probe probeConfigProperties);
}
