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
    public class EquipeRepository : Contexto
    {
        public void Inserir(Equipe model)
        {
            string comandoSql = @"INSERT INTO Equipes (LiderancaId, FuncionarioId) VALUES (@LiderancaId, @FuncionarioId)";

            using (var cmd = new SqlCommand(comandoSql, _conn))
            {
                cmd.Parameters.AddWithValue("@LiderancaId", model.LiderancaId);
                cmd.Parameters.AddWithValue("@FuncionarioId", model.FuncionarioId);
                cmd.ExecuteNonQuery();
            }
        }
        public void Atualizar(Equipe model)
        {
            string comandoSql = @"UPDATE Equipes 
                                SET LiderancaId = @LiderancaId, FuncionarioId = @FuncionarioId
                                WHERE EquipeId = @EquipeId";

            using (var cmd = new SqlCommand(comandoSql, _conn))
            {
                cmd.Parameters.AddWithValue("@Descricao", model.LiderancaId);
                cmd.Parameters.AddWithValue("@Descricao", model.FuncionarioId);

                if (cmd.ExecuteNonQuery() == 0)
                    throw new ValidacaoException($"Nenhum registro afetado para a Equipe {model.EquipeId}");
            }
        }
        public bool SeExiste(string EquipeId)
        {
            string comandoSql = @"SELECT COUNT(EquipeId) AS Total FROM Equipes WHERE EquipeId = @EquipeId";
            using (var cmd = new SqlCommand(comandoSql, _conn))
            {
                cmd.Parameters.AddWithValue("@EquipeId", EquipeId);
                return Convert.ToBoolean(cmd.ExecuteScalar());
            }
        }
        public List<Equipe> ListarEquipes()
        {
            string comandoSql = @"SELECT EquipeId, LiderancaId, FuncionarioId FROM Equipes";

            using (var cmd = new SqlCommand(comandoSql, _conn))
            {
                using (var rdr = cmd.ExecuteReader())
                {
                    var equipes = new List<Equipe>();
                    while (rdr.Read())
                    {
                        var equipe = new Equipe();
                        equipe.EquipeId = Convert.ToInt32(rdr["EquipeId"]);
                        equipe.LiderancaId = Convert.ToInt32(rdr["LiderancaId"]);
                        equipe.FuncionarioId = Convert.ToInt32(rdr["FuncionarioId"]);

                        equipes.Add(equipe);
                    }
                    return equipes;
                }
            }
        }
        public void Deletar(int EquipeId)
        {
            string comandoSql = "DELETE FROM Equipes WHERE EquipeId = @EquipeId";

            using (var cmd = new SqlCommand(comandoSql, _conn))
            {
                cmd.Parameters.AddWithValue("@EquipeId", EquipeId);
                if (cmd.ExecuteNonQuery() == 0)
                    throw new ValidacaoException($"Nenhum registro afetado para a Equipe {EquipeId}");
            }
        }
        public Equipe? Obter(int EquipeId)
        {
            string comandoSql = @"SELECT EquipeId, LiderancaId, FuncionarioId FROM Equipes WHERE EquipeId = @EquipeId";

            using (var cmd = new SqlCommand(comandoSql, _conn))
            {
                cmd.Parameters.AddWithValue("@EquipeId", EquipeId);

                using (var rdr = cmd.ExecuteReader())
                {
                    var equipe = new Equipe();

                    if (rdr.Read())
                    {
                        equipe.EquipeId = Convert.ToInt32(rdr["EquipeId"]);
                        equipe.LiderancaId = Convert.ToInt32(rdr["LiderancaId"]);
                        equipe.FuncionarioId = Convert.ToInt32(rdr["FuncionarioId"]);
                        return equipe;
                    }
                    else
                        return null;
                }
            }
        }
    }
}
