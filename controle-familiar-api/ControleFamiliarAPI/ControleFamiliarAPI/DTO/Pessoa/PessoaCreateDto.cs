using System.ComponentModel.DataAnnotations;

namespace ControleFamiliarAPI.DTO.Pessoa
{
    public class PessoaCreateDto
    {
        [Required]
        [MaxLength(200)]
        public string Nome { get; set; } = string.Empty;

        [Required]
        public int Idade { get; set; }
    }
}