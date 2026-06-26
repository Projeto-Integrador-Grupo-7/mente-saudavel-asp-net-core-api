using MenteSaudavelAPI._02.Services.Interfaces.Services;
using MenteSaudavelAPI._03.Data.ValueObjects;
using MenteSaudavelAPI._04.Infrastructure.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace MenteSaudavelAPI._01.API.Controllers
{
    [ApiController]
    [Route("api/usuarios")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [AllowAnonymous]
        [EnableRateLimiting("login")]
        [HttpPost("login")]
        public async Task<IActionResult> ValidarLogin([FromBody] Dictionary<string, string> dados)
        {
            try
            {
                UsuarioTO usuarioTO = new UsuarioTO
                {
                    Email = new Email(dados["email"]),
                    Senha = dados["senha"]
                };

                LoginRespostaTO resposta = await _usuarioService.ValidarLogin(usuarioTO);

                return Ok(resposta);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Mensagem = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Mensagem = "Ocorreu um erro ao validar email e senha." });
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUsuarios()
        {
            try
            {
                List<UsuarioTO> listaUsuario = await _usuarioService.GetUsuarios();

                if (!listaUsuario.Any())
                {
                    return NotFound("Nenhum usuário encontrado.");
                }

                return Ok(listaUsuario);
            }
            catch
            {
                return StatusCode(500, "Ocorreu um erro ao buscar os usuários.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CriarUsuario([FromBody] Dictionary<string, string> dados)
        {
            try
            {
                string dataNascimento = dados["dataNascimento"];
                string[] arrayDataNascimento = dataNascimento.Split('-');

                UsuarioTO usuarioTO = new UsuarioTO
                {
                    Nome = dados["nome"],
                    Email = new Email(dados["email"]),
                    Senha = dados["senha"],
                    DataNascimento = new DateOnly(int.Parse(arrayDataNascimento[0]), int.Parse(arrayDataNascimento[1]), int.Parse(arrayDataNascimento[2])),
                    Genero = new Genero(char.Parse(dados["sexo"]))
                };

                usuarioTO = await _usuarioService.CriarUsuario(usuarioTO);

                return Ok(usuarioTO);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Mensagem = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Mensagem = "Ocorreu um erro ao tentar cadastrar o usuário." });
            }
        }
    }   
}