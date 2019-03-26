using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Solidcon.TelegramBotApi.Models;
using Solidcon.TelegramBotApi.Servicos;

namespace Solidcon.TelegramBotApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUsuarioService _service;

        public LoginController(IUsuarioService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Post([FromBody] Usuario model)
        {
            var usuario = _service.Autentica(model.Nome, model.Senha);
            if (usuario == null)
                return BadRequest("Usuário ou senha está incorreta");

            return Ok(usuario);

        }
    }

}