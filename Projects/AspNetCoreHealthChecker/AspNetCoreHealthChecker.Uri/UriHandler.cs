using AspNetCoreHealthChecker.Config;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreHealthChecker.Uri;

public class UriHandler : IProbe
{
  public Type ConfigType => typeof(UriProperties);

  public bool Check(string name)
  {
    return String.Compare(name, "uri", StringComparison.OrdinalIgnoreCase) == 0;
  }

  public void Configure(IHealthChecksBuilder builder, Properties properties)
  {
    var p = properties as UriProperties;

    builder.AddUrlGroup(p.Uri, p.Name);
  }

  private class UriProperties : Properties
  {
    public System.Uri Uri { get; set; }

    public int StatusCode { get; set; }
  }
}
