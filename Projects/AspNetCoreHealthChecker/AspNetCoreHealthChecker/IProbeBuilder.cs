

namespace AspNetCoreHealthChecker;

public interface IProbeBuilder
{
  public bool Check(string name);

  public IProbe Build(Config.Probe probeConfig);
}
