namespace AspNetCoreHealthChecker.RabbitMQ;

public class RabbitMqPlugin : IPlugin
{
  private readonly List<IProbe> _supportedProbes = new() { new RabbitMqHandler() };

  public IEnumerable<IProbe> GetProbeTypes()
  {
    return _supportedProbes;
  }
}
