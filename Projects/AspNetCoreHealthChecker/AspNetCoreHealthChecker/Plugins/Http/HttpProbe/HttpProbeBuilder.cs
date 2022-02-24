namespace AspNetCoreHealthChecker.Plugins.Http.HttpProbe;

public class HttpProbeBuilder : IProbeBuilder
{
  public bool Check(string name)
  {
    return String.Compare(name, "HttpRequest", StringComparison.OrdinalIgnoreCase) == 0;
  }

  public IProbe Build(IReadOnlyDictionary<string, object> probeConfigProperties)
  {
    return new HttpProbe(probeConfigProperties);
  }
}
