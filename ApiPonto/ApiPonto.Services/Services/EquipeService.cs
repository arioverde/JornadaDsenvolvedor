using ApiPonto.Models.Exceptions;
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
           if (model is null)
            throw new ValidacaoException("Json mal formatado ou vazio.");

            if (!(model.LiderancaId == 0))
              throw new ValidacaoException("O identificador da liderança deve ser informado.");

            if (!(model.FuncionarioId == 0))
                throw new ValidacaoException("O identificador do funcionário deve ser informado.");
        }
    }
}