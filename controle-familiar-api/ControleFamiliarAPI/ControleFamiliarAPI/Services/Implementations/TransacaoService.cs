using ControleFamiliarAPI.DTOs.Transacao;
using ControleFamiliarAPI.Services.Interfaces;
using ControleGastos.Api.Data;
using ControleGastos.Api.Models;
using ControleGastos.Api.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace ControleFamiliarAPI.Services.Implementations
{
    /// <summary>
    /// Serviço responsável pela lógica de negócio das transações financeiras.
    /// </summary>
    public class TransacaoService : ITransacaoService
    {
        private readonly AppDbContext _context;

        public TransacaoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<TransacaoResponseDto>> Listar()
        {
            return await _context.Transacoes
                .Include(t => t.Pessoa)
                .Include(t => t.Categoria)
                .Select(t => new TransacaoResponseDto
                {
                    Id = t.Id,
                    Descricao = t.Descricao,
                    Valor = t.Valor,
                    Tipo = t.Tipo,
                    Pessoa = t.Pessoa!.Nome,
                    Categoria = t.Categoria!.Descricao
                })
                .ToListAsync();
        }

        public async Task Criar(TransacaoCreateDto dto)
        {
            if (dto.Valor <= 0)
                throw new Exception("O valor deve ser positivo.");

            var pessoa = await _context.Pessoas.FindAsync(dto.PessoaId);

            if (pessoa == null)
                throw new Exception("Pessoa não encontrada.");

            var categoria = await _context.Categorias.FindAsync(dto.CategoriaId);

            if (categoria == null)
                throw new Exception("Categoria não encontrada.");

            // REGRA 1
            if (pessoa.Idade < 18 && dto.Tipo == TipoTransacao.Receita)
                throw new Exception("Menores de idade só podem registrar despesas.");

            // REGRA 2
            if (dto.Tipo == TipoTransacao.Receita &&
                categoria.Finalidade == FinalidadeCategoria.Despesa)
                throw new Exception("Categoria incompatível.");

            if (dto.Tipo == TipoTransacao.Despesa &&
                categoria.Finalidade == FinalidadeCategoria.Receita)
                throw new Exception("Categoria incompatível.");

            var transacao = new Transacao
            {
                Descricao = dto.Descricao,
                Valor = dto.Valor,
                Tipo = dto.Tipo,
                PessoaId = dto.PessoaId,
                CategoriaId = dto.CategoriaId
            };

            _context.Transacoes.Add(transacao);

            await _context.SaveChangesAsync();
        }
    }
}