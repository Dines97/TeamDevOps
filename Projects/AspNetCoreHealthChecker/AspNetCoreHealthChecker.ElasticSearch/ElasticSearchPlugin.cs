namespace AspNetCoreHealthChecker.Elasticsearch;

public class ElasticSearchPlugin : IPlugin
{
  private readonly List<IProbe> _supportedProbes = new() { };

  public IEnumerable<IProbe> GetProbeTypes()
  {
    return _supportedProbes;
  }
}
