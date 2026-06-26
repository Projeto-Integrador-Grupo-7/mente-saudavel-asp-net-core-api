using MenteSaudavelAPI._02.Services.Interfaces.Services;
using MenteSaudavelAPI._04.Infrastructure.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MenteSaudavelAPI._01.API.Controllers
{
    [Authorize]
    [Route("api/questionarios")]
    public class QuestionarioController : BaseApiController
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

                if (questionarioTO.RespondenteId != UsuarioIdAutenticado)
                {
                    return Forbid();
                }

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
                    RespondenteId = UsuarioIdAutenticado,
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
        public async Task<IActionResult> GetUltimoQuestionarioRespondidoByUsuarioId()
        {
            try
            {
                QuestionarioTO ultimoQuestionarioRespondido = await _questionarioService.GetUltimoQuestionarioRespondidoByUsuarioId(UsuarioIdAutenticado);

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