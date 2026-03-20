using ControleFamiliarAPI.DTO.Pessoa;
using ControleFamiliarAPI.DTOs;

namespace ControleFamiliarAPI.Services.Interfaces
{
    public interface IPessoaService
    {
        Task<List<PessoaResponseDto>> Listar();

        Task<PessoaResponseDto> Criar(PessoaCreateDto dto);

        Task Atualizar(int id, PessoaUpdateDto dto);

        Task Deletar(int id);
    }
}