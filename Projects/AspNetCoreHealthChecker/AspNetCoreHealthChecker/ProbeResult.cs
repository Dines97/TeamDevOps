using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AspNetCoreHealthChecker;

public class ProbeResult
{
  [JsonConverter(typeof(StringEnumConverter))]
  [JsonProperty("Result")]
  private ProbeResultEnum ResultEnum { get; set; }

  [JsonProperty("Exception")] public Exception? Exception { get; set; }

  public ProbeResult(ProbeResultEnum resultEnum)
  {
    ResultEnum = resultEnum;
    Exception = null;
  }

  public ProbeResult(ProbeResultEnum resultEnum, Exception exception)
  {
    ResultEnum = resultEnum;
    Exception = exception;
  }
}

public enum ProbeResultEnum
{
  Success,
  Failure,
  Timeout
}
