using MenteSaudavelAPI._02.Services.Interfaces.Services;
using MenteSaudavelAPI._04.Infrastructure.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MenteSaudavelAPI._01.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/dashboard")]
    public class DashboardController : ControllerBase
    {
        private readonly IQuestionarioService _questionarioService;

        public DashboardController(IQuestionarioService questionarioService)
        {
            _questionarioService = questionarioService;
        }

        [HttpPost("historico")]
        public async Task<IActionResult> GetQuestionariosRespondidosByUsuarioId([FromBody] DashboardRequestTO requestTO)
        {
            try
            {
                List<QuestionarioTO> listaQuestionariosRespondidos = await _questionarioService.GetQuestionariosByUsuarioId(requestTO);

                return Ok(listaQuestionariosRespondidos);
            }
            catch
            {
                return StatusCode(500, "Ocorreu um erro ao buscar o histórico de questionários respondidos.");
            }
        }

        [HttpPost("graficoPizza")]
        public async Task<IActionResult> GetQtdeUsuariosPorEstratificacao([FromBody] DashboardRequestTO requestTO)
        {
            try
            {
                DashboardTO dashboardTO = (DashboardTO)requestTO;

                Dictionary<string, int> qtdeUsuariosPorEstratificacao = await _questionarioService.GetQtdeUsuariosPorEstratificacao(dashboardTO);

                return Ok(qtdeUsuariosPorEstratificacao);
            }
            catch
            {
                return StatusCode(500, "Ocorreu um erro ao buscar os questionários mais recentes de cada usuário.");
            }
        }
    }
}
