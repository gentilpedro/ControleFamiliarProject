using ControleFamiliarAPI.DTOs.Transacao;
using ControleFamiliarAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OpenApi;

namespace ControleFamiliarAPI.Controllers
{
    [ApiController]
    [Route("api/transacoes")]
    public class TransacoesController : ControllerBase
    {
        private readonly ITransacaoService _service;

        public TransacoesController(ITransacaoService service)
        {
            _service = service;
        }

        // GET api/transacoes
        [HttpGet]
        [Tags("Transações")]
        [EndpointSummary("Lista todas as transações financeiras")]
        [EndpointDescription("""
            Retorna todas as transações registradas no sistema.
            
            Cada transação contém:
            - Identificador da transação
            - Descrição
            - Valor
            - Tipo (Receita ou Despesa)
            - Pessoa associada
            - Categoria associada
            """)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Listar()
        {
            return Ok(await _service.Listar());
        }

        // POST api/transacoes
        [HttpPost]
        [Tags("Transações")]
        [EndpointSummary("Cria uma nova transação financeira")]
        [EndpointDescription("""
            Registra uma nova transação de receita ou despesa vinculada
            a uma pessoa e a uma categoria existente.
            
            Dados necessários:
            - Descrição da transação
            - Valor (deve ser positivo)
            - Tipo da transação (Receita ou Despesa)
            - Identificador da pessoa
            - Identificador da categoria
            
            Regras de negócio:
            - O valor deve ser maior que zero
            - Pessoas menores de 18 anos podem registrar apenas despesas
            - A categoria deve ser compatível com o tipo da transação
            """)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Criar(TransacaoCreateDto dto)
        {
            await _service.Criar(dto);

            return Ok();
        }
    }
}