using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiPonto.Repositories
{
    public class Contexto
    {
        public readonly SqlConnection _conn;

        public Contexto()
        {
            _conn = new SqlConnection("Server=DESKTOP-U6BFCQI\\SQLEXPRESS;Database=ponto;Trusted_Connection=True;");
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
