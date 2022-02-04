using AspNetCoreHealthChecker.Config;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreHealthChecker.RabbitMQ
{
  public class RabbitMQPlugin : IPlugin
  {
    public bool Check(string text)
    {
      throw new NotImplementedException();
    }

    public void Run(IHealthChecksBuilder healthChecksBuilder, Probe probe)
    {
      throw new NotImplementedException();
    }
  }
}