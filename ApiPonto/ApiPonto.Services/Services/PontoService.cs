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
    public class PontoService
    {
        private readonly PontoRepository _repository;
        public PontoService()
        {
            _repository = new PontoRepository();
        }
        public List<Ponto> Listar()
        {
            try
            {
                _repository.AbrirConexao();
                return _repository.ListarPontos();
            }
            finally
            {
                _repository.FecharConexao();
            }
        }
        public Ponto? Obter(int PontoId)
        {
            try
            {
                _repository.AbrirConexao();
                return _repository.Obter(PontoId);
            }
            finally
            {
                _repository.FecharConexao();
            }
        }

        public void Atualizar(Ponto model)
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
        public void Deletar(int PontoId)
        {
            try
            {
                _repository.AbrirConexao();
                _repository.Deletar(PontoId);
            }
            finally
            {
                _repository.FecharConexao();
            }
        }
        public void Inserir(Ponto model)
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
        private static void ValidarModel(Ponto model)
        {
            if (model is null)
                throw new ValidacaoException("Json mal formatado ou vazio.");

            if (model.DataHorarioPonto.Equals(DBNull.Value))
                throw new ValidacaoException("A data e hora do ponto deve ser informada.");

            if (model.Justificativa is not null)
                if (model.Justificativa.Trim().Length < 3 || model.Justificativa.Trim().Length > 255)
                    throw new ValidacaoException("A justificativa deve possuir entre 3 e 255 caracteres.");

            if ((model.FuncionarioId == 0))
                throw new ValidacaoException("O identificador do funcionário deve ser informado.");
        }
    }
}