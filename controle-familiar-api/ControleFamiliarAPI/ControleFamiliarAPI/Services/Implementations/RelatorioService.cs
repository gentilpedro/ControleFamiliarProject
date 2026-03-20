using ClosedXML.Excel;
using ControleFamiliarAPI.DTO.Relatorios;
using ControleFamiliarAPI.DTOs.Relatorios;
using ControleFamiliarAPI.Services.Interfaces;
using ControleGastos.Api.Data;
using ControleGastos.Api.Models.Enums;
using Microsoft.EntityFrameworkCore;

public class RelatorioService : IRelatorioService
{
    private readonly AppDbContext _context;

    public RelatorioService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ResumoPessoasDto> TotaisPorPessoa()
    {
        var pessoas = await _context.Pessoas
            .Select(p => new TotaisPessoaDto
            {
                Pessoa = p.Nome,

                TotalReceitas = p.Transacoes
                    .Where(t => t.Tipo == TipoTransacao.Receita)
                    .Sum(t => (decimal?)t.Valor) ?? 0,

                TotalDespesas = p.Transacoes
                    .Where(t => t.Tipo == TipoTransacao.Despesa)
                    .Sum(t => (decimal?)t.Valor) ?? 0
            })
            .ToListAsync();

        return new ResumoPessoasDto
        {
            Pessoas = pessoas,
            TotalReceitas = pessoas.Sum(x => x.TotalReceitas),
            TotalDespesas = pessoas.Sum(x => x.TotalDespesas)
        };
    }

    public async Task<List<TotaisCategoriaDto>> TotaisPorCategoria()
    {
        return await _context.Transacoes
            .Include(t => t.Categoria)
            .Where(t => t.Tipo == TipoTransacao.Despesa)
            .GroupBy(t => t.Categoria!.Descricao)
            .Select(g => new TotaisCategoriaDto
            {
                Categoria = g.Key,
                Total = g.Sum(x => x.Valor)
            })
            .ToListAsync();
    }

    public async Task<byte[]> GerarExcelTotaisPessoa()
    {
        var resumo = await TotaisPorPessoa();

        using var workbook = new XLWorkbook();
        var ws = workbook.Worksheets.Add("Relatório");

        int linha = 1;

        ws.Cell(linha, 1).Value = "RELATÓRIO FINANCEIRO - TOTAIS POR PESSOA";
        ws.Range(linha, 1, linha, 4).Merge();
        ws.Cell(linha, 1).Style.Font.Bold = true;
        ws.Cell(linha, 1).Style.Font.FontSize = 16;

        linha += 2;

        ws.Cell(linha, 1).Value = $"Gerado em: {DateTime.Now:dd/MM/yyyy HH:mm}";
        linha += 2;

        ws.Cell(linha, 1).Value = "Pessoa";
        ws.Cell(linha, 2).Value = "Receitas";
        ws.Cell(linha, 3).Value = "Despesas";
        ws.Cell(linha, 4).Value = "Saldo";

        ws.Range(linha, 1, linha, 4).Style.Font.Bold = true;

        linha++;

        foreach (var item in resumo.Pessoas)
        {
            ws.Cell(linha, 1).Value = item.Pessoa;
            ws.Cell(linha, 2).Value = item.TotalReceitas;
            ws.Cell(linha, 3).Value = item.TotalDespesas;
            ws.Cell(linha, 4).Value = item.TotalReceitas - item.TotalDespesas;

            ws.Cell(linha, 2).Style.NumberFormat.Format = "R$ #,##0.00";
            ws.Cell(linha, 3).Style.NumberFormat.Format = "R$ #,##0.00";
            ws.Cell(linha, 4).Style.NumberFormat.Format = "R$ #,##0.00";

            linha++;
        }

        ws.Columns().AdjustToContents();

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);

        return stream.ToArray();
    }

    public async Task<byte[]> GerarExcelTotaisCategoria()
    {
        var categorias = await TotaisPorCategoria();

        using var workbook = new XLWorkbook();
        var ws = workbook.Worksheets.Add("Relatório");

        ws.Cell(1, 1).Value = "Categoria";
        ws.Cell(1, 2).Value = "Total";

        int linha = 2;

        foreach (var item in categorias)
        {
            ws.Cell(linha, 1).Value = item.Categoria;
            ws.Cell(linha, 2).Value = item.Total;

            ws.Cell(linha, 2).Style.NumberFormat.Format = "R$ #,##0.00";

            linha++;
        }

        ws.Columns().AdjustToContents();

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);

        return stream.ToArray();
    }
}