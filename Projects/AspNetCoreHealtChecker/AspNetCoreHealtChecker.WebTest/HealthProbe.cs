using System.Collections.Generic;

namespace TurkuazGO.VehicleShipment.WebApi
{
  public class HealthProbe
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
    public Dictionary<string, object> Properties { get; set; }
  }

  public enum SeverityEnum
  {
    Low,
    Normal,
    Critical
  }
}
