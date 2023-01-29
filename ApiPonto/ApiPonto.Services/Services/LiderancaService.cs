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
            if (model is null)
                throw new ValidacaoException("Json mal formatado ou vazio.");

            if (!(model.FuncionarioId == 0))
                throw new ValidacaoException("O identificador do funcionário deve ser informado.");

            if (string.IsNullOrWhiteSpace(model.DescricaoEquipe))
                throw new ValidacaoException("A descrição da equipe é obrigatória.");

            if (model.DescricaoEquipe.Trim().Length < 3 || model.DescricaoEquipe.Trim().Length > 255)
                throw new ValidacaoException("A descrição da equipe deve possuir entre 3 e 255 caracteres.");

            model.DescricaoEquipe = model.DescricaoEquipe.Trim();
        }
    }
}