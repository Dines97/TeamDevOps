namespace AspNetCoreHealthChecker.Config;

public class HealthCheck
{
  public bool IgnoreUnsupportedProbes { get; set; } = false;

  public string[] Plugins { get; set; } = Array.Empty<string>();

  public Properties[] Probes { get; set; }

  public Endpoint[] Endpoints { get; set; }
}

public class Properties
{
  //{
  //   "Name": "SomeName",
  public string Name { get; set; }

  //   "Type": "HttpRequest",
  public string Type { get; set; }

  public string Description { get; set; }

  // //   "Severity": "Critical",
  // public SeverityEnum Severity { get; set; } = SeverityEnum.Normal;

  public IEnumerable<string> Tags { get; set; }
  
}

public class Endpoint
{
  public string Name { get; set; }

  public string Uri { get; set; }

  public ResponseType ResponseType { get; set; }

  public String? Tag { get; set; }
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
