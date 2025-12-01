using reserva_de_salas.Interfaces;
using reserva_de_salas.Models;

namespace reserva_de_salas.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }
        public async Task<Usuario> CreateAsync(Usuario usuario)
        {
            var existUser = await _usuarioRepository.GetByEmailAsync(usuario.Email);
            if(existUser != null)
            {
                throw new InvalidOperationException("Email já cadastrado");
            }

            await _usuarioRepository.AddAsync(usuario);
            await _usuarioRepository.SaveChangesAsync();
            return usuario;
        }

        public async Task DeleteAsync(long id)
        {
            var user = await _usuarioRepository.GetByIdAsync(id);
            if(user == null)
            {
                throw new InvalidOperationException("Usuário não encontrado");
            }

            _usuarioRepository.Delete(user);
            await _usuarioRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<Usuario>> GetAllAsync()
        {
            return await _usuarioRepository.GetAllAsync();
        }

        public async Task<Usuario> GetByIdAsync(long id)
        {
            return await _usuarioRepository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(Usuario usuario)
        {
            _usuarioRepository.Update(usuario);
            await _usuarioRepository.SaveChangesAsync();
        }
    }
}
