using ApiPonto.Models.Exceptions;
using ApiPonto.Models.Models;
using ApiPonto.Services.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiPonto.Controllers
{
    [ApiController]
    public class PontoController : ControllerBase
    {
        private readonly PontoService _service;
        public PontoController()
        {
            _service = new PontoService();
        }
        [HttpPost("ponto")]
        public IActionResult Inserir([FromBody] Ponto model)
        {
            model.DataHorarioPonto = DateTime.Now;

            try
            {
                _service.Inserir(model);
                return StatusCode(201);
            }
            catch (ValidadaoException ex)
            {
                return StatusCode(400, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }

        [HttpGet("ponto")]
        public IActionResult Listar()
        {
            return StatusCode(200, _service.Listar());
        }

        [HttpPut("ponto")]
        public IActionResult Atualizar([FromBody] Ponto model)
        {
            try
            {
                _service.Atualizar(model);
                return StatusCode(201);
            }

            catch (ValidadaoException ex)
            {
                return StatusCode(400, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }

        [HttpDelete("ponto/{pontoId}")]
        public IActionResult Deletar([FromRoute] int pontoId)
        {
            try
            {
                _service.Deletar(pontoId);
                return StatusCode(200);
            }
            catch (ValidadaoException ex)
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
