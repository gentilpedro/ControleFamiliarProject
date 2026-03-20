using ControleFamiliarAPI.DTO.Pessoa;
using ControleFamiliarAPI.DTOs;
using ControleFamiliarAPI.Services.Interfaces;
using ControleGastos.Api.Data;
using ControleGastos.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleFamiliarAPI.Services.Implementations
{
    public class PessoaService : IPessoaService
    {
        private readonly AppDbContext _context;

        public PessoaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<PessoaResponseDto>> Listar()
        {
            return await _context.Pessoas
                .Select(p => new PessoaResponseDto
                {
                    Id = p.Id,
                    Nome = p.Nome,
                    Idade = p.Idade
                })
                .ToListAsync();
        }

        public async Task<PessoaResponseDto> Criar(PessoaCreateDto dto)
        {
            var pessoa = new Pessoa
            {
                Nome = dto.Nome,
                Idade = dto.Idade
            };

            _context.Pessoas.Add(pessoa);

            await _context.SaveChangesAsync();

            return new PessoaResponseDto
            {
                Id = pessoa.Id,
                Nome = pessoa.Nome,
                Idade = pessoa.Idade
            };
        }

        public async Task Atualizar(int id, PessoaUpdateDto dto)
        {
            var pessoa = await _context.Pessoas.FindAsync(id);

            if (pessoa == null)
                throw new Exception("Pessoa não encontrada");

            if (!string.IsNullOrEmpty(dto.Nome))
                pessoa.Nome = dto.Nome;

            if (dto.Idade.HasValue)
                pessoa.Idade = dto.Idade.Value;

            await _context.SaveChangesAsync();
        }

        public async Task Deletar(int id)
        {
            var pessoa = await _context.Pessoas.FindAsync(id);

            if (pessoa == null)
                throw new Exception("Pessoa não encontrada");

            _context.Pessoas.Remove(pessoa);

            await _context.SaveChangesAsync();
        }
    }
}