using System.Text.Json.Serialization;
using AspNetCoreHealthChecker.Config;
using k8s.Models;
using Newtonsoft.Json.Linq;

namespace AspNetCoreHealthChecker.Prober.Kubernetes;

public class Probe : CustomResource<ProbeSpec, ProbeStatus>
{
  public override string ToString()
  {
    return $"{Metadata.Name} - {Spec.Type}";
  }
}

public class ProbeSpec : Properties
{
  public JObject Properties { get; set; }
}

public class ProbeStatus : V1Status
{
  [JsonPropertyName("temperature")] public string Temperature { get; set; }
}
