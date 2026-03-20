using ControleFamiliarAPI.DTOs.Categoria;
using ControleFamiliarAPI.Services.Interfaces;
using ControleGastos.Api.Data;
using ControleGastos.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleFamiliarAPI.Services.Implementations
{
    public class CategoriaService : ICategoriaService
    {
        private readonly AppDbContext _context;

        public CategoriaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<CategoriaResponseDto>> Listar()
        {
            return await _context.Categorias
                .Select(c => new CategoriaResponseDto
                {
                    Id = c.Id,
                    Descricao = c.Descricao,
                    Finalidade = c.Finalidade
                })
                .ToListAsync();
        }

        public async Task<CategoriaResponseDto> Criar(CategoriaCreateDto dto)
        {
            var categoria = new Categoria
            {
                Descricao = dto.Descricao,
                Finalidade = dto.Finalidade
            };

            _context.Categorias.Add(categoria);
            await _context.SaveChangesAsync();

            return new CategoriaResponseDto
            {
                Id = categoria.Id,
                Descricao = categoria.Descricao,
                Finalidade = categoria.Finalidade
            };
        }

        public async Task Deletar(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);

            if (categoria == null)
                throw new Exception("Categoria não encontrada.");

            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();
        }
    }
}