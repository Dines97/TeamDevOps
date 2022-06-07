using k8s;

namespace AspNetCoreHealthChecker.Prober.Kubernetes.Configuration;

public class KubernetesConfigurationProvider : ConfigurationProvider
{
  private readonly k8s.Kubernetes _client;
  private readonly CustomResourceDefinition _crd;
  private readonly GenericClient _genericClient;

  public KubernetesConfigurationProvider(k8s.Kubernetes client, CustomResourceDefinition crd,
    GenericClient genericClient)
  {
    _client = client;
    _crd = crd;
    _genericClient = genericClient;
  }

  public override void Load()
  {
    var crs = _genericClient.ListNamespacedAsync<CustomResourceList<Probe>>("default").ConfigureAwait(false)
      .GetAwaiter()
      .GetResult();

    var resp = _client
      .ListNamespacedCustomObjectWithHttpMessagesAsync(_crd.Group, _crd.Version, "default", _crd.PluralName)
      .ConfigureAwait(false).GetAwaiter().GetResult();
    //return KubernetesJson.Deserialize<T>(resp.Body.ToString());

    var jsonString = resp.Body.ToString();


    Data = KubernetesConfigurationParser.Parse(jsonString);

    // jsonObject = JsonSerializer.Deserialize<JsonDocument>(jsonString).RootElement;


    // var i = 0;
    // foreach (var jsonNode in jsonObject.GetProperty("items").EnumerateArray())
    // {
    //   //Data.Add($"HealthCheck:Probes:{i}:Name", jsonNode["metadata"]["name"].ToString());
    //
    //   foreach (var properties in jsonNode["spec"].AsObject())
    //   {
    //     try
    //     {
    //       Data.Add($"HealthCheck:Probes:{i}:{properties.Key}", properties.Value.ToString());
    //     }
    //     catch (Exception e)
    //     {
    //       Console.WriteLine(e);
    //       throw;
    //     }
    //
    //     Data.Add($"HealthCheck:Probes:{i}:{property.Key}", property.Value.ToString());
    //   }
    //
    //   i++;
    // }
    //
    // Console.WriteLine(jsonObject.ToString());
    //
    // foreach (var test in Data)
    // {
    //   Console.WriteLine(test.Key + " " + test.Value);
    // }


    // var json = new
    // {
    //   HealtCheck = new
    //   {
    //     IgnoreUnsupportedProbes = true,
    //     Plugins = new[]
    //     {
    //       "AspNetCoreHealthChecker.RabbitMQ", "AspNetCoreHealthChecker.Network", "AspNetCoreHealthChecker.Uri"
    //     },
    //     Probes = Array.Empty<JsonObject>(),
    //     Endpoints = new[] { new { Name = "json", Uri = "/hc", ResponseType = "json" } }
    //   }
    // };


    // string test = File.ReadAllText("template.json");
    // var newJsonObject = JsonObject.Parse(test);
    // Console.WriteLine(newJsonObject);
    //
    // foreach (var i in jsonObject["items"].AsArray())
    // {
    //   foreach (var j in i["spec"].AsObject())
    //   {
    //     Console.WriteLine(j);
    //     
    //   }
    // }


    // var config = new MapperConfiguration(cfg =>
    // {
    //   cfg.CreateMap<Probe, Properties>()
    //     .ForMember(dest=>dest.)
    // });

    // Console.WriteLine(resp.Body.ToString());
    // var test = JsonConvert.DeserializeObject<Dictionary<string, JObject>>(resp.Body.ToString());
    //
    // Console.WriteLine(test);
    // Console.WriteLine(test);

    // Console.WriteLine(dynamic.ToString());
    // Console.WriteLine(dynamic);


    // List<dynamic> intermediateProbes = new List<dynamic>();
    //
    // foreach (var probe in dynamic.items)
    // {
    //   dynamic tmp;
    //
    //   foreach (var k in probe.GetType().GetProperties())
    //   {
    //     Console.WriteLine(k);
    //   }
    //   
    //   
    //   intermediateProbes.Add(new
    //   {
    //     probe
    //   });
    // }

    // var json = new
    // {
    //   HealtCheck = new
    //   {
    //     IgnoreUnsupportedProbes = true,
    //     Plugins = new[]
    //     {
    //       "AspNetCoreHealthChecker.RabbitMQ", "AspNetCoreHealthChecker.Network", "AspNetCoreHealthChecker.Uri"
    //     },
    //     Probes = new dynamic[]
    //     {
    //       
    //     },
    //     Endpoints = new object[] { new { Name = "json", Uri = "/hc", ResponseType = "json" } }
    //   }
    // };


    // var i = 0;
    // foreach (var cr in crs.Items)
    // {
    //   Console.WriteLine("- CR Item {0} = {1}", crs.Items.IndexOf(cr), cr.Metadata.Name);
    //   Console.WriteLine(cr.ToString());
    //   Console.WriteLine(cr.Spec.Properties.ToString());
    //   
    //   
    //   Data[$"HealthCheck:Probes:{i}:Name"] = cr.Metadata.Name;
    // }
  }
}
