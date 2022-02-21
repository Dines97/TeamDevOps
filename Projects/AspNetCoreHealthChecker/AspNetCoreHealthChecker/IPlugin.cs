using AspNetCoreHealthChecker.Config;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreHealthChecker
{
  public interface IPlugin
  {
    List<IProbeBuilder> GetProbeTypes();
  }
}
