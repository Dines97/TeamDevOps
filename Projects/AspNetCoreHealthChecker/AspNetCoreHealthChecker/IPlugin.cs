namespace AspNetCoreHealthChecker
{
  public interface IPlugin
  {
    IEnumerable<IProbe> GetProbeTypes();
  }
}
