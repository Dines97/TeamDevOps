namespace AspNetCoreHealthChecker.NpgSql;

public class NpgSqlPlugin : IPlugin
{
  private readonly List<IProbe> _supportedProbes = new() { new NpgSqlHandler() };

  public IEnumerable<IProbe> GetProbeTypes()
  {
    return _supportedProbes;
  }
}
