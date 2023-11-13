namespace AppTurismoAPI.Services
{
    using AppTurismoAPI.DbContexts;
    using AppTurismoAPI.Entities;
    using AppTurismoAPI.Migrations;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.IdentityModel.Tokens;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;

    namespace rpgapi.Data
    {
        public class AuthRepository : IAuthRepository
        {
            private readonly AppTurismoContext _context;
            private readonly IConfiguration _configuration;

            public AuthRepository(AppTurismoContext context, IConfiguration configuration)
            {
                _context = context;
                _configuration = configuration;
            }

            public async Task<ServiceResposta<int>> Registrar(Usuario usuario, string senha)
            {
                var resposta = new ServiceResposta<int>();
                CriarSenhaHash(senha, out byte[] hash, out byte[] salt);

                usuario.SenhaSalt = salt;
                usuario.SenhaHash = hash;

                if (await UsuarioExiste(usuario.Username))
                {
                    resposta.Sucesso = false;
                    resposta.Mensagem = "Usuário já cadastrado!";
                    return resposta;
                }

                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();

                resposta.Dados = usuario.Id;

                return resposta;
            }

            public async Task<ServiceResposta<string>> Login(string username, string senha)
            {
                var resposta = new ServiceResposta<string>();
                var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Username.ToLower() == username.ToLower());

                if (usuario is null)
                {
                    resposta.Sucesso = false;
                    resposta.Mensagem = "Usuário não encontrado!";
                }
                else if (!VerificarHash(senha, usuario.SenhaHash, usuario.SenhaSalt))
                {
                    resposta.Sucesso = false;
                    resposta.Mensagem = "Senha incorreta";
                }
                else
                {
                    resposta.Dados = CriarToken(usuario);
                }

                return resposta;
            }

            public async Task<bool> UsuarioExiste(string username)
            {
                if (await _context.Usuarios.AnyAsync(u => u.Username.ToLower() == username.ToLower()))
                {
                    return true;
                }

                return false;
            }

            private void CriarSenhaHash(string senha, out byte[] hash, out byte[] salt)
            {
                using (var hmac = new System.Security.Cryptography.HMACSHA512())
                {
                    salt = hmac.Key;
                    hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(senha));
                }
            }

            private bool VerificarHash(string senha, byte[] hash, byte[] salt)
            {
                using (var hmac = new System.Security.Cryptography.HMACSHA512(salt))
                {
                    var HashComputado = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(senha));

                    return HashComputado.SequenceEqual(hash);
                }
            }

            private string CriarToken(Usuario usuario)
            {
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.Username)
            };

                var appSettingsToken = _configuration.GetSection("AppSettings:Token").Value;
                if (appSettingsToken is null) throw new Exception("Token inválido");


                SymmetricSecurityKey key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(appSettingsToken));

                SigningCredentials credenciais = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddDays(2),
                    SigningCredentials = credenciais
                };

                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);


                return tokenHandler.WriteToken(token);


            }

            public async Task<Usuario> DeletarUsuario(int id)
            {
                 var usuarioDeletado = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);

                if (usuarioDeletado is null)
                {
                    return null;
                }

                _context.Usuarios.Remove(usuarioDeletado);

                await _context.SaveChangesAsync();

                return usuarioDeletado;
            }

            public async Task<Usuario> GetUsuarioById(int id)
            {
                var usuarioPorId = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);

                if (usuarioPorId is null)
                {
                    return null;
                }

                return usuarioPorId;
            }
        }
    }

}
