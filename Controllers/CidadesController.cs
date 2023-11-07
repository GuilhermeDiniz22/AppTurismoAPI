using AppTurismoAPI.Models;
using Microsoft.AspNetCore.Mvc;
using AppTurismoAPI.Services;
using AutoMapper;
using AppTurismoAPI.Entities;

namespace AppTurismoAPI.Controllers
{
    public class CidadesController : ControllerBase
    {
        private readonly CidadesDados _cidadesDados;
        private readonly IAppTurismoRepository _appTurismoRepository;
        private readonly IMapper _mapper;

        public CidadesController(IAppTurismoRepository appTurismoRepository, IMapper mapper)
        {
            _appTurismoRepository = appTurismoRepository ?? throw new ArgumentNullException(nameof(appTurismoRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("api/cidades")]
        public async Task<ActionResult<IEnumerable<CidadeSemPontoTuristicoDto>>> GetCidades(string? nome)
        {
            var cidades = await _appTurismoRepository.GetCidadesAsync(nome);
           
            return Ok(_mapper.Map<IEnumerable<CidadeSemPontoTuristicoDto>>(cidades));
        } 

        [HttpGet("api/cidade/{id}")]
        public async Task<IActionResult> GetCidade(int id, bool incluirPontoTuristico = false)
        {
            var cidade = await _appTurismoRepository.GetCidadeAsync(id, incluirPontoTuristico);

            if(cidade is null)
            {
                return NotFound("Cidade não encontrada.");
            }

            if (incluirPontoTuristico)
            {
                return Ok(_mapper.Map<CidadeDto>(cidade));
            }

            return Ok(_mapper.Map<CidadeSemPontoTuristicoDto>(cidade));
        }
    }
}
