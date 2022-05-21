using System.Text.Json.Serialization;
using AspNetCoreHealthChecker.Config;
using k8s.Models;

namespace AspNetCoreHealthChecker.Prober.Kubernetes;

public class Probe : CustomResource<Properties, ProbeStatus>
{
  public override string ToString()
  {
    return $"{Metadata.Name} - {Spec.Type}";
  }
}

public class ProbeSpec : Properties
{
}

public class ProbeStatus : V1Status
{
  [JsonPropertyName("temperature")] public string Temperature { get; set; }
}
