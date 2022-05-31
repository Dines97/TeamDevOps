using k8s;

namespace AspNetCoreHealthChecker.Prober.Kubernetes.Configuration;

public static class ConfigurationBuilderExtensions
{
  public static IConfigurationBuilder AddKubernetesConfiguration(this IConfigurationBuilder builder,
    k8s.Kubernetes client, CustomResourceDefinition crd, GenericClient genericClient)
  {
    return builder.Add(new KubernetesConfigurationSource(client, crd, genericClient));
  }
}
