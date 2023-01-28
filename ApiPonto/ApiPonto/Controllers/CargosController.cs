using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiPonto.Controllers
{
    [ApiController]
    public class CargosController : ControllerBase
    {
        [HttpGet("cargo")]
        public IActionResult Listar()
        {
            return StatusCode(200);
        }
    }
}
