namespace AspNetCoreHealthChecker.Network;

public class HttpRequestPlugin : IPlugin
{
  private readonly List<IProbe> _supportedProbes = new() {new DnsResolveHandler(), new TcpHandler(), new SslHandler()};

  public IEnumerable<IProbe> GetProbeTypes()
  {
    return _supportedProbes;
  }
}
