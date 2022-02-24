namespace AspNetCoreHealthChecker.RabbitMQ;

public class ProbeBuilder : IProbeBuilder
{
  public bool Check(string name)
  {
    return String.Compare(name, "RabbitMQ", StringComparison.OrdinalIgnoreCase) == 0;
  }

  public IProbe Build(IReadOnlyDictionary<string, object> probeConfigProperties)
  {
    return new Probe(probeConfigProperties);
  }
}
