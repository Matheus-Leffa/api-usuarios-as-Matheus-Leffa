using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;


namespace Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repository;

        public UsuarioService(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<UsuarioReadDto>> ListarAsync(CancellationToken ct)
        {
            var usuarios = await _repository.GetAllAsync(ct);

            return usuarios.Select(user => new UsuarioReadDto(
                user.Id,
                user.Nome,
                user.Email,
                user.DataNascimento,
                user.Telefone,
                user.Ativo,
                user.DataCriacao
            ));
        }

        public async Task<UsuarioReadDto?> ObterAsync(int id, CancellationToken ct)
        {
            var usuario = await _repository.GetByIdAsync(id, ct);

            if(usuario is null) 
                return null;

            return new UsuarioReadDto(
                usuario.Id,
                usuario.Nome,
                usuario.Email,
                usuario.DataNascimento,
                usuario.Telefone,
                usuario.Ativo,
                usuario.DataCriacao
            );
        }

        public async Task<bool> EmailJaCadastradoAsync(string email, CancellationToken ct)
        {
            return await _repository.EmailExistsAsync(email, ct);
        }

        public async Task<UsuarioReadDto> CriarAsync(UsuarioCreateDto dto, CancellationToken ct)
        {
            var usuario = new Usuario
            {
                Nome = dto.Nome,
                Email = dto.Email.ToLower(),
                Senha = dto.Senha,
                DataNascimento = dto.DataNascimento,
                Telefone = dto.Telefone,
                Ativo = true,
                DataCriacao = DateTime.Now
            };

            await _repository.AddAsync(usuario, ct);
            await _repository.SaveChangesAsync(ct);

            return new UsuarioReadDto(
                usuario.Id,
                usuario.Nome,
                usuario.Email,
                usuario.DataNascimento,
                usuario.Telefone,
                usuario.Ativo,
                usuario.DataCriacao
            );
        }

        public async Task<UsuarioReadDto> AtualizarAsync(int id, UsuarioUpdateDto dto, CancellationToken ct)
        {
            var email = dto.Email.ToLower();
            var usuario = await _repository.GetByIdAsync(id, ct);

            if(usuario is null)
                throw new Exception($"O Usuario com o id {id} não foi encontrado!");

            usuario.Nome = dto.Nome;
            usuario.Email = email;
            usuario.DataNascimento = dto.DataNascimento;
            usuario.Telefone = dto.Telefone;
            usuario.Ativo = dto.Ativo;

            await _repository.UpdateAsync(usuario, ct);
            await _repository.SaveChangesAsync(ct);

            return new UsuarioReadDto(
                usuario.Id,
                usuario.Nome,
                usuario.Email,
                usuario.DataNascimento,
                usuario.Telefone,
                usuario.Ativo,
                usuario.DataCriacao
            );
        }

        public async Task<bool> RemoverAsync(int id, CancellationToken ct)
        {
            var usuario = await _repository.GetByIdAsync(id, ct);

            if(usuario is null) 
                return false;

            usuario.Ativo = false;

            await _repository.UpdateAsync(usuario, ct);
            await _repository.SaveChangesAsync(ct);

            return true;
        }
    }
}