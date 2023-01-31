using ApiTarefa.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiTarefa.Repositories
{
    public class UsuarioRepositorio : Contexto
    {
        public UsuarioRepositorio(IConfiguration configuration) : base(configuration)
        {
        }
        public Usuario? ObterUsuarioPorRedenciais(string email, string senha)
        {
            string comandoSql = @"SELECT u.Email, u.Nome, u.IdentificadorCargo FROM Usuario u
                                JOIN Cargo c ON u.IdentificadorCargo = c.IdentificadorCargo
                                WHERE u.Email = @email AND u.Senha = @senha";

            using (var cmd = new SqlCommand(comandoSql, _conn))
            {
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@senha", senha);

                using (var rdr = cmd.ExecuteReader())
                {
                    if (rdr.Read())
                    {
                        return new Usuario()
                        {
                            Email = (rdr["Email"]).ToString(),
                            Nome = (rdr["Nome"]).ToString(),
                            CargoUsuario = (EnumCargoUsuario)(rdr["IdentificadorCargo"])
                        };
                    }
                    else
                        return null;
                }
            }
        }
    }
}
