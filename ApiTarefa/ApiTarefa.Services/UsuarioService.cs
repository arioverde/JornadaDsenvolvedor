using ApiTarefa.Models;
using ApiTarefa.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiTarefa.Services
{
    public class UsuarioService
    {
        private readonly UsuarioRepositorio _repositorio;
        public UsuarioService(UsuarioRepositorio repositorio)
        {
            _repositorio = repositorio;
        }
        public Usuario? ObterUsuarioPorRedenciais(string email, string senha, bool isCriptografada = true)
        {
            try
            {
                _repositorio.AbrirConexao();

                if (isCriptografada)
                    senha = CriptografarSha512(senha);

                return _repositorio.ObterUsuarioPorRedenciais(email, senha);
            }
            finally
            {
                _repositorio.FecharConexao();
            }
        }
        private static string CriptografarSha512(string texto)
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(texto);
            using (var hash = System.Security.Cryptography.SHA512.Create())
            {
                var hashedInputBytes = hash.ComputeHash(bytes);
                var hashedInputStringBuilder = new System.Text.StringBuilder(128);
                foreach (var b in hashedInputBytes)
                    hashedInputStringBuilder.Append(b.ToString("X2"));
                return hashedInputStringBuilder.ToString().ToLower();
            }
        }
    }
}
