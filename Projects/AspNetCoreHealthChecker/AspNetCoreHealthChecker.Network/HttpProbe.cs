using AspNetCoreHealthChecker.Config;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreHealthChecker.Network;

public class HttpProbe : IProbe
{
  public bool Check(string name)
  {
    //throw new NotImplementedException();
    return false;
  }

  public void Configure(IHealthChecksBuilder builder, Probe probeConfigProperties)
  {
    //throw new NotImplementedException();
  }
}
