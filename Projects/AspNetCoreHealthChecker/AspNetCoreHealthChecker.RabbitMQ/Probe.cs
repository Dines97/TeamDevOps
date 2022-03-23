using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreHealthChecker.RabbitMQ
{
  public class Probe : IProbe
  {
    public Type ConfigType { get; }

    public bool Check(string name)
    {
      return String.Compare(name, "RabbitMQ", StringComparison.OrdinalIgnoreCase) == 0;
    }

    public void Configure(IHealthChecksBuilder builder, Config.Probe probeConfigProperties)
    {
      builder.AddRabbitMQ(probeConfigProperties.Properties["ConnectionString"] as string,
        name: probeConfigProperties.Name);
    }
  }
}
