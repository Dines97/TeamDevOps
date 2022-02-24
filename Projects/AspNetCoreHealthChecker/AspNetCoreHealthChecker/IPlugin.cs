namespace AspNetCoreHealthChecker
{
  public interface IPlugin
  {
    List<IProbeBuilder> GetProbeTypes();
  }
}
