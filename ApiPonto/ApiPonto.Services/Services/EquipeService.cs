using ApiPonto.Models.Models;
using ApiPonto.Repositories.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiPonto.Services.Services
{
    public class EquipeService
    {
        private readonly EquipeRepository _repository;
        public EquipeService()
        {
            _repository = new EquipeRepository();
        }
        public List<Equipe> Listar()
        {
            try
            {
                _repository.AbrirConexao();
                return _repository.ListarEquipes();
            }
            finally
            {
                _repository.FecharConexao();
            }
        }
        public Equipe? Obter(int EquipeId)
        {
            try
            {
                _repository.AbrirConexao();
                return _repository.Obter(EquipeId);
            }
            finally
            {
                _repository.FecharConexao();
            }
        }

        public void Atualizar(Equipe model)
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
        public void Deletar(int EquipeId)
        {
            try
            {
                _repository.AbrirConexao();
                _repository.Deletar(EquipeId);
            }
            finally
            {
                _repository.FecharConexao();
            }
        }
        public void Inserir(Equipe model)
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
        private static void ValidarModel(Equipe model)
        {
           // if (model is null)
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