namespace ControleFamiliarAPI.DTOs.Relatorios
{
    public class ResumoPessoasDto
    {
        public List<TotaisPessoaDto> Pessoas { get; set; } = new();

        public decimal TotalReceitas { get; set; }

        public decimal TotalDespesas { get; set; }

        public decimal SaldoLiquido => TotalReceitas - TotalDespesas;
    }
}