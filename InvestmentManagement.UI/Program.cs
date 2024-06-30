using InvestmentManagement.UI;
using InvestmentManagement.UI.Services;

var builder = Host.CreateApplicationBuilder(args);

builder.Logging.ClearProviders();

builder.Services.AddSingleton<ISocketService, SocketService>();
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
