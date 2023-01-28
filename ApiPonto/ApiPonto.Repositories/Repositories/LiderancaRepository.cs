using ApiPonto.Models.Exceptions;
using ApiPonto.Models.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiPonto.Repositories.Repositories
{
    public class LiderancaRepository : Contexto
    {
        public void Inserir(Lideranca model)
        {
            string comandoSql = @"INSERT INTO Liderancas (FuncionarioId, DescricaoEquipe) VALUES (@FuncionarioId, @DescricaoEquipe)";

            using (var cmd = new SqlCommand(comandoSql, _conn))
            {
                cmd.Parameters.AddWithValue("@FuncionarioId", model.FuncionarioId);
                cmd.Parameters.AddWithValue("@DescricaoEquipe", model.DescricaoEquipe);
                cmd.ExecuteNonQuery();
            }
        }
        public void Atualizar(Lideranca model)
        {
            string comandoSql = @"UPDATE Liderancas 
                                SET FuncionarioId = @FuncionarioId, DescricaoEquipe = @DescricaoEquipe
                                WHERE LiderancaId = @LiderancaId";

            using (var cmd = new SqlCommand(comandoSql, _conn))
            {
                cmd.Parameters.AddWithValue("@FuncionarioId", model.FuncionarioId);
                cmd.Parameters.AddWithValue("@DescricaoEquipe", model.DescricaoEquipe);
                if (cmd.ExecuteNonQuery() == 0)
                    throw new ValidadaoException($"Nenhum registro afetado para a Lideranca {model.LiderancaId}");
            }
        }
        public bool SeExiste(string LiderancaId)
        {
            string comandoSql = @"SELECT COUNT(LiderancaId) AS Total FROM Liderancas WHERE LiderancaId = @LiderancaId";
            using (var cmd = new SqlCommand(comandoSql, _conn))
            {
                cmd.Parameters.AddWithValue("@LiderancaId", LiderancaId);
                return Convert.ToBoolean(cmd.ExecuteScalar());
            }
        }
        public List<Lideranca> ListarLiderancas(string? nome)
        {
            string comandoSql = @"SELECT LiderancaId, FuncionarioId, DescricaoEquipe FROM Liderancas";

            if (!(string.IsNullOrWhiteSpace(nome)))
                comandoSql += " WHERE Descricao LIKE @nome";


            using (var cmd = new SqlCommand(comandoSql, _conn))
            {
                if (!(string.IsNullOrWhiteSpace(nome)))
                    cmd.Parameters.AddWithValue("@nome", "%" + nome + "%");

                using (var rdr = cmd.ExecuteReader())
                {
                    var liderancas = new List<Lideranca>();
                    while (rdr.Read())
                    {
                        var lideranca = new Lideranca();
                        lideranca.LiderancaId = Convert.ToInt32(rdr["LiderancaId"]);
                        lideranca.FuncionarioId = Convert.ToInt32(rdr["FuncionarioId"]);
                        lideranca.DescricaoEquipe = Convert.ToString(rdr["DescricaoEquipe"]);
                        liderancas.Add(lideranca);
                    }
                    return liderancas;
                }
            }
        }
        public void Deletar(int LiderancaId)
        {
            string comandoSql = "DELETE FROM Liderancas WHERE LiderancaId = @LiderancaId";

            using (var cmd = new SqlCommand(comandoSql, _conn))
            {
                cmd.Parameters.AddWithValue("@LiderancaId", LiderancaId);
                if (cmd.ExecuteNonQuery() == 0)
                    throw new ValidadaoException($"Nenhum registro afetado para a Lideranca {LiderancaId}");
            }
        }
        public Lideranca? Obter(int LiderancaId)
        {
            string comandoSql = @"SELECT LiderancaId, FuncionarioId, DescricaoEquipe FROM Liderancas WHERE LiderancaId = @LiderancaId";

            using (var cmd = new SqlCommand(comandoSql, _conn))
            {
                cmd.Parameters.AddWithValue("@LiderancaId", LiderancaId);

                using (var rdr = cmd.ExecuteReader())
                {
                    var lideranca = new Lideranca();

                    if (rdr.Read())
                    {
                        lideranca.LiderancaId = Convert.ToInt32(rdr["LiderancaId"]);
                        lideranca.FuncionarioId = Convert.ToInt32(rdr["FuncionarioId"]);
                        lideranca.DescricaoEquipe = Convert.ToString(rdr["DescricaoEquipe"]);

                        return lideranca;
                    }
                    else
                        return null;
                }
            }
        }

    }
}
