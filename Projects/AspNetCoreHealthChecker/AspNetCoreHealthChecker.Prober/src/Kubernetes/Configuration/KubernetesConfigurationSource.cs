using k8s;

namespace AspNetCoreHealthChecker.Prober.Kubernetes.Configuration;

public class KubernetesConfigurationSource : IConfigurationSource
{
  private readonly GenericClient _genericClient;

  public KubernetesConfigurationSource(GenericClient genericClient)
  {
    _genericClient = genericClient;
  }

  public IConfigurationProvider Build(IConfigurationBuilder builder)
  {
    return new KubernetesConfigurationProvider(_genericClient);
  }
}
