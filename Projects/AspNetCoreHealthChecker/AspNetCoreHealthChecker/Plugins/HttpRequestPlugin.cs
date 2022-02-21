using AspNetCoreHealthChecker.Config;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreHealthChecker.Plugins
{
  public class HttpRequestPlugin : IPlugin
  {
    
    private readonly List<IProbeBuilder> _supportedProbes = new List<IProbeBuilder>();
    
    
    public HttpRequestPlugin()
    {
      _supportedProbes.Add(new HttpProbeBuilder());
      
    }

    public List<IProbeBuilder> GetProbeTypes()
    {
      return _supportedProbes;
    }
  }
}
