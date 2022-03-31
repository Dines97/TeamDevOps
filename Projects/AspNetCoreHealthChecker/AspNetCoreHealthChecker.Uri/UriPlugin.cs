namespace AspNetCoreHealthChecker.Uri;

public class UriPlugin : IPlugin
{
  public IEnumerable<IProbe> GetProbeTypes()
  {
    return new List<IProbe> {new UriHandler()};
  }
}
