using Gtm.Business.Infrastructure.Helpers;
using NLog;
using NLog.Extensions.Logging;

string allowSpecificOrigins = "allowSpecificOrigins";
WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

configuration.AddJsonFile("configuration.json", false, true);

LogManager.Configuration = new NLogLoggingConfiguration(configuration.GetSection("NLog"));

builder.Services.AddDependencyGroup(configuration, allowSpecificOrigins, new Version(1, 0), typeof(Program));

builder.Logging.AddNLog();

WebApplication? app = builder.Build();

app.UseGroup(configuration, app.Environment, allowSpecificOrigins);
