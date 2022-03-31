﻿using AspNetCoreHealthChecker.Config;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreHealthChecker.RabbitMQ;

public class RabbitMqHandler : IProbe
{
  public Type ConfigType => typeof(RabbitMqProperties);

  public bool Check(string name)
  {
    return String.Compare(name, "RabbitMQ", StringComparison.OrdinalIgnoreCase) == 0;
  }

  public void Configure(IHealthChecksBuilder builder, Properties properties)
  {
    var p = properties as RabbitMqProperties;

    builder.AddRabbitMQ(p.ConnectionString,
      name: p.Name);
  }
}

internal class RabbitMqProperties : Properties
{
  public string ConnectionString { get; set; }
}
