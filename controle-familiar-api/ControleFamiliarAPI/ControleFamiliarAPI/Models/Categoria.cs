using System.ComponentModel.DataAnnotations;
using ControleGastos.Api.Models.Enums;

namespace ControleGastos.Api.Models
{
    public class Categoria
    {
        /// <summary>
        /// Identificador único gerado automaticamente.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Descrição da categoria. Máximo de 400 caracteres.
        /// </summary>
        [Required]
        [MaxLength(400)]
        public string Descricao { get; set; } = string.Empty;

        /// <summary>
        /// Define se a categoria pode ser usada para receita, despesa ou ambas.
        /// </summary>
        [Required]
        public FinalidadeCategoria Finalidade { get; set; }

        /// <summary>
        /// Lista de transações vinculadas à categoria.
        /// </summary>
        public List<Transacao> Transacoes { get; set; } = new();
    }
}