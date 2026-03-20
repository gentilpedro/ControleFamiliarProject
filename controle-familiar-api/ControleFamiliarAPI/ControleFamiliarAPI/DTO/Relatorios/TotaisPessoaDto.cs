namespace ControleFamiliarAPI.DTOs.Relatorios
{
    public class TotaisPessoaDto
    {
        public string Pessoa { get; set; } = string.Empty;

        public decimal TotalReceitas { get; set; }

        public decimal TotalDespesas { get; set; }

        public decimal Saldo => TotalReceitas - TotalDespesas;
    }
}