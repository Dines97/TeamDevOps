using AspNetCoreHealthChecker.Prober;
using AspNetCoreHealthChecker.Prober.Kubernetes;
using k8s;

var k8SClientConfig = KubernetesClientConfiguration.InClusterConfig();
var client = new Kubernetes(k8SClientConfig);

var crd = Utils.MakeProbeDefinition();
var generic = new GenericClient(client, crd.Group, crd.Version, crd.PluralName);


var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddKubernetesConfiguration(generic);

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
