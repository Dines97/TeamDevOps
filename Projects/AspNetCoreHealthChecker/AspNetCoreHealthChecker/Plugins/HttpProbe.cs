using AspNetCoreHealthChecker.Config;

namespace AspNetCoreHealthChecker.Plugins;

public class HttpProbe : IProbe
{
  private string Name { get; set; }
  
  public HttpProbe(Config.Probe probe)
  {
    Name = probe.Name;
  }

  public ProbeResult Run()
  {
    //throw new NotImplementedException();
    return new ProbeResult(ProbeResultEnum.Success);
  }
  
}
public class HttpProbeBuilder : IProbeBuilder
{
  public bool Check(string name)
  {
    return String.Compare(name, "HttpRequest", StringComparison.OrdinalIgnoreCase) == 0;
  }

  public IProbe Build(Config.Probe probeConfig)
  {
    return new HttpProbe(probeConfig);
  }
}
