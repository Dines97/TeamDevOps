namespace AspNetCoreHealthChecker;

public interface IProbe
{
  ProbeResult Run();
}
