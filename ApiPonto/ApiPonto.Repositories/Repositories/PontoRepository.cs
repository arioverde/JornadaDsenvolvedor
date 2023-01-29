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
    public class PontoRepository : Contexto
    {
        public void Inserir(Ponto model)
        {
            string comandoSql = @"INSERT INTO Ponto (PontoId, DataHorarioPonto, Justificativa, FuncionarioId) 
                                VALUES 
                                (@PontoId, @DataHorarioPonto, @Justificativa, @FuncionarioId)";

            using (var cmd = new SqlCommand(comandoSql, _conn))
            {
                cmd.Parameters.AddWithValue("@PontoId", model.PontoId);
                cmd.Parameters.AddWithValue("@DataHorarioPonto", model.DataHorarioPonto);
                cmd.Parameters.AddWithValue("@Justificativa", model.Justificativa);
                cmd.Parameters.AddWithValue("@FuncionarioId", model.FuncionarioId);
                cmd.ExecuteNonQuery();
            }
        }
        public void Atualizar(Ponto model)
        {
            string comandoSql = @"UPDATE Ponto 
                                SET DataHorarioPonto = @DataHorarioPonto, Justificativa = @Justificativa, FuncionarioId = @FuncionarioId 
                                WHERE PontoId = @PontoId";

            using (var cmd = new SqlCommand(comandoSql, _conn))
            {
                cmd.Parameters.AddWithValue("@PontoId", model.PontoId);
                cmd.Parameters.AddWithValue("@DataHorarioPonto", model.DataHorarioPonto);
                cmd.Parameters.AddWithValue("@Justificativa", model.Justificativa);
                cmd.Parameters.AddWithValue("@FuncionarioId", model.FuncionarioId);
                if (cmd.ExecuteNonQuery() == 0)
                    throw new ValidadaoException($"Nenhum registro afetado para o Ponto {model.PontoId}");
            }
        }
        public bool SeExiste(string PontoId)
        {
            string comandoSql = @"SELECT COUNT(PontoId) AS Total FROM Ponto WHERE PontoId = @PontoId";
            using (var cmd = new SqlCommand(comandoSql, _conn))
            {
                cmd.Parameters.AddWithValue("@PontoId", PontoId);
                return Convert.ToBoolean(cmd.ExecuteScalar());
            }
        }
        public List<Ponto> ListarPontos()
        {
            string comandoSql = @"SELECT PontoId, DataHorarioPonto, Justificativa, FuncionarioId FROM Ponto";

            using (var cmd = new SqlCommand(comandoSql, _conn))

            using (var rdr = cmd.ExecuteReader())
            {
                var pontos = new List<Ponto>();
                while (rdr.Read())
                {
                    var ponto = new Ponto();
                    ponto.PontoId = Convert.ToInt32(rdr["PontoId"]);
                    ponto.DataHorarioPonto = Convert.ToDateTime(rdr["DataHorarioPonto"]);
                    ponto.Justificativa = Convert.ToString(rdr["Justificativa"]);
                    ponto.FuncionarioId = Convert.ToInt32(rdr["FuncionarioId"]);

                    pontos.Add(ponto);
                }
                return pontos;
            }
        }
        public void Deletar(int PontoId)
        {
            string comandoSql = "DELETE FROM Ponto WHERE PontoId = @PontoId";

            using (var cmd = new SqlCommand(comandoSql, _conn))
            {
                cmd.Parameters.AddWithValue("@PontoId", PontoId);
                if (cmd.ExecuteNonQuery() == 0)
                    throw new ValidadaoException($"Nenhum registro afetado para o Ponto {PontoId}");
            }
        }
        public Ponto? Obter(int PontoId)
        {
            string comandoSql = @"SELECT PontoId, DataHorarioPonto, Justificativa, FuncionarioId FROM Ponto WHERE PontoId = @PontoId";

            using (var cmd = new SqlCommand(comandoSql, _conn))
            {
                cmd.Parameters.AddWithValue("@PontoId", PontoId);

                using (var rdr = cmd.ExecuteReader())
                {
                    var ponto = new Ponto();

                    if (rdr.Read())
                    {
                        ponto.PontoId = Convert.ToInt32(rdr["PontoId"]);
                        ponto.DataHorarioPonto = Convert.ToDateTime(rdr["DataHorarioPonto"]);
                        ponto.Justificativa = Convert.ToString(rdr["Justificativa"]);
                        ponto.FuncionarioId = Convert.ToInt32(rdr["FuncionarioId"]);
                        return ponto;
                    }
                    else
                        return null;
                }
            }
        }
   
    }
}

