using AspNetCoreHealthChecker.Config;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreHealthChecker;

public class MatrixHandler : IProbe
{
  public Type ConfigType { get; }
  public bool Check(string type)
  {
    throw new NotImplementedException();
  }

  public void Configure(IHealthChecksBuilder builder, Properties properties)
  {
    throw new NotImplementedException();
  }
}
