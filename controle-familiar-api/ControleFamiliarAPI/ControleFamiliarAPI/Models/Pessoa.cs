using System.ComponentModel.DataAnnotations;

namespace ControleGastos.Api.Models
{
    public class Pessoa
    {
        /// <summary>
        /// Identificador único gerado automaticamente.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nome da pessoa. Máximo de 200 caracteres.
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string Nome { get; set; } = string.Empty;

        /// <summary>
        /// Idade da pessoa.
        /// </summary>
        [Required]
        public int Idade { get; set; }

        /// <summary>
        /// Lista de transações vinculadas à pessoa.
        /// </summary>
        public List<Transacao> Transacoes { get; set; } = new();
    }
}