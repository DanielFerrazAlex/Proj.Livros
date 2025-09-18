using OfficeOpenXml;
using Worker.Models;
using Worker.Repositories;

public class RelatorioWorker : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<RelatorioWorker> _logger;

    public RelatorioWorker(IServiceProvider serviceProvider, ILogger<RelatorioWorker> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Gerando relatório de atrasados...");

            using (var scope = _serviceProvider.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<EmprestimoRepository>();
                var atrasados = await repo.SelecionarAtrasados();

                if (atrasados.Any())
                {
                    GerarExcel(atrasados);
                }
            }

            await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
        }
    }

    private void GerarExcel(List<EmprestimoModel> atrasados)
    {
        ExcelPackage.License.SetNonCommercialPersonal("Daniel");

        using var package = new ExcelPackage();
        var worksheet = package.Workbook.Worksheets.Add("Atrasados");

        worksheet.Cells[1, 1].Value = "Aluno";
        worksheet.Cells[1, 2].Value = "Livro";
        worksheet.Cells[1, 3].Value = "Data Empréstimo";
        worksheet.Cells[1, 4].Value = "Data Devolução";

        int row = 2;
        foreach (var item in atrasados)
        {
            worksheet.Cells[row, 1].Value = item.Aluno;
            worksheet.Cells[row, 2].Value = item.Livro;
            worksheet.Cells[row, 3].Value = item.DataEmprestimo.ToShortDateString();
            worksheet.Cells[row, 4].Value = item.DataDevolucao.ToShortDateString();
            row++;
        }

        var filePath = Path.Combine(AppContext.BaseDirectory, $"Relatorio_Atrasados_{DateTime.Now:yyyyMMdd}.xlsx");
        File.WriteAllBytes(filePath, package.GetAsByteArray());

        _logger.LogInformation("Relatório gerado em: {filePath}", filePath);
    }
}
