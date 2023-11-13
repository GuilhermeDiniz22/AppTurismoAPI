using Microsoft.AspNetCore.Mvc;
using AppTurismoAPI.Entities;
using AppTurismoAPI.Services;
using AppTurismoAPI.Models;

namespace rpgapi.Controllers
{
    [ApiController]
    [Route("controller")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepo;

        public AuthController(IAuthRepository authRepo)
        {
            _authRepo = authRepo;
        }

        [HttpPost("registrar")]
        public async Task<ActionResult<ServiceResposta<int>>> Registrar(RegistroUsuarioDto request)
        {
            var resposta = await _authRepo.Registrar(new Usuario { Username = request.Username }, request.Senha);

            if (!resposta.Sucesso)
            {
                return BadRequest(resposta);
            }

            return Ok(resposta);
        }

        [HttpPost("login")]
        public async Task<ActionResult<ServiceResposta<int>>> Login(LoginUsuarioDto request)
        {
            var resposta = await _authRepo.Login(request.Username, request.Senha);

            if (!resposta.Sucesso)
            {
                return BadRequest(resposta);
            }

            return Ok(resposta);
        }

        [HttpDelete("deletar/{id}")]
        public async Task<IActionResult> Deletar(int id)
        {
            var usuarioDeletado = await _authRepo.GetUsuarioById(id);

            if (usuarioDeletado is null)
            {
                return NotFound("Usuário não encontrado");
            }

            await _authRepo.DeletarUsuario(usuarioDeletado.Id);

            return Ok("Usuário Deletado com sucesso");
        }
    }
}

