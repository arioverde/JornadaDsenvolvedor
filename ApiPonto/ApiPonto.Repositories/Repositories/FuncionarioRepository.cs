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
    public class FuncionarioRepository : Contexto
    {
        public void Inserir(Funcionario model)
        {
            string comandoSql = @"INSERT INTO Funcionarios (NomeDoFuncionario, Cpf, NascimentoFuncionario, DataDeAdmissao, CelularFuncionario, EmailFuncionario, CargoId) 
                                VALUES 
                                (@NomeDoFuncionario, @Cpf, @NascimentoFuncionario, @DataDeAdmissao, @CelularFuncionario, @EmailFuncionario, @CargoId)";

            using (var cmd = new SqlCommand(comandoSql, _conn))
            {
                cmd.Parameters.AddWithValue("@NomeDoFuncionario", model.NomeDoFuncionario);
                cmd.Parameters.AddWithValue("@Cpf", model.Cpf);
                cmd.Parameters.AddWithValue("@NascimentoFuncionario", model.NascimentoFuncionario);
                cmd.Parameters.AddWithValue("@DataDeAdmissao", model.DataDeAdmissao);
                cmd.Parameters.AddWithValue("@CelularFuncionario", model.CelularFuncionario is null ? DBNull.Value : model.CelularFuncionario);
                cmd.Parameters.AddWithValue("@EmailFuncionario", model.EmailFuncionario);
                cmd.Parameters.AddWithValue("@CargoId", model.CargoId);
                cmd.ExecuteNonQuery();
            }
        }
        public void Atualizar(Funcionario model)
        {
            string comandoSql = @"UPDATE Funcionarios 
                                SET NomeDoFuncionario = @NomeDoFuncionario, Cpf = @Cpf, NascimentoFuncionario = @NascimentoFuncionario,
                                DataDeAdmissao = @DataDeAdmissao, CelularFuncionario = @CelularFuncionario, EmailFuncionario = @EmailFuncionario, CargoId = @CargoId
                                WHERE FuncionarioId = @FuncionarioId";

            using (var cmd = new SqlCommand(comandoSql, _conn))
            {
                cmd.Parameters.AddWithValue("@FuncionarioId", model.FuncionarioId);
                cmd.Parameters.AddWithValue("@NomeDoFuncionario", model.NomeDoFuncionario);
                cmd.Parameters.AddWithValue("@Cpf", model.Cpf);
                cmd.Parameters.AddWithValue("@DataDeAdmissao", model.DataDeAdmissao);
                cmd.Parameters.AddWithValue("@NascimentoFuncionario", model.NascimentoFuncionario);
                cmd.Parameters.AddWithValue("@CelularFuncionario", model.CelularFuncionario is null ? DBNull.Value : model.CelularFuncionario);
                cmd.Parameters.AddWithValue("@EmailFuncionario", model.EmailFuncionario);
                cmd.Parameters.AddWithValue("@CargoId", model.CargoId);
                if (cmd.ExecuteNonQuery() == 0)
                    throw new ValidacaoException($"Nenhum registro afetado para o Funcionário {model.FuncionarioId}");
            }
        }
        public bool SeExiste(string FuncionarioId)
        {
            string comandoSql = @"SELECT COUNT(FuncionarioId) AS Total FROM Funcionarios WHERE FuncionarioId = @FuncionarioId";
            using (var cmd = new SqlCommand(comandoSql, _conn))
            {
                cmd.Parameters.AddWithValue("@FuncionarioId", FuncionarioId);
                return Convert.ToBoolean(cmd.ExecuteScalar());
            }
        }
        public List<Funcionario> ListarFuncionarios(string? nome)
        {
            string comandoSql = @"SELECT FuncionarioId, NomeDoFuncionario, Cpf, NascimentoFuncionario, DataDeAdmissao, CelularFuncionario, EmailFuncionario, CargoId FROM Funcionarios";

            if (!(string.IsNullOrWhiteSpace(nome)))
                comandoSql += " WHERE NomeDoFuncionario LIKE @nome";


            using (var cmd = new SqlCommand(comandoSql, _conn))
            {
                if (!(string.IsNullOrWhiteSpace(nome)))
                    cmd.Parameters.AddWithValue("@nome", "%" + nome + "%");

                using (var rdr = cmd.ExecuteReader())
                {
                    var funcionarios = new List<Funcionario>();
                    while (rdr.Read())
                    {
                        var funcionario = new Funcionario();
                        funcionario.FuncionarioId = Convert.ToInt32(rdr["FuncionarioId"]);
                        funcionario.NomeDoFuncionario = Convert.ToString(rdr["NomeDoFuncionario"]);
                        funcionario.Cpf = Convert.ToString(rdr["Cpf"]);
                        funcionario.NascimentoFuncionario= Convert.ToDateTime(rdr["NascimentoFuncionario"]);
                        funcionario.DataDeAdmissao = Convert.ToDateTime(rdr["DataDeAdmissao"]);
                        funcionario.CelularFuncionario = rdr["CelularFuncionario"] == DBNull.Value ? null : Convert.ToString(rdr["CelularFuncionario"]);
                        funcionario.EmailFuncionario = Convert.ToString(rdr["EmailFuncionario"]);
                        funcionario.CargoId = Convert.ToInt32(rdr["CargoId"]);
                        funcionarios.Add(funcionario);
                    }
                    return funcionarios;
                }
            }
        }
        public void Deletar(int FuncionarioId)
        {
            string comandoSql = "DELETE FROM Funcionarios WHERE FuncionarioId = @FuncionarioId";

            using (var cmd = new SqlCommand(comandoSql, _conn))
            {
                cmd.Parameters.AddWithValue("@FuncionarioId", FuncionarioId);
                if (cmd.ExecuteNonQuery() == 0)
                    throw new ValidacaoException($"Nenhum registro afetado para o Funcionário {FuncionarioId}");
            }
        }
        public Funcionario? Obter(int FuncionarioId)
        {
            string comandoSql = @"SELECT FuncionarioId, NomeDoFuncionario, Cpf, NascimentoFuncionario, DataDeAdmissao, CelularFuncionario, EmailFuncionario, CargoId FROM Funcionarios WHERE FuncionarioId = @FuncionarioId";

            using (var cmd = new SqlCommand(comandoSql, _conn))
            {
                cmd.Parameters.AddWithValue("@FuncionarioId", FuncionarioId);

                using (var rdr = cmd.ExecuteReader())
                {
                    var funcionario = new Funcionario();

                    if (rdr.Read())
                    {
                        funcionario.FuncionarioId = Convert.ToInt32(rdr["FuncionarioId"]);
                        funcionario.NomeDoFuncionario = Convert.ToString(rdr["NomeDoFuncionario"]);
                        funcionario.Cpf = Convert.ToString(rdr["Cpf"]);
                        funcionario.NascimentoFuncionario = Convert.ToDateTime(rdr["NascimentoFuncionario"]);
                        funcionario.DataDeAdmissao = Convert.ToDateTime(rdr["DataDeAdmissao"]);
                        funcionario.CelularFuncionario = rdr["CelularFuncionario"] == DBNull.Value ? null : Convert.ToString(rdr["CelularFuncionario"]);
                        funcionario.EmailFuncionario = Convert.ToString(rdr["EmailFuncionario"]);
                        funcionario.CargoId = Convert.ToInt32(rdr["CargoId"]);
                        return funcionario;
                    }
                    else
                        return null;
                }
            }
        }
    }
}
