using AspNetCoreHealthChecker.Config;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreHealthChecker.Elasticsearch;

public class ElasticSearchHandler : IProbe
{
  public Type ConfigType => typeof(ElasticSearchProperties);

  public bool Check(string name)
  {
    return String.Compare(name, "ElasticSearch", StringComparison.OrdinalIgnoreCase) == 0;
  }

  public void Configure(IHealthChecksBuilder builder, Properties properties)
  {
    var p = properties as ElasticSearchProperties;

    builder.AddElasticsearch(name: p.Name, elasticsearchUri: p.ElasticSearchUri,
      timeout: TimeSpan.FromMilliseconds(p.Timeout));
  }
}

internal class ElasticSearchProperties : Properties
{
  public string ElasticSearchUri { get; set; }

  public int Timeout { get; set; }
}
