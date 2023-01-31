using ApiTarefa.Models;
using ApiTarefa.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiTarefa.Controllers
{
    [AllowAnonymous]
    [ApiController]
    public class AutorizacaoController : ControllerBase
    {
        private readonly AutorizacaoService _service;
        public AutorizacaoController(AutorizacaoService service)
        {
            _service = service;
        }
        [HttpPost("Autorizacao")]
        public IActionResult Login(Usuario model)
        {
            try
            {
                var token = _service.Login(model);
                return StatusCode(200, token);
            }
            catch (Exception)
            {
                return StatusCode(401);
            }
        }
    }
}
