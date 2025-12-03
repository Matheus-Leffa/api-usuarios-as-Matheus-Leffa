using Application.Interfaces;

using Microsoft.EntityFrameworkCore;
using Infrastructure.Persistence;
using Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _context;

        public UsuarioRepository(AppDbContext context)
        {
            _context = context;
        }
        
        async Task<IEnumerable<Usuario>> IUsuarioRepository.GetAllAsync(CancellationToken ct)
        {
            return await _context.Usuarios.ToListAsync(ct);
        }

        async Task<Usuario?> IUsuarioRepository.GetByIdAsync(int id, CancellationToken ct)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id, ct);
        }

        async Task<Usuario?> IUsuarioRepository.GetByEmailAsync(string email, CancellationToken ct)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email, ct);
        }

        async Task IUsuarioRepository.AddAsync(Usuario usuario, CancellationToken ct)
        {
            await _context.Usuarios.AddAsync(usuario, ct);
        }

        Task IUsuarioRepository.UpdateAsync(Usuario usuario, CancellationToken ct)
        {
            _context.Usuarios.Update(usuario);
            return Task.CompletedTask;
        }

        Task IUsuarioRepository.RemoveAsync(Usuario usuario, CancellationToken ct)
        {
            _context.Usuarios.Remove(usuario);
            return Task.CompletedTask;
        }
        
        async Task<bool> IUsuarioRepository.EmailExistsAsync(string email, CancellationToken ct)
        {
            return await _context.Usuarios.AnyAsync(u => u.Email == email, ct);
        }

        async Task<int> IUsuarioRepository.SaveChangesAsync(CancellationToken ct)
        {
            return await _context.SaveChangesAsync(ct);
        }
    }
}