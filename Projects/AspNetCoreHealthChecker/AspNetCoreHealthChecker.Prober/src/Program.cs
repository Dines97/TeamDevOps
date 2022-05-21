using AspNetCoreHealthChecker.Prober;
using AspNetCoreHealthChecker.Prober.Kubernetes;
using AspNetCoreHealthChecker.Prober.Kubernetes.Configuration;
using k8s;

const bool inCluster = true;

var k8SClientConfig = inCluster ? KubernetesClientConfiguration.InClusterConfig() : KubernetesClientConfiguration.BuildConfigFromConfigFile();

var client = new Kubernetes(k8SClientConfig);

var crd = Utils.MakeProbeDefinition();
var generic = new GenericClient(client, crd.Group, crd.Version, crd.PluralName);

Console.WriteLine("CR list: ");
var probes = await generic.ListNamespacedAsync<CustomResourceList<Probe>>("default");

foreach (var probe in probes.Items)
{
  Console.WriteLine(probe.ToString());
}

var builder = WebApplication.CreateBuilder(args);
//builder.Configuration.AddKubernetesConfiguration(generic);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
