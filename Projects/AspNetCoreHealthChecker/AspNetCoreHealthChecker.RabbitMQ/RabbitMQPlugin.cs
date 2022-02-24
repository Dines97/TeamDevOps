namespace AspNetCoreHealthChecker.RabbitMQ
{
  public class RabbitMqPlugin : IPlugin
  {
    private readonly List<IProbeBuilder> _supportedProbes = new List<IProbeBuilder>() {new ProbeBuilder()};

    public List<IProbeBuilder> GetProbeTypes()
    {
      return _supportedProbes;
    }
  }
}
