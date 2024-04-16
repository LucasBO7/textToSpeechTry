using Microsoft.AspNetCore.Mvc;
using TextoPraFalaTeste.Repositories;

namespace TextoPraFalaTeste.Controllers
{
    public class Talking : Controller
    {
        private readonly Talk _talkText;

        public Talking()
        {
            _talkText = new Talk();
        }

        [HttpPost("FalarTexto")]
        public IActionResult FalarTexto(string textToTalk, string voiceAgentName)
        {
            try
            {
                var result = _talkText.SpeechText(textToTalk, voiceAgentName);
                return Ok("Funcionou!");
            }
            catch (Exception exc)
            {
                return BadRequest("Houve um erro: " + exc.Message);
            }
        }
    }
}
