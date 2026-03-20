using ControleGastos.Api.Models.Enums;

namespace ControleFamiliarAPI.DTOs.Transacao
{
    public class TransacaoResponseDto
    {
        public int Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public TipoTransacao Tipo { get; set; }
        public string Pessoa { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
    }
}