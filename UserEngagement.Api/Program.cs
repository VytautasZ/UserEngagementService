using System.Reflection;
using UserEngagement.Application.DependencyInjection;
using UserEngagement.Application.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

ConfigurationManager config = builder.Configuration;
builder.Services.AddControllers();
builder.Services.AddApiVersioning();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(swagger =>
{
    string? xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    swagger.IncludeXmlComments(xmlPath);
});
builder.Services.AddServiceDependencies();
builder.Services.AddRepositoryDependencies();
builder.Services.ConfigureDatabases(config);

WebApplication app = builder.Build();

app.UsePathBase("/user-engagement");
app.UseRouting();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapSwagger();
app.MapControllers();
await app.UseDatabases();
app.Run();