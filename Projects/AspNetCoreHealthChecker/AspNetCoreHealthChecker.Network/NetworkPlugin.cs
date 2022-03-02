namespace AspNetCoreHealthChecker.Network
{
  public class HttpRequestPlugin : IPlugin
  {
    private readonly List<IProbe> _supportedProbes = new List<IProbe>()
    {
      new HttpProbe(), new DnsResolveProbe()
    };

    public IEnumerable<IProbe> GetProbeTypes()
    {
      return _supportedProbes;
    }
  }
}
