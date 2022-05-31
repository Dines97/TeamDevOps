using AspNetCoreHealthChecker.Config;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreHealthChecker.RabbitMQ;

public class RabbitMqHandler : IProbe
{
  public Type ConfigType => typeof(RabbitMqProperties);

  public bool Check(string name)
  {
    return String.Compare(name, "RabbitMQ", StringComparison.OrdinalIgnoreCase) == 0;
  }

  public void Configure(IHealthChecksBuilder builder, Properties properties)
  {
    var p = properties as RabbitMqProperties;

    builder.AddRabbitMQ(
      rabbitConnectionString: p.ConnectionString,
      name: p.Name,
      tags: p.Tags
    );
  }
}

internal class RabbitMqProperties : Properties
{
  public string ConnectionString { get; set; }
}
