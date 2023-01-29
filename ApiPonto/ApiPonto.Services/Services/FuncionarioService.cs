using ApiPonto.Models.Exceptions;
using ApiPonto.Models.Models;
using ApiPonto.Repositories.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            if (model is null)
                throw new ValidacaoException("Json mal formatado ou vazio.");

            if (string.IsNullOrWhiteSpace(model.NomeDoFuncionario))
                throw new ValidacaoException("O nome do funcionáio é obrigatório.");

            if (model.NomeDoFuncionario.Trim().Length < 3 || model.NomeDoFuncionario.Trim().Length > 255)
                throw new ValidacaoException("O nome do funcionáio deve possuir entre 3 e 255 caracteres.");

            model.NomeDoFuncionario = model.NomeDoFuncionario.Trim();

            if (string.IsNullOrWhiteSpace(model.Cpf))
                throw new ValidacaoException("O Cpf do funcionário é obrigatório.");

            if (!(long.TryParse(model.Cpf, out _) && model.Cpf.Trim().Length == 11))
                throw new ValidacaoException("O Cpf deve conter 11 números sem máscara.");

            if (!(ValidarCPF(model.Cpf)))
                throw new ValidacaoException("CpfCliente é inválido.");

            if (model.NascimentoFuncionario.Equals(DBNull.Value))
                throw new ValidacaoException("A data de nascimento deve ser informada.");

            if (model.DataDeAdmissao.Equals(DBNull.Value))
                throw new ValidacaoException("A data de admissão deve ser informada.");

            if (model.CelularFuncionario is not null)
            {
                if (model.CelularFuncionario.Trim().Length < 11
                 || model.CelularFuncionario.Trim().Length > 15
                 || model.CelularFuncionario.Trim().Length != RemoverMascaraTelefone(model.CelularFuncionario).Length)
                    throw new ValidacaoException("O telefone deve possuir entre 11 e 15 digitos e não pode ter máscaras.");
            }

            if (model.EmailFuncionario is null)
                throw new ValidacaoException("O e-mail deve ser informado.");

            if (!(ValidarEmail(model.EmailFuncionario)))
                throw new ValidacaoException("O e-mail é inválido.");

            if(model.CargoId == 0)
                throw new ValidacaoException("O identificador do cargo deve ser informado.");
        }
        private static string RemoverMascaraTelefone(string telefone)
        {
            return Regex.Replace(telefone, @"[^\d]", "");
        }
        public static bool ValidarCPF(string CPF)
        {
            string valor = CPF.Replace(".", "");
            valor = valor.Replace("-", "");

            if (valor.Length != 11)
                return false;

            bool igual = true;

            for (int i = 1; i < 11 && igual; i++)
                if (valor[i] != valor[0])
                    igual = false;

            if (igual || valor == "12345678909")
                return false;

            int[] numeros = new int[11];

            for (int i = 0; i < 11; i++)
                numeros[i] = int.Parse(valor[i].ToString());

            int soma = 0;

            for (int i = 0; i < 9; i++)
                soma += (10 - i) * numeros[i];

            int resultado = soma % 11;

            if (resultado == 1 || resultado == 0)
            {
                if (numeros[9] != 0)
                    return false;
            }
            else if (numeros[9] != 11 - resultado)
                return false;

            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += (11 - i) * numeros[i];

            resultado = soma % 11;

            if (resultado == 1 || resultado == 0)
            {
                if (numeros[10] != 0)
                    return false;
            }
            else
                if (numeros[10] != 11 - resultado)
                return false;

            return true;
        }

        public static bool ValidarEmail(string email)
        {
            if (new EmailAddressAttribute().IsValid(email))
                return true;
            else
                return false;
        }
    }
}