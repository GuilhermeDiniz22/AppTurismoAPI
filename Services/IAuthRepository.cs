using AppTurismoAPI.Entities;

namespace AppTurismoAPI.Services
{
    public interface IAuthRepository
    {
        Task<ServiceResposta<int>> Registrar(Usuario usuario, string senha);

        Task<ServiceResposta<string>> Login(string username, string senha);

        Task<Usuario> DeletarUsuario(int id);

        Task<Usuario> GetUsuarioById(int id);

        Task<bool> UsuarioExiste(string username);

    }
}
