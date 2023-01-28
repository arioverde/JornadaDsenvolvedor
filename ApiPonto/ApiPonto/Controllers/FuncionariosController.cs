using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiPonto.Controllers
{
    [ApiController]
    public class FuncionariosController : ControllerBase
    {
        [HttpGet("funcionario")]
        public IActionResult Listar()
        {
            return StatusCode(200);
        }
    }
}
