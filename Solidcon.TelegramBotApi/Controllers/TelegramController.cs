using Microsoft.AspNetCore.Mvc;
using Solidcon.TelegramBotApi.Servicos;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;


namespace Solidcon.TelegramBotApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    
    public class TelegramController : ControllerBase
    {

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get() => Ok("Solidcon Informática");

        [HttpPost]
        public IActionResult Post([FromBody] MensagemViewModel model)
        {
            Task.Run(async () => await TelegramBotService.Botclient.SendTextMessageAsync(model.ChatId, model.Mensagem));

            return Ok("Mensagem Enviada com sucesso");
        }


    }

    public class MensagemViewModel
    {
        public long ChatId { get; set; }
        public string Mensagem { get; set; }
    }
}