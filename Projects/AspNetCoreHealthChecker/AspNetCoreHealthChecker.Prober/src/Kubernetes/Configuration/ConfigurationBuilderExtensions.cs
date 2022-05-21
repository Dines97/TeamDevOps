using k8s;

namespace AspNetCoreHealthChecker.Prober.Kubernetes.Configuration;

public static class ConfigurationBuilderExtensions
{
  public static IConfigurationBuilder AddKubernetesConfiguration(this IConfigurationBuilder builder,
    GenericClient genericClient)
  {
    return builder.Add(new KubernetesConfigurationSource(genericClient));
  }
}
