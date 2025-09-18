using Worker.Repositories;

var builder = Host.CreateApplicationBuilder(args);

string connection = builder.Configuration.GetConnectionString("Connection");

builder.Services.AddScoped<EmprestimoRepository>(sp => new EmprestimoRepository(connection));

builder.Services.AddHostedService<RelatorioWorker>();

var host = builder.Build();

IHost hosted = Host.CreateDefaultBuilder(args).ConfigureServices(
    services =>
    {
        services.AddHostedService<RelatorioWorker>();
    })
    .Build();

host.Run();
