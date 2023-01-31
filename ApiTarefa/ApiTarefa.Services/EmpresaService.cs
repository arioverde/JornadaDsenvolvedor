using ApiTarefa.Models;
using ApiTarefa.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ApiTarefa.Services
{
    public class EmpresaService
    {
        private readonly EmpresaRepositorio _repositorio;
        public EmpresaService(EmpresaRepositorio repositorio)
        {
            _repositorio = repositorio;
        }
        public List<Empresa> Listar(string? nome)
        {
            try
            {
                _repositorio.AbrirConexao();
                return _repositorio.ListarEmpresas(nome);
            }
            finally
            {
                _repositorio.FecharConexao();
            }
        }
        public Empresa? Obter(string Cnpj)
        {
            try
            {
                _repositorio.AbrirConexao();
                return _repositorio.Obter(Cnpj);
            }
            finally
            {
                _repositorio.FecharConexao();
            }
        }

        public void Atualizar(Empresa model)
        {
            try
            {
                ValidarModel(model, true);
                _repositorio.AbrirConexao();
                _repositorio.Atualizar(model);
            }
            finally
            {
                _repositorio.FecharConexao();
            }
        }
        public void Deletar(string Cnpj)
        {
            try
            {
                _repositorio.AbrirConexao();
                _repositorio.Deletar(Cnpj);
            }
            finally
            {
                _repositorio.FecharConexao();
            }
        }
        public void Inserir(Empresa model)
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
        private static void ValidarModel(Empresa model, bool isUpdate = false)
        {
            if (model is null)
                throw new ValidacaoException("Jason mal formatado ou vazio.");

            //if (!isUpdate)
            //{
            //    if (string.IsNullOrWhiteSpace(model.Cnpj))
            //        throw new ValidacaoException("O Cnpj da empresa é obrigatório.");

            //    if (!(long.TryParse(model.Cnpj, out _) && model.Cnpj.Trim().Length == 14))
            //        throw new ValidacaoException("O Cnpj deve conter 14 números sem máscara.");

            //    //if (!(ValidarCnpJ(model.Cnpj)))
            //    //    throw new ValidacaoException("O Cnpj da empresa é inválido.");
            //}

            //if (string.IsNullOrWhiteSpace(model.RazaoSocial))
            //    throw new ValidacaoException("A razão social é obrigatória.");

            //if (model.RazaoSocial.Trim().Length < 3 || model.RazaoSocial.Trim().Length > 255)
            //    throw new ValidacaoException("A razão social deve possuir entre 3 e 255 caracteres.");

            //model.RazaoSocial = model.RazaoSocial.Trim();
        }
    }
}
