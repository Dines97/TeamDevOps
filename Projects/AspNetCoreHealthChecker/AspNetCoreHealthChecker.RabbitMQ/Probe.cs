using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;

namespace AspNetCoreHealthChecker.RabbitMQ
{
  public class Probe : IProbe
  {
    private string Name { get; set; }
    private string Description { get; set; }
    private int Timeout { get; set; }
    private Config.SeverityEnum Severity { get; set; }

    private readonly ConnectionFactory connectionFactory;


    public Probe(Config.Probe probeConfig)
    {
      Name = probeConfig.Name;
      Description = probeConfig.Description;
      Timeout = probeConfig.Timeout;
      Severity = probeConfig.Severity;


      connectionFactory = new ConnectionFactory();
      connectionFactory.Uri = new Uri(probeConfig.Properties["ConnectionString"] as string);
    }

    

    public ProbeResult Run()
    {
      try
      {
        IConnection connection = connectionFactory.CreateConnection();

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

  public class ProbeBuilder : IProbeBuilder
  {
    public bool Check(string name)
    {
      return String.Compare(name, "RabbitMQ", StringComparison.OrdinalIgnoreCase) == 0;
    }

    public IProbe Build(Config.Probe probeConfig)
    {
      return new Probe(probeConfig);
    }
  }
}
