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
    public class CargoService
    {
        private readonly CargoRepository _repository;
        public CargoService()
        {
            _repository = new CargoRepository();
        }
        public List<Cargo> Listar()
        {
            try
            {
                _repository.AbrirConexao();
                return _repository.ListarCargos();
            }
            finally
            {
                _repository.FecharConexao();
            }
        }
        public Cargo? Obter(int CargoId)
        {
            try
            {
                _repository.AbrirConexao();
                return _repository.Obter(CargoId);
            }
            finally
            {
                _repository.FecharConexao();
            }
        }

        public void Atualizar(Cargo model)
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
        public void Deletar(int CargoId)
        {
            try
            {
                _repository.AbrirConexao();
                _repository.Deletar(CargoId);
            }
            finally
            {
                _repository.FecharConexao();
            }
        }
        public void Inserir(Cargo model)
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
        private static void ValidarModel(Cargo model)
        {
            if (model is null)
                throw new ValidacaoException("Json mal formatado ou vazio.");

            if (string.IsNullOrWhiteSpace(model.Descricao))
                throw new ValidacaoException("A descrição é obrigatória.");

            if (model.Descricao.Trim().Length < 3 || model.Descricao.Trim().Length > 255)
                throw new ValidacaoException("A descrição deve possuir entre 3 e 255 caracteres.");

            model.Descricao = model.Descricao.Trim();
        }
    }
}
