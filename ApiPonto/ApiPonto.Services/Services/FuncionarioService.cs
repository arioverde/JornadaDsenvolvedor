using ApiPonto.Models.Models;
using ApiPonto.Repositories.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiPonto.Services.Services
{
    public class FuncionarioService
    {
        private readonly FuncionarioRepository _repository;
        public FuncionarioService()
        {
            _repository = new FuncionarioRepository();
        }
        public List<Funcionario> Listar(string? nome)
        {
            try
            {
                _repository.AbrirConexao();
                return _repository.ListarFuncionarios(nome);
            }
            finally
            {
                _repository.FecharConexao();
            }
        }
        public Funcionario? Obter(int FuncionarioId)
        {
            try
            {
                _repository.AbrirConexao();
                return _repository.Obter(FuncionarioId);
            }
            finally
            {
                _repository.FecharConexao();
            }
        }

        public void Atualizar(Funcionario model)
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
        public void Deletar(int FuncionarioId)
        {
            try
            {
                _repository.AbrirConexao();
                _repository.Deletar(FuncionarioId);
            }
            finally
            {
                _repository.FecharConexao();
            }
        }
        public void Inserir(Funcionario model)
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
        private static void ValidarModel(Funcionario model)
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