using ApiPonto.Models.Exceptions;
using ApiPonto.Models.Models;
using ApiPonto.Services.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiPonto.Controllers
{
    [ApiController]
    public class CargoController : ControllerBase
    {
        private readonly CargoService _service;
        public CargoController()
        {
            _service = new CargoService();
        }
        [HttpPost("cargo")]
        public IActionResult Inserir([FromBody] Cargo model)
        {
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

        [HttpGet("cargo")]
        public IActionResult Listar()
        {
            return StatusCode(200, _service.Listar());
        }

        [HttpPut("cargo")]
        public IActionResult Atualizar([FromBody] Cargo model)
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

        [HttpDelete("cargo/{cargoId}")]
        public IActionResult Deletar([FromRoute] int cargoId)
        {
            try
            {
                _service.Deletar(cargoId);
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
