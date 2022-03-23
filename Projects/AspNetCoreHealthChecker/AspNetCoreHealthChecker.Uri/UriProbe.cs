using AspNetCoreHealthChecker.Config;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreHealthChecker.Uri;

public class UriProbe : IProbe
{
  public Type ConfigType { get; }

  public bool Check(string name)
  {
    return String.Compare(name, "uri", StringComparison.OrdinalIgnoreCase) == 0;
  }

  public void Configure(IHealthChecksBuilder builder, Probe probeConfigProperties)
  {
    var uri = new System.Uri((string)probeConfigProperties.Properties["Uri"]);

    builder.AddUrlGroup(uri: uri, name: probeConfigProperties.Name);
  }
}
