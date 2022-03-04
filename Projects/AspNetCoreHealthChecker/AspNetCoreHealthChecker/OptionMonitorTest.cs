using AspNetCoreHealthChecker.Config;
using Microsoft.Extensions.Options;

namespace AspNetCoreHealthChecker;

public class OptionMonitorTest
{
  private readonly IOptionsMonitor<HealthCheck> _optionsMonitor;

  public OptionMonitorTest(IOptionsMonitor<HealthCheck> optionsMonitor)
  {
    _optionsMonitor = optionsMonitor;
  }
}
