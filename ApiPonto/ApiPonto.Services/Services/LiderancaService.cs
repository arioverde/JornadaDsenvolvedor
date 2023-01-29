using ApiPonto.Models.Models;
using ApiPonto.Repositories.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiPonto.Services.Services
{
    public class LiderancaService
    {
        private readonly LiderancaRepository _repository;
        public LiderancaService()
        {
            _repository = new LiderancaRepository();
        }
        public List<Lideranca> Listar()
        {
            try
            {
                _repository.AbrirConexao();
                return _repository.ListarLiderancas();
            }
            finally
            {
                _repository.FecharConexao();
            }
        }
        public Lideranca? Obter(int LiderancaId)
        {
            try
            {
                _repository.AbrirConexao();
                return _repository.Obter(LiderancaId);
            }
            finally
            {
                _repository.FecharConexao();
            }
        }

        public void Atualizar(Lideranca model)
        {
            try
            {
                ValidarModel(model);
                _repository.AbrirConexao();
                _repository.Atualizar(model);
            }
            finally
            {
                _repository.FecharConexao();
            }
        }
        public void Deletar(int LiderancaId)
        {
            try
            {
                _repository.AbrirConexao();
                _repository.Deletar(LiderancaId);
            }
            finally
            {
                _repository.FecharConexao();
            }
        }
        public void Inserir(Lideranca model)
        {
            try
            {
                ValidarModel(model);
                _repository.AbrirConexao();
                _repository.Inserir(model);
            }
            finally
            {
                _repository.FecharConexao();
            }
        }
        private static void ValidarModel(Lideranca model)
        {
            //if (model is null)
            //    throw new ValidadaoException("Jason mal formatado ou vazio.");

            //if (string.IsNullOrWhiteSpace(model.Nome))
            //    throw new ValidadaoException("O nome é obrigatório.");

            //if (model.Nome.Trim().Length < 3 || model.Nome.Trim().Length > 255)
            //    throw new ValidadaoException("O nome deve possuir entre 3 e 255 caracteres.");

            //if (!(model.Valor > 0))
            //    throw new ValidadaoException("O valor deve ser informado.");

            //model.Nome = model.Nome.Trim();

        }
    }
}