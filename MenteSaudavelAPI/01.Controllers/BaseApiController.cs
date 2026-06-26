using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace MenteSaudavelAPI._01.API.Controllers
{
    /// <summary>
    /// Base dos controllers autenticados. Expõe o id do usuário a partir do token
    /// (claim), de modo que os endpoints nunca confiem em um usuarioId vindo do corpo.
    /// </summary>
    [ApiController]
    public abstract class BaseApiController : ControllerBase
    {
        protected Guid UsuarioIdAutenticado
        {
            get
            {
                string? id = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (!Guid.TryParse(id, out Guid usuarioId))
                {
                    throw new ArgumentException("Token sem identificador de usuário válido.");
                }

                return usuarioId;
            }
        }
    }
}
