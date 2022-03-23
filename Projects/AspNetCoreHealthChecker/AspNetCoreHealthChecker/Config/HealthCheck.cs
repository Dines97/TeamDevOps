using Newtonsoft.Json;

namespace AspNetCoreHealthChecker.Config
{
  public class HealthCheck
  {

    public bool IgnoreUnsupportedProbes { get; set; } = false;

    public string[] Plugins { get; set; } = Array.Empty<string>();

    public Probe[] Probes { get; set; }

    public Endpoint[] Endpoints { get; set; }
  }


  public class Probe
  {
    //{
    //   "Name": "SomeName",
    public string Name { get; set; }

    //   "Type": "HttpRequest",
    public string Type { get; set; }

    //   "Timeout": 1,
    public int Timeout { get; set; }

    // "RetryCount": 1
    public int RetryCount { get; set; } = 1;


    //   "Severity": "Critical",
    public SeverityEnum Severity { get; set; } = SeverityEnum.Normal;

    //   "Properties": {
    //     "Path": "some.service",
    //     "ReturnCode": 200
    //   }
    
    public string Description { get; set; }
    
    public Dictionary<string, object> Properties { get; set; }
  }

  public class Endpoint
  {
    public string Name { get; set; }

    public string Uri { get; set; }

    public ResponseType ResponseType { get; set; }
  }


  public enum ResponseType
  {
    PlainText,
    Json
  }

  public enum SeverityEnum
  {
    Low,
    Normal,
    Critical
  }
}
