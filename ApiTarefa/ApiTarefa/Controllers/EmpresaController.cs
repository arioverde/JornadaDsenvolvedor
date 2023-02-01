using ApiTarefa.Models;
using ApiTarefa.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiTarefa.Controllers
{
    [AllowAnonymous]
    [Authorize(Roles = "2")]
    [ApiController]
    public class EmpresaController : ControllerBase
    {
        private readonly EmpresaService _service;
        public EmpresaController(EmpresaService service)
        {
            _service = service;
        }

        [HttpGet("empresa")]
        public IActionResult Listar([FromQuery] string? nome)
        {
            return StatusCode(200, _service.Listar(nome));
        }

        [HttpGet("empresa/{cnpj}")]
        public IActionResult Obter([FromRoute] string cnpj)
        {
            return StatusCode(200, _service.Obter(cnpj));
        }

        [HttpPost("empresa")]
        public IActionResult Inserir([FromBody] Empresa model)
        {
            try
            {
                _service.Inserir(model);
                return StatusCode(201);
            }
            catch (ValidacaoException ex)
            {
                return StatusCode(400, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }

        [HttpDelete("empresa/{cnpj}")]
        public IActionResult Deletar([FromRoute] string cnpj)
        {
            try
            {
                _service.Deletar(cnpj);
                return StatusCode(200);
            }
            catch (ValidacaoException ex)
            {
                return StatusCode(400, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }

        }

        [HttpPut("empresa")]
        public IActionResult Atualizar([FromBody] Empresa model)
        {
            try
            {
                _service.Atualizar(model);
                return StatusCode(201);
            }

            catch (ValidacaoException ex)
            {
                return StatusCode(400, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }
    }
}



