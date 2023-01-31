using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiTarefa.Repositories
{
    public class Contexto
    {
        public readonly SqlConnection _conn;

        public Contexto(IConfiguration configuration)
        {
            _conn = new SqlConnection(configuration["DbCredentials"]);
        }
        public void AbrirConexao()
        {
            _conn.Open();
        }
        public void FecharConexao()
        {
            _conn.Close();
        }

    }
}
