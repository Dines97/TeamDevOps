using AspNetCoreHealthChecker.Plugins.Http.DnsResolve;
using AspNetCoreHealthChecker.Plugins.Http.HttpProbe;

namespace AspNetCoreHealthChecker.Plugins.Http
{
  public class HttpRequestPlugin : IPlugin
  {
    private readonly List<IProbeBuilder> _supportedProbes = new List<IProbeBuilder>()
    {
      new HttpProbeBuilder(), new DnsResolveProbeBuilder()
    };

    public List<IProbeBuilder> GetProbeTypes()
    {
      return _supportedProbes;
    }
  }
}
