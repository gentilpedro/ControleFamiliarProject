using ControleFamiliarAPI.DTO.Relatorios;
using ControleFamiliarAPI.DTOs.Relatorios;

namespace ControleFamiliarAPI.Services.Interfaces
{
    public interface IRelatorioService
    {
        Task<ResumoPessoasDto> TotaisPorPessoa();
        Task<List<TotaisCategoriaDto>> TotaisPorCategoria();
        Task<byte[]> GerarExcelTotaisPessoa();
        Task<byte[]> GerarExcelTotaisCategoria();
    }

}