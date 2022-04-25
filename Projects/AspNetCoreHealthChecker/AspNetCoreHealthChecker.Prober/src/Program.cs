using System.Net;
using k8s;
using k8s.Models;
using Microsoft.VisualBasic.CompilerServices;
using AspNetCoreHealthChecker;
using AspNetCoreHealthChecker.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;

namespace AspNetCoreHealthChecker.Prober;

internal class Program
{
  private static void Main(string[] args)
  {
    var builder = WebApplication.CreateBuilder(args);
    builder.ConfigureHealthCheck();
    
    var app = builder.Build();
    app.UseAspNetHealthChecks();
    

    app = builder.Build();
    
    var k8SClientConfig = KubernetesClientConfiguration.InClusterConfig();
    var client = new Kubernetes(k8SClientConfig);

    var crd = Utils.MakeProbeDefinition();
    var generic = new GenericClient(client, crd.Group, crd.Version, crd.PluralName);

    var crs = generic.ListNamespacedAsync<CustomResourceList<Probe>>("default").ConfigureAwait(false).GetAwaiter()
      .GetResult();
    foreach (var cr in crs.Items)
    {
      Console.WriteLine("- CR Item {0} = {1}", crs.Items.IndexOf(cr), cr.Metadata.Name);
    }
  }
}
