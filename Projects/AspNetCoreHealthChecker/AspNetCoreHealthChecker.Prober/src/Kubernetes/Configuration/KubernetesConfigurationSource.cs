using k8s;

namespace AspNetCoreHealthChecker.Prober.Kubernetes.Configuration;

public class KubernetesConfigurationSource : IConfigurationSource
{
  private readonly k8s.Kubernetes _client;
  private readonly CustomResourceDefinition _crd;
  private readonly GenericClient _genericClient;

  public KubernetesConfigurationSource(k8s.Kubernetes client, CustomResourceDefinition crd, GenericClient genericClient)
  {
    _client = client;
    _crd = crd;
    _genericClient = genericClient;
  }

  public IConfigurationProvider Build(IConfigurationBuilder builder)
  {
    return new KubernetesConfigurationProvider(_client, _crd, _genericClient);
  }
}
