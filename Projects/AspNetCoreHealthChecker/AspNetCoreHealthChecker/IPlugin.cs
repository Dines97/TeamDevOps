using AspNetCoreHealthChecker.Config;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreHealthChecker
{
  public interface IPlugin
  {
    bool Check(string text);

    void Run(IHealthChecksBuilder healthChecksBuilder, Probe probe);
  }
}