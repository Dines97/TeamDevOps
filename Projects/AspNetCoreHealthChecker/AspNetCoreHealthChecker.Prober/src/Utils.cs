using k8s.Models;

namespace AspNetCoreHealthChecker.Prober;

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
