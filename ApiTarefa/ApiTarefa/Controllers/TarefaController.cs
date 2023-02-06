using ApiTarefa.Models;
using ApiTarefa.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiTarefa.Controllers
{
    [AllowAnonymous]
    [ApiController]
    public class TarefaController : ControllerBase
    {
        private readonly TarefaService _service;
        public TarefaController(TarefaService service)
        {
            _service = service;
        }

        [HttpGet("tarefa")]
        public IActionResult Listar([FromQuery] int tarefasPorPeriodo, string? razaoSocial, string? nomeColaborador)
        {
            return StatusCode(200, _service.ListarTarefas(tarefasPorPeriodo, razaoSocial, nomeColaborador));
        }

        [Authorize(Roles = "1")]
        [HttpGet("tarefa/{identificadorTarefa}")]
        public IActionResult Obter([FromRoute] int identificadorTarefa)
        {
            return StatusCode(200, _service.Obter(identificadorTarefa));
        }

        [Authorize(Roles = "1")]
        [HttpPost("tarefa")]
        public IActionResult Inserir([FromBody] Tarefa model)
        {
            model.HorarioInicio = DateTime.Now;
            model.HorarioFim = null;

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

        [Authorize(Roles = "1")]
        [HttpDelete("tarefa/{identificadorTarefa}")]
        public IActionResult Deletar([FromRoute] int identificadorTarefa)
        {
            try
            {
                _service.Deletar(identificadorTarefa);
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

        [Authorize(Roles = "1")]
        [HttpPut("tarefa")]
        public IActionResult Atualizar([FromBody] Tarefa model)
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
        [Authorize(Roles = "1")]
        [HttpPut("tarefa/{identificadorTarefa}")]
        public IActionResult Finalizar([FromRoute] int identificadorTarefa)
        {
            try
            {
                _service.Finalizar(identificadorTarefa);
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
