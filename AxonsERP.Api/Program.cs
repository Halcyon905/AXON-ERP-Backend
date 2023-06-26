using AxonsERP.Api.Extensions;
using Microsoft.AspNetCore.HttpOverrides;

var builder = WebApplication.CreateBuilder(args);
builder.Services.ConfigureCors();
builder.Services.ConfigureIISIntegration();
builder.Services.ConfigureRepositorManager();
builder.Services.ConfigureServiceManager();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllers()
  .AddApplicationPart(typeof(AxonsERP.Api.Presentation.AssemblyReference).Assembly);
builder.Services.ConfigureSwagger();

var app = builder.Build();
app.ConfigureExceptionHandler();
if (app.Environment.IsProduction())
    app.UseHsts();

app.UseStaticFiles();
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.All
});

app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger(c =>
{
    c.RouteTemplate = $"{builder.Configuration["ROU"]}" + "/{documentName}/swagger.json";
});

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint($"/{builder.Configuration["ROU"]}/v1/swagger.json", "My API V1");
    c.RoutePrefix = $"{builder.Configuration["ROU"]}";
});

app.UsePathBase($"/{builder.Configuration["ROU"]}");
app.UseRouting();
app.MapControllers();
app.Run();
