namespace AspNetCoreHealthChecker.Plugins.Http.DnsResolve;

public class DnsResolveProbeBuilder : IProbeBuilder
{
  public bool Check(string name)
  {
    return String.Compare(name, "DnsResolve", StringComparison.OrdinalIgnoreCase) == 0;
  }

  public IProbe Build(IReadOnlyDictionary<string, object> probeConfigProperties)
  {
    return new DnsResolveProbe(probeConfigProperties);
  }
}
