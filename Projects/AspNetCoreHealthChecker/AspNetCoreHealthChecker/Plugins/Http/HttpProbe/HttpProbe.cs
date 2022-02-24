namespace AspNetCoreHealthChecker.Plugins.Http.HttpProbe;

public class HttpProbe : IProbe
{
  public HttpProbe(IReadOnlyDictionary<string, object> probeConfigProperties)
  {
  }

  public ProbeResult Run()
  {
    //throw new NotImplementedException();
    return new ProbeResult(ProbeResultEnum.Success);
  }
}
