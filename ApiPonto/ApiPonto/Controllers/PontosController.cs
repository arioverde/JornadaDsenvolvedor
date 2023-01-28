using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiPonto.Controllers
{
    [ApiController]
    public class PontosController : ControllerBase
    {
        [HttpGet("ponto")]
        public IActionResult Listar()
        {
            return StatusCode(200);
        }
    }
}
