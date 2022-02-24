using AspNetCoreHealthChecker.Config;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AspNetCoreHealthChecker;

public class ProbeDelegate
{
  [JsonProperty("Name")] private string _name;
  [JsonProperty("Type")] private string _type;
  [JsonProperty("Description")] private string _description;
  private int _retryCount;
  private int _timeout;

  [JsonConverter(typeof(StringEnumConverter))] [JsonProperty("Severity")]
  private SeverityEnum _severityEnum;

  private readonly IProbe _probe;
  [JsonProperty("Probe result")] private ProbeResult? _probeResult;

  public ProbeDelegate(IProbeBuilder probeBuilder, Config.Probe probeConfig)
  {
    this._name = probeConfig.Name;
    this._type = probeConfig.Type;
    this._description = probeConfig.Description;
    this._retryCount = probeConfig.RetryCount;
    this._timeout = probeConfig.Timeout;
    this._severityEnum = probeConfig.Severity;

    _probe = probeBuilder.Build(probeConfig.Properties);
  }

  public void Run()
  {
    _probeResult = _probe.Run();
  }
}
