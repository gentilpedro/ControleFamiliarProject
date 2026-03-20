using ControleFamiliarAPI.DTO.Pessoa;
using ControleFamiliarAPI.Responses;
using ControleFamiliarAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OpenApi;

namespace ControleFamiliarAPI.Controllers
{
    [ApiController]
    [Route("api/pessoas")]
    public class PessoasController : ControllerBase
    {
        private readonly IPessoaService _service;

        public PessoasController(IPessoaService service)
        {
            _service = service;
        }

        // GET api/pessoas
        [HttpGet]
        [Tags("Pessoas")]
        [EndpointSummary("Lista todas as pessoas cadastradas")]
        [EndpointDescription("""
            Retorna todas as pessoas registradas no sistema.
            
            Cada pessoa possui:
            - Identificador único
            - Nome
            - Idade
            
            Essas informações são utilizadas para vincular transações financeiras
            ao responsável pela receita ou despesa.
            """)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Listar()
        {
            return Ok(await _service.Listar());
        }

        // POST api/pessoas
        [HttpPost]
        [Tags("Pessoas")]
        [EndpointSummary("Cria uma nova pessoa")]
        [EndpointDescription("""
            Registra uma nova pessoa que poderá realizar transações financeiras.
            
            Dados necessários:
            - Nome (máximo de 200 caracteres)
            - Idade
            
            Regras de negócio:
            - Pessoas menores de 18 anos não podem registrar receitas
            - Ao remover uma pessoa, todas as transações associadas a ela
              serão removidas automaticamente
            """)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Criar(PessoaCreateDto dto)
        {
            var pessoa = await _service.Criar(dto);
            return Ok(new ApiResponse<object>(pessoa));
        }

        // PATCH api/pessoas/{id}
        [HttpPatch("{id}")]
        [Tags("Pessoas")]
        [EndpointSummary("Atualiza os dados de uma pessoa")]
        [EndpointDescription("""
            Permite alterar o nome ou idade de uma pessoa já cadastrada.
            O identificador da pessoa deve ser informado na rota.
            """)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Atualizar(int id, PessoaUpdateDto dto)
        {
            await _service.Atualizar(id, dto);
            return Ok(new ApiResponse<string>("Pessoa atualizada com sucesso"));
        }

        // DELETE api/pessoas/{id}
        [HttpDelete("{id}")]
        [Tags("Pessoas")]
        [EndpointSummary("Remove uma pessoa do sistema")]
        [EndpointDescription("""
            Remove uma pessoa cadastrada através do seu identificador.
            
            Importante:
            - Todas as transações associadas a essa pessoa
              serão removidas automaticamente do sistema
            """)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Deletar(int id)
        {
            await _service.Deletar(id);
            return Ok(new ApiResponse<string>("Pessoa removida com sucesso"));
        }
    }
}