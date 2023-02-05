using ApiTarefa.Models;
using ApiTarefa.Repositories;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiTarefa.Services
{
    public class TarefaService
    {
        private readonly TarefaRepositorio _repositorio;
        public TarefaService(TarefaRepositorio repositorio)
        {
            _repositorio = repositorio;
        }
        public List<Tarefa> ListarTarefas(int tarefasPorPeriodo, string razaoSocial, string nomeColaborador)
        {
            try
            {
                _repositorio.AbrirConexao();

                if (tarefasPorPeriodo == 0)
                {
                    if (razaoSocial is null && nomeColaborador is null)
                    {
                        return _repositorio.ListarTarefas();
                    }
                }

                var tarefasFiltradas = AplicarFiltros(tarefasPorPeriodo, razaoSocial, nomeColaborador);
                return tarefasFiltradas;
            }

            finally
            {
                _repositorio.FecharConexao();
            }
        }

        public Tarefa? Obter(int IdentificadorTarefa)
        {
            try
            {
                _repositorio.AbrirConexao();
                return _repositorio.Obter(IdentificadorTarefa);
            }
            finally
            {
                _repositorio.FecharConexao();
            }
        }

        public void Atualizar(Tarefa model)
        {
            try
            {
                ValidarModel(model);
                _repositorio.AbrirConexao();
                _repositorio.Atualizar(model);
            }
            finally
            {
                _repositorio.FecharConexao();
            }
        }
        public void Deletar(int IdentificadorTarefa)
        {
            try
            {
                _repositorio.AbrirConexao();
                _repositorio.Deletar(IdentificadorTarefa);
            }
            finally
            {
                _repositorio.FecharConexao();
            }
        }
        public void Inserir(Tarefa model)
        {


            try
            {
                ValidarModel(model);
                _repositorio.AbrirConexao();
                _repositorio.Inserir(model);
            }
            finally
            {
                _repositorio.FecharConexao();
            }
        }
        public void Finalizar(int IdentificadorTarefa)
        {
            try
            {
                _repositorio.AbrirConexao();
                _repositorio.Finalizar(IdentificadorTarefa);
            }
            finally
            {
                _repositorio.FecharConexao();
            }
        }

        private static void ValidarModel(Tarefa model)
        {
            if (model is null)
                throw new ValidacaoException("Jason mal formatado ou vazio.");
        }
        public List<Tarefa> AplicarFiltros(int tarefasPorPeriodo, string razaoSocial, string nomeColaborador)
        {
            List<Tarefa> listaAuxiliar = new List<Tarefa>();
            var tarefasFiltradas = _repositorio.ListarTarefas();

            if (tarefasPorPeriodo != 0)
            {
                if (tarefasPorPeriodo == 1)
                {
                    foreach (var tarefa in tarefasFiltradas)
                    {
                        if (tarefa.HorarioInicio.Day == DateTime.Now.Day && tarefa.HorarioInicio.Month == DateTime.Now.Month)
                            listaAuxiliar.Add(tarefa);
                    }
                }

                if (tarefasPorPeriodo == 2)
                {
                    //foreach (var tarefa in tarefasFiltradas)
                    //{
                    //    if (tarefa.HorarioInicio.Month == DateTime.Now.Month && tarefa.HorarioInicio.Year == DateTime.Now.Year)
                    //        listaAuxiliar.Add(tarefa);
                    //}
                }

                if (tarefasPorPeriodo == 3)
                {
                    foreach (var tarefa in tarefasFiltradas)
                    {
                        if (tarefa.HorarioInicio.Month == DateTime.Now.Month && tarefa.HorarioInicio.Year == DateTime.Now.Year)
                            listaAuxiliar.Add(tarefa);
                    }
                }

                tarefasFiltradas = listaAuxiliar;
            }

            if (razaoSocial != null)
            {

                foreach (var tarefa in tarefasFiltradas.ToList())
                {
                    if (tarefa.RazaoSocial == razaoSocial)
                        continue;
                    else
                        tarefasFiltradas.Remove(tarefa);
                }
            }

            if (nomeColaborador != null)
            {
                foreach (var tarefa in tarefasFiltradas.ToList())
                {
                    if (tarefa.NomeColaborador == nomeColaborador)
                        continue;
                    else
                        tarefasFiltradas.Remove(tarefa);
                }
            }

            CalcularTempoTarefa(tarefasFiltradas);
            return tarefasFiltradas;
        }
        private List<Tarefa> CalcularTempoTarefa(List<Tarefa> tarefasFiltradas)
        {
            foreach (var tarefa in tarefasFiltradas)
            {
                if (tarefa.HorarioFim == null)
                    tarefa.HorarioFim = DateTime.Now;

                tarefa.TempoTarefa = (TimeSpan)(tarefa.HorarioFim - tarefa.HorarioInicio);
            }

            // SomarTotalHoras(tarefasFiltradas);
            return tarefasFiltradas;

        }
        //private List<Tarefa> SomarTotalHoras(List<Tarefa> tarefasFiltradas)
        //{
        //    TimeSpan totalHoras = new TimeSpan();

        //    foreach (var tarefa in tarefasFiltradas)
        //    {
        //        totalHoras += tarefa.TempoTarefa;
        //    }

        //    return tarefasFiltradas;
        //}
    }
}


