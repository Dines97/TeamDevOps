
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreHealthChecker.RabbitMQ
{
  public class RabbitMQPlugin : IPlugin
  {
    private readonly List<IProbeBuilder> _supportedProbes = new List<IProbeBuilder>();

    public RabbitMQPlugin()
    {
      _supportedProbes.Add(new ProbeBuilder());

    }

    public List<IProbeBuilder> GetProbeTypes()
    {
        return _supportedProbes;
    }
  }
}
