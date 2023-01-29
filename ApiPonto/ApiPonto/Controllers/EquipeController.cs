using ApiPonto.Models.Exceptions;
using ApiPonto.Models.Models;
using ApiPonto.Services.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiPonto.Controllers
{
    [ApiController]
    public class EquipeController : ControllerBase
    {
        private readonly EquipeService _service;
        public EquipeController()
        {
            _service = new EquipeService();
        }
        [HttpPost("equipe")]
        public IActionResult Inserir([FromBody] Equipe model)
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

        [HttpGet("equipe")]
        public IActionResult Listar()
        {
            return StatusCode(200, _service.Listar());
        }

        [HttpPut("equipe")]
        public IActionResult Atualizar([FromBody] Equipe model)
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

        [HttpDelete("equipe/{equipeId}")]
        public IActionResult Deletar([FromRoute] int equipeId)
        {
            try
            {
                _service.Deletar(equipeId);
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
