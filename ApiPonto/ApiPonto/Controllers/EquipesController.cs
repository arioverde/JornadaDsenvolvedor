using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiPonto.Controllers
{
    [ApiController]
    public class EquipesController : ControllerBase
    {
        [HttpGet("equipe")]
        public IActionResult Listar()
        {
            return StatusCode(200);
        }
    }
}
