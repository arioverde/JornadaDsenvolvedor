using ApiTarefa.Models;
using ApiTarefa.Repositories;
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
        public List<Tarefa> ListarTarefas()
        {
            try
            {
                _repositorio.AbrirConexao();
                return _repositorio.ListarTarefas();
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

            //if (string.IsNullOrWhiteSpace(model.RazaoSocial))
            //    throw new ValidacaoException("A razão social é obrigatória.");

            //if (model.RazaoSocial.Trim().Length < 3 || model.RazaoSocial.Trim().Length > 255)
            //    throw new ValidacaoException("A razão social deve possuir entre 3 e 255 caracteres.");

            //model.RazaoSocial = model.RazaoSocial.Trim();
        }
    }
}


