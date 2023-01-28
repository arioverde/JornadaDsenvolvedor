using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiPonto.Controllers
{
    [ApiController]
    public class LiderancasController : ControllerBase
    {
        [HttpGet("lideranca")]
        public IActionResult Listar()
        {
            return StatusCode(200);
        }
    }
}
