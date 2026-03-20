using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ControleGastos.Api.Models.Enums;

namespace ControleGastos.Api.Models
{
    public class Transacao
    {
        /// <summary>
        /// Identificador único gerado automaticamente.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Descrição da transação. Máximo de 400 caracteres.
        /// </summary>
        [Required]
        [MaxLength(400)]
        public string Descricao { get; set; } = string.Empty;

        /// <summary>
        /// Valor da transação. Deve ser positivo.
        /// </summary>
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Valor { get; set; }

        /// <summary>
        /// Tipo da transação: Receita ou Despesa.
        /// </summary>
        [Required]
        public TipoTransacao Tipo { get; set; }

        /// <summary>
        /// Pessoa relacionada à transação.
        /// </summary>
        [Required]
        public int PessoaId { get; set; }
        public Pessoa? Pessoa { get; set; }

        /// <summary>
        /// Categoria relacionada à transação.
        /// </summary>
        [Required]
        public int CategoriaId { get; set; }
        public Categoria? Categoria { get; set; }
    }
}