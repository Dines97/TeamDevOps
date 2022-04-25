using System.Text.Json.Serialization;
using k8s.Models;

namespace AspNetCoreHealthChecker.Prober;

public class Probe : CustomResource<ProbeSpec, ProbeStatus>
{
}

public class ProbeSpec
{
  [JsonPropertyName("cityName")] public string CityName { get; set; }
}

public class ProbeStatus : V1Status
{
  [JsonPropertyName("temperature")] public string Temperature { get; set; }
}
