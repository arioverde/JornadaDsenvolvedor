using ApiTarefa.Models;
using ApiTarefa.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
                ValidarModel(model);
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

                if (_repositorio.SeExisteVinculo(Cnpj))
                    throw new ValidacaoException("Exclusão não autorizada. Existe tarefas registradas para esta empresa.");

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

        private static void ValidarModel(Empresa model)
        {
            if (model is null)
                throw new ValidacaoException("Jason mal formatado ou vazio.");

            model.RazaoSocial = model.RazaoSocial.Trim();
        }
    }
}
