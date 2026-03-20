using ControleFamiliarAPI.DTOs.Categoria;

namespace ControleFamiliarAPI.Services.Interfaces
{
    public interface ICategoriaService
    {
        Task<List<CategoriaResponseDto>> Listar();
        Task<CategoriaResponseDto> Criar(CategoriaCreateDto dto);
        Task Deletar(int id);
    }
}