namespace AspNetCoreHealthChecker.RabbitMQ
{
  public class RabbitMqPlugin : IPlugin
  {
    private readonly List<IProbe> _supportedProbes = new List<IProbe>() {new Probe()};

    public IEnumerable<IProbe> GetProbeTypes()
    {
      return _supportedProbes;
    }
  }
}
