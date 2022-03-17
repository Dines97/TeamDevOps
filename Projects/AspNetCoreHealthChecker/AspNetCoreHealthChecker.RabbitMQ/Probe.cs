using AspNetCoreHealthChecker.Config;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace AspNetCoreHealthChecker.RabbitMQ;

public class Probe : IProbe
{
  class Properties
  {
    public string ConnectionString { get; set; }
  }

  public bool Check(string name)
  {
    return String.Compare(name, "RabbitMQ", StringComparison.OrdinalIgnoreCase) == 0;
  }

  public void Configure(IHealthChecksBuilder builder, Config.Probe probeConfigProperties)
  {
    var p = probeConfigProperties.Properties.ToObject<Properties>();

    builder.AddRabbitMQ(p.ConnectionString,
      name: probeConfigProperties.Name);
  }
}
