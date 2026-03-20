using System.ComponentModel.DataAnnotations;
using ControleGastos.Api.Models.Enums;

namespace ControleFamiliarAPI.DTOs.Categoria
{
    public class CategoriaCreateDto
    {
        [Required]
        [MaxLength(400)]
        public string Descricao { get; set; } = string.Empty;

        [Required]
        public FinalidadeCategoria Finalidade { get; set; }
    }
}