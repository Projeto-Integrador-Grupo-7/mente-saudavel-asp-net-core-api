using MenteSaudavelAPI._02.Services.Interfaces.Services;
using MenteSaudavelAPI._04.Infrastructure.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MenteSaudavelAPI._01.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/questionarios")]
    public class QuestionarioController : ControllerBase
    {
        private readonly IQuestionarioService _questionarioService;

        public QuestionarioController(IQuestionarioService questionarioService)
        {
            _questionarioService = questionarioService;
        }

        [HttpGet("{questionarioId:Guid}")]
        public async Task<IActionResult> GetQuestionario(Guid questionarioId)
        {
            try
            {
                QuestionarioTO questionarioTO = await _questionarioService.GetQuestionario(questionarioId);

                return Ok(questionarioTO);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch
            {
                return StatusCode(500, "Ocorreu um erro ao submeter o questionário.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CriarQuestionario([FromBody] QuestionarioRequestTO requestTO)
        {
            try
            {
                List<RespostaTO> listaRespostasTO = requestTO.Respostas.Select(x => new RespostaTO
                {
                    Numero = int.Parse(x.Key.Substring(1)),
                    Valor = x.Value
                }).ToList();

                QuestionarioTO questionarioTO = new QuestionarioTO
                {
                    RespondenteId = requestTO.UsuarioId,
                    ListaRespostas = listaRespostasTO
                };

                questionarioTO = await _questionarioService.CriarQuestionario(questionarioTO);

                return Ok(questionarioTO);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch
            {
                return StatusCode(500, "Ocorreu um erro ao submeter o questionário.");
            }
        }

        [HttpPost("relatorio")]
        public async Task<IActionResult> GetUltimoQuestionarioRespondidoByUsuarioId([FromBody] Guid usuarioId)
        {
            try
            {
                QuestionarioTO ultimoQuestionarioRespondido = await _questionarioService.GetUltimoQuestionarioRespondidoByUsuarioId(usuarioId);

                return Ok(ultimoQuestionarioRespondido);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch
            {
                return StatusCode(500, "Ocorreu um erro ao buscar o último questionário respondido.");
            }
        }
    }
}