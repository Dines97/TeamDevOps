namespace AspNetCoreHealthChecker.Prober.Kubernetes;

public static class Utils
{
  public static CustomResourceDefinition MakeProbeDefinition()
  {
    return  new CustomResourceDefinition()
    {
      Kind = "Probe", Group = "dteknoloji.com", Version = "v1alpha1", PluralName = "probes"
    };
    
  }
}
