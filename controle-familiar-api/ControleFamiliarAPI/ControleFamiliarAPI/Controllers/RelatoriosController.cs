using ControleFamiliarAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OpenApi;

namespace ControleFamiliarAPI.Controllers
{
    [ApiController]
    [Route("api/relatorios")]
    public class RelatoriosController : ControllerBase
    {
        private readonly IRelatorioService _relatorioService;

        public RelatoriosController(IRelatorioService relatorioService)
        {
            _relatorioService = relatorioService;
        }

        // GET api/relatorios/totais-por-pessoa
        [HttpGet("totais-por-pessoa")]
        [Tags("Relatórios")]
        [EndpointSummary("Resumo financeiro por pessoa")]
        [EndpointDescription("""
            Retorna o total de receitas, despesas e saldo agrupado por pessoa.
            
            Este endpoint é utilizado para geração de gráficos e dashboards.
            
            Exemplo de resposta:
            
            {
              "pessoas": [
                {
                  "pessoa": "Pedro",
                  "totalReceitas": 5000,
                  "totalDespesas": 300
                },
                {
                  "pessoa": "Ana",
                  "totalReceitas": 1500,
                  "totalDespesas": 80
                }
              ],
              "totalReceitas": 6500,
              "totalDespesas": 380
            }
            """)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> TotaisPorPessoa()
        {
            var result = await _relatorioService.TotaisPorPessoa();
            return Ok(result);
        }

        // GET api/relatorios/totais-por-categoria
        [HttpGet("totais-por-categoria")]
        [Tags("Relatórios")]
        [EndpointSummary("Resumo de despesas por categoria")]
        [EndpointDescription("""
            Retorna o total de despesas agrupadas por categoria.
            
            Utilizado para gráficos de distribuição de gastos.
            
            Exemplo de resposta:
            
            [
              {
                "categoria": "Alimentação",
                "total": 300
              },
              {
                "categoria": "Lazer",
                "total": 60
              },
              {
                "categoria": "Transporte",
                "total": 80
              }
            ]
            """)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> TotaisPorCategoria()
        {
            var result = await _relatorioService.TotaisPorCategoria();
            return Ok(result);
        }

        // GET api/relatorios/excel-pessoa
        [HttpGet("excel-pessoa")]
        [Tags("Relatórios")]
        [EndpointSummary("Exporta relatório financeiro por pessoa (Excel)")]
        [EndpointDescription("""
            Gera um arquivo Excel contendo:
            
            - Pessoa
            - Total de Receitas
            - Total de Despesas
            - Saldo
            
            O arquivo é retornado como download no formato .xlsx.
            """)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ExcelPessoa()
        {
            var file = await _relatorioService.GerarExcelTotaisPessoa();

            return File(
                file,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "relatorio-financeiro-pessoas.xlsx"
            );
        }

        // GET api/relatorios/excel-categoria
        [HttpGet("excel-categoria")]
        [Tags("Relatórios")]
        [EndpointSummary("Exporta relatório de despesas por categoria (Excel)")]
        [EndpointDescription("""
            Gera um arquivo Excel contendo:
            
            - Categoria
            - Total gasto
            
            O arquivo é retornado como download no formato .xlsx.
            """)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ExcelCategoria()
        {
            var file = await _relatorioService.GerarExcelTotaisCategoria();

            return File(
                file,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "relatorio-financeiro-categorias.xlsx"
            );
        }
    }
}