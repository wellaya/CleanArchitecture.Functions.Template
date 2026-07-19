using CleanArchitecture.Functions.Application;
using CleanArchitecture.Functions.Infrastructure;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.Hosting;
using Platform.Functions;
using Platform.Functions.Extensions;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();
builder.UsePlatformMiddleware();

builder.Services.AddPlatformFunctionsServices();   
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Build().Run();