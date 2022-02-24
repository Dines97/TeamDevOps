using System.Net;

namespace AspNetCoreHealthChecker.Plugins.Http.DnsResolve;

public class DnsResolveProbe : IProbe
{
  private string Host { get; set; }

  private string TargetIp { get; set; }

  public DnsResolveProbe(IReadOnlyDictionary<string, object> probeConfigProperties)
  {
    this.Host = probeConfigProperties["Host"] as string;
    this.TargetIp = probeConfigProperties["TargetIP"] as string;
  }

  public ProbeResult Run()
  {
    IPHostEntry ipHostEntry = Dns.GetHostEntry(Host);

    if (ipHostEntry.AddressList.Any(ip => ip.ToString() == TargetIp))
    {
      return new ProbeResult(ProbeResultEnum.Success);
    }

    return new ProbeResult(ProbeResultEnum.Failure);
  }
}
