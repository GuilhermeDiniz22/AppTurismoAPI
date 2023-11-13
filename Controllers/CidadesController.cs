using AppTurismoAPI.Models;
using Microsoft.AspNetCore.Mvc;
using AppTurismoAPI.Services;
using AutoMapper;
using AppTurismoAPI.Entities;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;

namespace AppTurismoAPI.Controllers
{
    [Authorize]
    [Route("api/cidades")]
    [ApiController]
    public class CidadesController : ControllerBase
    {
        private readonly CidadesDados _cidadesDados;
        private readonly IAppTurismoRepository _appTurismoRepository;
        private readonly IMapper _mapper;

        const int maxTamanhoPagina = 20;

        public CidadesController(IAppTurismoRepository appTurismoRepository, IMapper mapper)
        {
            _appTurismoRepository = appTurismoRepository ?? throw new ArgumentNullException(nameof(appTurismoRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CidadeSemPontoTuristicoDto>>> GetCidades(string? nome, string? filtro, int paginaNumero = 1, int paginaTamanho = 10)
        {
            if(paginaTamanho > maxTamanhoPagina)
            {
                paginaTamanho = maxTamanhoPagina;
            }

            var (cidades, metadados) = await _appTurismoRepository.GetCidadesAsync(nome, filtro, paginaNumero, paginaTamanho);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metadados));
           
            return Ok(_mapper.Map<IEnumerable<CidadeSemPontoTuristicoDto>>(cidades));
        } 

        [HttpGet("api/cidades/{id}")]
        public async Task<IActionResult> GetCidade(int id, bool incluirPontoTuristico = false)
        {
            var cidade = await _appTurismoRepository.GetCidadeAsync(id, incluirPontoTuristico);

            if(cidade is null)
            {
                return NotFound("Cidade não encontrada.");
            }

            if (incluirPontoTuristico is true)
            {
                return Ok(_mapper.Map<CidadeDto>(cidade));
            }

            return Ok(_mapper.Map<CidadeSemPontoTuristicoDto>(cidade));
        }

        [HttpPost]
        public async Task<IActionResult> AddCidade(CidadePostDto cidade)
        {
            var cidadeAdd = _mapper.Map<Cidade>(cidade);

            await _appTurismoRepository.AddCidadeAsync(cidadeAdd);

            await _appTurismoRepository.Salvar();

            var entidadeMapeada = _mapper.Map<CidadeDto>(cidadeAdd);

            return Ok("Cidade Adicionada");

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCidade(int cidadeId, CidadePutDto cidade, bool incluirPontoTuristico = false)
        {
            var CidadeDestino = await _appTurismoRepository.GetCidadeAsync(cidadeId, incluirPontoTuristico);

            if (CidadeDestino is null)
            {
                return NotFound("Cidade não encontrada.");
            }

            _mapper.Map(cidade, CidadeDestino);

            await _appTurismoRepository.Salvar();

            return Ok("Cidade Atualizada");

        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCidade(int id, bool incluirPontoTuristico = false)
        {
            var cidade = await _appTurismoRepository.GetCidadeAsync(id, incluirPontoTuristico);

            if (cidade is null)
            {
                return NotFound("Cidade não encontrada.");
            }

            _appTurismoRepository.DeletarCidade(cidade);

            await _appTurismoRepository.Salvar();

            return Ok("Cidade Deletada com sucesso");
        }

    }
}
