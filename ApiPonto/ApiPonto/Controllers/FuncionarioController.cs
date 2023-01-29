using ApiPonto.Models.Exceptions;
using ApiPonto.Models.Models;
using ApiPonto.Services.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiPonto.Controllers
{
    [ApiController]
    public class FuncionarioController : ControllerBase
    {
        private readonly FuncionarioService _service;
        public FuncionarioController()
        {
            _service = new FuncionarioService();
        }

        [HttpPost("funcionario")]
        public IActionResult Inserir([FromBody] Funcionario model)
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

        [HttpGet("funcionario")]
        public IActionResult Listar([FromQuery] string? nome)
        {
            return StatusCode(200, _service.Listar(nome));
        }

        [HttpPut("funcionario")]
        public IActionResult Atualizar([FromBody] Funcionario model)
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

        [HttpDelete("funcionario/{funcionarioId}")]
        public IActionResult Deletar([FromRoute] int funcionarioId)
        {
            try
            {
                _service.Deletar(funcionarioId);
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
