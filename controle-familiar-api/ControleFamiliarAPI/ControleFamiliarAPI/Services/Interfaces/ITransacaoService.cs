using ControleFamiliarAPI.DTOs.Transacao;

namespace ControleFamiliarAPI.Services.Interfaces
{
    public interface ITransacaoService
    {
        Task<List<TransacaoResponseDto>> Listar();

        Task Criar(TransacaoCreateDto dto);
    }
}