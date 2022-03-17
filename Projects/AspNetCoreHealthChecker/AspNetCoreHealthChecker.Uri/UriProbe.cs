using AspNetCoreHealthChecker.Config;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreHealthChecker.Uri;

public class UriProbe : IProbe
{
  class Properties
  {
    public System.Uri Uri { get; set; }
  }

  public bool Check(string name)
  {
    return String.Compare(name, "uri", StringComparison.OrdinalIgnoreCase) == 0;
  }

  public void Configure(IHealthChecksBuilder builder, Probe probeConfigProperties)
  {
    var p = probeConfigProperties.Properties.ToObject<Properties>();

    builder.AddUrlGroup(uri: p.Uri, name: probeConfigProperties.Name);
  }
}
