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
    public class TarefaRepositorio : Contexto
    {
        public TarefaRepositorio(IConfiguration configuration) : base(configuration)
        {
        }

        public void Inserir(Tarefa model)
        {
            string comandoSql = @"INSERT INTO Tarefa (HorarioInicio, DescricaoResumida, DescricaoLonga, TipoTarefa, Email, Cnpj, RazaoSocial) 
                                VALUES 
                                (@HorarioInicio, @DescricaoResumida, @DescricaoLonga, @TipoTarefa, @Email, @Cnpj, @RazaoSocial)";

            using (var cmd = new SqlCommand(comandoSql, _conn))
            {
                cmd.Parameters.AddWithValue("@HorarioInicio", model.HorarioInicio);
                cmd.Parameters.AddWithValue("@DescricaoResumida", model.DescricaoResumida);
                cmd.Parameters.AddWithValue("@DescricaoLonga", model.DescricaoLonga);
                cmd.Parameters.AddWithValue("@TipoTarefa", model.TipoTarefa);
                cmd.Parameters.AddWithValue("@Email", model.Email);
                cmd.Parameters.AddWithValue("@Cnpj", model.Cnpj);
                cmd.Parameters.AddWithValue("@RazaoSocial", model.RazaoSocial);

                cmd.ExecuteNonQuery();
            }
        }
        public void Atualizar(Tarefa model)
        {
            //string comandoSql = @"UPDATE Tarefa
            //            SET HorarioInicio = @HorarioInicio, HorarioFim = @HorarioFim, DescricaoResumida = @DescricaoResumida, 
            //                DescricaoLonga = @DescricaoLonga, TipoTarefa = @TipoTarefa, Email = @Email, Cnpj = @Cnpj, RazaoSocial = @RazaoSocial
            //            WHERE IdentificadorTarefa = @IdentificadorTarefa";

            string comandoSql = @"UPDATE Tarefa
                        SET HorarioFim = @HorarioFim, DescricaoResumida = @DescricaoResumida, 
                            DescricaoLonga = @DescricaoLonga, TipoTarefa = @TipoTarefa
                        WHERE IdentificadorTarefa = @IdentificadorTarefa";


            using (var cmd = new SqlCommand(comandoSql, _conn))
            {
                cmd.Parameters.AddWithValue("@IdentificadorTarefa", model.IdentificadorTarefa);
                //cmd.Parameters.AddWithValue("@HorarioInicio", model.HorarioInicio);
                cmd.Parameters.AddWithValue("@HorarioFim", model.HorarioFim is null ? DBNull.Value :  model.HorarioFim);
                cmd.Parameters.AddWithValue("@DescricaoResumida", model.DescricaoResumida);
                cmd.Parameters.AddWithValue("@DescricaoLonga", model.DescricaoLonga);
                cmd.Parameters.AddWithValue("@TipoTarefa", model.TipoTarefa);
                //cmd.Parameters.AddWithValue("@Email", model.Email);
                //cmd.Parameters.AddWithValue("@Cnpj", model.Cnpj);
                //cmd.Parameters.AddWithValue("@RazaoSocial", model.RazaoSocial);
                if (cmd.ExecuteNonQuery() == 0)
                    throw new ValidacaoException($"Nenhum registro afetado para o IdentificadorTarefa {model.IdentificadorTarefa}");
            }
        }
        public List<Tarefa> ListarTarefas()
        {
            string comandoSql = @"SELECT IdentificadorTarefa, HorarioInicio, HorarioFim, DescricaoResumida, 
                            DescricaoLonga, TipoTarefa, Email, t.Cnpj, e.RazaoSocial FROM Tarefa t
                            JOIN Empresa e ON t.Cnpj = e.Cnpj;";

            using (var cmd = new SqlCommand(comandoSql, _conn))
            {
                using (var rdr = cmd.ExecuteReader())
                {
                    var tarefas = new List<Tarefa>();
                    while (rdr.Read())
                    {
                        var tarefa = new Tarefa();
                        tarefa.IdentificadorTarefa = Convert.ToInt32(rdr["IdentificadorTarefa"]);
                        tarefa.HorarioInicio = Convert.ToDateTime(rdr["HorarioInicio"]);
                        tarefa.HorarioFim = (rdr["HorarioFim"]) == DBNull.Value ? null : Convert.ToDateTime(rdr["HorarioFim"]);
                        tarefa.DescricaoResumida = Convert.ToString(rdr["DescricaoResumida"]);
                        tarefa.DescricaoLonga = Convert.ToString(rdr["DescricaoLonga"]);
                        tarefa.TipoTarefa = (EnumTipoTarefa)(rdr["TipoTarefa"]);
                        tarefa.Email = Convert.ToString(rdr["Email"]);
                        tarefa.Cnpj = Convert.ToString(rdr["Cnpj"]);
                        tarefa.RazaoSocial = Convert.ToString(rdr["RazaoSocial"]);
                        tarefas.Add(tarefa);
                    }
                    return tarefas;
                }
            }
        }
        public void Deletar(int IdentificadorTarefa)
        {
            string comandoSql = "DELETE FROM Tarefa WHERE IdentificadorTarefa = @IdentificadorTarefa";

            using (var cmd = new SqlCommand(comandoSql, _conn))
            {
                cmd.Parameters.AddWithValue("@IdentificadorTarefa", IdentificadorTarefa);
                if (cmd.ExecuteNonQuery() == 0)
                    throw new ValidacaoException($"Nenhum registro afetado para o IdentificadorTarefa {IdentificadorTarefa}");
            }
        }
        public Tarefa? Obter(int IdentificadorTarefa)
        {
            string comandoSql = @"SELECT IdentificadorTarefa, HorarioInicio, HorarioFim, DescricaoResumida, DescricaoLonga, TipoTarefa, Email, Cnpj, RazaoSocial
                                FROM Tarefa WHERE IdentificadorTarefa = @IdentificadorTarefa";

            using (var cmd = new SqlCommand(comandoSql, _conn))
            {
                cmd.Parameters.AddWithValue("@IdentificadorTarefa", IdentificadorTarefa);

                using (var rdr = cmd.ExecuteReader())
                {
                    var tarefa = new Tarefa();

                    if (rdr.Read())
                    {
                        tarefa.IdentificadorTarefa = Convert.ToInt32(rdr["IdentificadorTarefa"]);
                        tarefa.HorarioInicio = Convert.ToDateTime(rdr["HorarioInicio"]);
                        tarefa.HorarioFim = (rdr["HorarioFim"]) == DBNull.Value ? null : Convert.ToDateTime(rdr["HorarioFim"]);
                        tarefa.DescricaoResumida = Convert.ToString(rdr["DescricaoResumida"]);
                        tarefa.DescricaoLonga = Convert.ToString(rdr["DescricaoLonga"]);
                        tarefa.TipoTarefa = (EnumTipoTarefa)(rdr["TipoTarefa"]);
                        tarefa.Email = Convert.ToString(rdr["Email"]);
                        tarefa.Cnpj = Convert.ToString(rdr["Cnpj"]);
                        tarefa.RazaoSocial = Convert.ToString(rdr["RazaoSocial"]);
                        return tarefa;
                    }
                    else
                        return null;
                }
            }
        }
    }
}
