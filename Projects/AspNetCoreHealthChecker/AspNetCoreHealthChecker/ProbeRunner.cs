namespace AspNetCoreHealthChecker;

public class ProbeRunner
{
  public void run(IProbe probe)
  {
  }

  public void run(IEnumerable<IProbe> probes)
  {
    foreach (var probe in probes)
    {
      Console.WriteLine(probe.Run().ResultEnum);
    }
  }
}
