using ApiPonto.Models.Exceptions;
using ApiPonto.Models.Models;
using ApiPonto.Services.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiPonto.Controllers
{
    [ApiController]
    public class LiderancaController : ControllerBase
    {
        private readonly LiderancaService _service;
        public LiderancaController()
        {
            _service = new LiderancaService();
        }
        [HttpPost("lideranca")]
        public IActionResult Inserir([FromBody] Lideranca model)
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

        [HttpGet("lideranca")]
        public IActionResult Listar()
        {
            return StatusCode(200, _service.Listar());
        }

        [HttpPut("lideranca")]
        public IActionResult Atualizar([FromBody] Lideranca model)
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

        [HttpDelete("lideranca/{liderancaId}")]
        public IActionResult Deletar([FromRoute] int liderancaId)
        {
            try
            {
                _service.Deletar(liderancaId);
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
    }
}
