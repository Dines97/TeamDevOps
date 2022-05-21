using AspNetCoreHealthChecker.Config;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreHealthChecker.NpgSql;

public class NpgSqlHandler: IProbe
{
  public Type ConfigType => typeof(NpgSqlProperties);
  public bool Check(string name)
  {
    return String.Compare(name, "NpgSql", StringComparison.OrdinalIgnoreCase) == 0;
  }

  public void Configure(IHealthChecksBuilder builder, Properties properties)
  {
    var p = properties as NpgSqlProperties;

    builder.AddNpgSql(p.ConnectionString, name: p.Name);
  }
}

internal class NpgSqlProperties : Properties
{
  public string ConnectionString { get; set; }
}
