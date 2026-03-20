using ControleGastos.Api.Models.Enums;

namespace ControleFamiliarAPI.DTOs.Categoria
{
    public class CategoriaResponseDto
    {
        public int Id { get; set; }

        public string Descricao { get; set; } = string.Empty;

        public FinalidadeCategoria Finalidade { get; set; }
    }
}