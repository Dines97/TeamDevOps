using k8s;

namespace AspNetCoreHealthChecker.Prober.Kubernetes.Configuration;

public class KubernetesConfigurationProvider : ConfigurationProvider
{
  private readonly GenericClient _genericClient;

  public KubernetesConfigurationProvider(GenericClient genericClient)
  {
    _genericClient = genericClient;
  }

  public override void Load()
  {
    var crs = _genericClient.ListNamespacedAsync<CustomResourceList<Probe>>("default").ConfigureAwait(false).GetAwaiter()
      .GetResult();

    // int i = 0;
    // foreach (var cr in crs.Items)
    // { 
    //   Console.WriteLine("- CR Item {0} = {1}", crs.Items.IndexOf(cr), cr.Metadata.Name);
    //   
    //   Data[$"HealthCheck:Probes:{i}:Name"] = cr.Metadata.Name;
    //   cr.
    //   i++;
    // }
  }
}
