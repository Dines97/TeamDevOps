using AspNetCoreHealthChecker.Config;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreHealthChecker.NpgSql;

public class NpgSqlHandler : IProbe
{
  public Type ConfigType => typeof(NpgSqlProperties);

  public bool Check(string name)
  {
    return String.Compare(name, "NpgSql", StringComparison.OrdinalIgnoreCase) == 0;
  }

  public void Configure(IHealthChecksBuilder builder, Properties properties)
  {
    var p = properties as NpgSqlProperties;

    if (String.IsNullOrEmpty(p.ConnectionString))
    {
      p.ConnectionString =
        $"Host={p.Host}; Port={p.Port}; Username={p.Username}; Password={p.Password}; Database={p.Database}";
    }

    builder.AddNpgSql(
      p.ConnectionString,
      name: p.Name);
  }
}

internal class NpgSqlProperties : Properties
{
  public string ConnectionString { get; set; }

  public string Host { get; set; }

  public ushort Port { get; set; }

  public string Username { get; set; }

  public string Password { get; set; }

  public string Database { get; set; }
}
