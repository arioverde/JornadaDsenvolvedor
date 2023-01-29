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
    public class CargoRepository : Contexto
    {
        public void Inserir(Cargo model)
        {
            string comandoSql = @"INSERT INTO Cargos (Descricao) VALUES (@Descricao)";

            using (var cmd = new SqlCommand(comandoSql, _conn))
            {
                cmd.Parameters.AddWithValue("@Descricao", model.Descricao);
                cmd.ExecuteNonQuery();
            }
        }
        public void Atualizar(Cargo model)
        {
            string comandoSql = @"UPDATE Cargos 
                                SET Descricao = @Descricao
                                WHERE CargoId = @CargoId";

            using (var cmd = new SqlCommand(comandoSql, _conn))
            {
                cmd.Parameters.AddWithValue("@CargoId", model.CargoId);
                cmd.Parameters.AddWithValue("@Descricao", model.Descricao);
                if (cmd.ExecuteNonQuery() == 0)
                    throw new ValidacaoException($"Nenhum registro afetado para o Cargo {model.CargoId}");
            }
        }
        public bool SeExiste(string CargoId)
        {
            string comandoSql = @"SELECT COUNT(CargoId) AS Total FROM Cargos WHERE CargoId = @CargoId";
            using (var cmd = new SqlCommand(comandoSql, _conn))
            {
                cmd.Parameters.AddWithValue("@CargoId", CargoId);
                return Convert.ToBoolean(cmd.ExecuteScalar());
            }
        }
        public List<Cargo> ListarCargos()
        {
            string comandoSql = @"SELECT CargoId, Descricao FROM Cargos";

            using (var cmd = new SqlCommand(comandoSql, _conn))
            {
                using (var rdr = cmd.ExecuteReader())
                {
                    var cargos = new List<Cargo>();
                    while (rdr.Read())
                    {
                        var cargo = new Cargo();
                        cargo.CargoId = Convert.ToInt32(rdr["CargoId"]);
                        cargo.Descricao = Convert.ToString(rdr["Descricao"]);
                        cargos.Add(cargo);
                    }
                    return cargos;
                }
            }
        }
        public void Deletar(int CargoId)
        {
            string comandoSql = "DELETE FROM Cargos WHERE CargoId = @CargoId";

            using (var cmd = new SqlCommand(comandoSql, _conn))
            {
                cmd.Parameters.AddWithValue("@CargoId", CargoId);
                if (cmd.ExecuteNonQuery() == 0)
                    throw new ValidacaoException($"Nenhum registro afetado para o Cargo {CargoId}");
            }
        }
        public Cargo? Obter(int CargoId)
        {
            string comandoSql = @"SELECT CargoId, Descricao FROM Cargos WHERE CargoId = @CargoId";

            using (var cmd = new SqlCommand(comandoSql, _conn))
            {
                cmd.Parameters.AddWithValue("@CargoId", CargoId);

                using (var rdr = cmd.ExecuteReader())
                {
                    var cargo = new Cargo();

                    if (rdr.Read())
                    {
                        cargo.CargoId = Convert.ToInt32(rdr["CargoId"]);
                        cargo.Descricao = Convert.ToString(rdr["Descricao"]);
                       
                        return cargo;
                    }
                    else
                        return null;
                }
            }
        }
    }
}
