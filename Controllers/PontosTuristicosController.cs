using AppTurismoAPI.Entities;
using AppTurismoAPI.Models;
using AppTurismoAPI.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace AppTurismoAPI.Controllers
{
    [Authorize]
    [Route("api/cidades/{cidadeId}/pontosturisticos")]
    [ApiController]
    public class PontosTuristicosController : ControllerBase
    {
        private readonly ILogger<PontosTuristicosController> _logger; //injeção de dependencia do logger, serviço de email, Repositorio do App e Imapper
        private readonly IEmailService _emailService;
        private readonly IAppTurismoRepository _appTurismoRepository;
        private readonly IMapper _mapper;

        public PontosTuristicosController(ILogger<PontosTuristicosController> logger, IEmailService emailService, IAppTurismoRepository appTurismoRepository, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
            _appTurismoRepository = appTurismoRepository ?? throw new ArgumentNullException(nameof(appTurismoRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PontosTuristicosDto>>> GetPontosTuristicos(int cidadeId)
        {
            if (!await _appTurismoRepository.CidadeExisteAsync(cidadeId)) // se cidade n existe no repositório retorna false
            {
                _logger.LogInformation($"Cidade com id: {cidadeId} não foi encontrada.");
                return NotFound("Cidade não encontrada");
            }
            var pontosTuriticos = await _appTurismoRepository.GetPontoTuristicoAsync(cidadeId);

            return Ok(_mapper.Map<IEnumerable<PontosTuristicosDto>>(pontosTuriticos));
        }

        [HttpGet("{pontoTuristicoId}", Name = "GetPontoTuristico")]
        public async Task<ActionResult<PontosTuristicosDto>> GetPontoTuristico(int cidadeId, int pontoTuristicoId)
        {
            if (!await _appTurismoRepository.CidadeExisteAsync(cidadeId))
            {
                return NotFound("Cidade não encontrada.");
            }

            var pontoTuristico = await _appTurismoRepository.GetPontoTuristicoCidadeAsync(cidadeId, pontoTuristicoId);

            if (pontoTuristico is null)
            {
                return NotFound("Ponto Turistico não encontrado");
            }

            return Ok(_mapper.Map<PontosTuristicosDto>(pontoTuristico));
        }

        [HttpPost]
        public async Task<ActionResult<PontosTuristicosDto>> CriarPontoTuristico(int cidadeId, PontoTuristicoPostDto pontoTuristico)
        {

            if(!await _appTurismoRepository.CidadeExisteAsync(cidadeId))
            {
                return NotFound("Cidade não encontrada.");
            }

            var pontoTuristicoFinal = _mapper.Map<PontoTuristico>(pontoTuristico);

            await _appTurismoRepository.AddPontoTuristico(cidadeId, pontoTuristicoFinal);

            await _appTurismoRepository.Salvar();

            var entidadeMapeada = _mapper.Map<PontosTuristicosDto>(pontoTuristicoFinal);

            
            return CreatedAtRoute("GetPontoTuristico",
                new
                {
                    cidadeId = cidadeId,
                    pontoTuristicoId = entidadeMapeada.Id,
                },
                entidadeMapeada);
        }

        [HttpPut("{pontoTuristicoId}")]
                                  
        public async Task<ActionResult> AtualizarPontoTuristico(int cidadeId, int pontoTuristicoId, PontoTuristicoPutDto pontoTuristico)
        {
            if (!await _appTurismoRepository.CidadeExisteAsync(cidadeId))
            {
                return NotFound("Cidade não encontrada.");
            }

            var PontoTuristicoDestino = await _appTurismoRepository.GetPontoTuristicoCidadeAsync(cidadeId, pontoTuristicoId);
            if (PontoTuristicoDestino is null)
            {
                return NotFound("Ponto Turistico não encontrado.");
            }

            _mapper.Map(pontoTuristico, PontoTuristicoDestino);

            await _appTurismoRepository.Salvar();

            return NoContent();
        }

        [HttpPatch("{pontoTuristicoId}")]
        public async Task<ActionResult> UpdateParcialPontosTuristicos(int cidadeId, int pontoTuristicoId, JsonPatchDocument<PontoTuristicoPutDto> patchDocument)
        {

            if (!await _appTurismoRepository.CidadeExisteAsync(cidadeId))
            {
                return NotFound("Cidade não encontrada.");
            }

            var PontoTuristicoDestino = await _appTurismoRepository.GetPontoTuristicoCidadeAsync(cidadeId, pontoTuristicoId);
            if (PontoTuristicoDestino is null)
            {
                return NotFound("Ponto Turistico não encontrado.");
            }

            var pontoTuristicoParaAtualizar = _mapper.Map<PontoTuristicoPutDto>(PontoTuristicoDestino);

            patchDocument.ApplyTo(pontoTuristicoParaAtualizar, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!TryValidateModel(pontoTuristicoParaAtualizar))
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(pontoTuristicoParaAtualizar, PontoTuristicoDestino);

            await _appTurismoRepository.Salvar();

            return NoContent();
        }

        [HttpDelete("{pontoTuristicoId}")]
        public async Task<ActionResult> DeletarPontoTuristico(int cidadeId, int pontoTuristicoId)
        {
            if (!await _appTurismoRepository.CidadeExisteAsync(cidadeId))
            {
                return NotFound("Cidade não encontrada.");
            }

            var PontoTuristicoDestino = await _appTurismoRepository.GetPontoTuristicoCidadeAsync(cidadeId, pontoTuristicoId);
            if (PontoTuristicoDestino is null)
            {
                return NotFound("Ponto Turistico não encontrado.");
            }

            _appTurismoRepository.DeletarPontoTuristico(PontoTuristicoDestino);

            await _appTurismoRepository.Salvar();

            _emailService.EnviarEmail("Ponto turistico deletado.", $"Ponto turistico {PontoTuristicoDestino.Nome} com id {PontoTuristicoDestino.Id} foi deletado");
            return NoContent();
        }
    }
}








