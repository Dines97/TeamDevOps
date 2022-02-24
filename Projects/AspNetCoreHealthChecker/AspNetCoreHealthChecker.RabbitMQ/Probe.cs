using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;

namespace AspNetCoreHealthChecker.RabbitMQ
{
  public class Probe : IProbe
  {
    private readonly ConnectionFactory _connectionFactory;

    public Probe(IReadOnlyDictionary<string, object> probeConfigProperties)
    {
      _connectionFactory = new ConnectionFactory
      {
        Uri = new Uri(probeConfigProperties["ConnectionString"] as string)
      };
    }


    public ProbeResult Run()
    {
      try
      {
        IConnection connection = _connectionFactory.CreateConnection();

        using (connection.CreateModel())
        {
          return new ProbeResult(ProbeResultEnum.Success);
        }
      }
      catch (BrokerUnreachableException e)
      {
        return new ProbeResult(ProbeResultEnum.Failure, e);
      }
    }
  }
}
