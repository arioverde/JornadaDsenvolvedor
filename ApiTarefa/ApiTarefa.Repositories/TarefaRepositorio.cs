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
            string comandoSql = @"INSERT INTO Tarefa (HoraInicio, HoraFim, DescricaoResumida, DescricaoLonga, TipoTarefa) 
                                VALUES 
                                (@HoraInicio, @HoraFim, @DescricaoResumida, @DescricaoLonga, @TipoTarefa)";

            using (var cmd = new SqlCommand(comandoSql, _conn))
            {
                cmd.Parameters.AddWithValue("@HoraInicio", model.HoraInicio);
                cmd.Parameters.AddWithValue("@HoraFim", model.HoraFim);
                cmd.Parameters.AddWithValue("@DescricaoResumida", model.DescricaoResumida);
                cmd.Parameters.AddWithValue("@DescricaoLonga", model.DescricaoLonga);
                cmd.Parameters.AddWithValue("@TipoTarefa", model.TipoTarefa);
                cmd.ExecuteNonQuery();
            }
        }
        public void Atualizar(Tarefa model)
        {
            string comandoSql = @"UPDATE Tarefa
                        SET HoraInicio = @HoraInicio, HoraFim = @HoraFim, DescricaoResumida = @DescricaoResumida, DescricaoLonga = @DescricaoLonga, TipoTarefa = @TipoTarefa
                        WHERE IdentificadorTarefa = @IdentificadorTarefa";

            using (var cmd = new SqlCommand(comandoSql, _conn))
            {
                cmd.Parameters.AddWithValue("@IdentificadorTarefa", model.IdentificadorTarefa);
                cmd.Parameters.AddWithValue("@HoraInicio", model.HoraInicio);
                cmd.Parameters.AddWithValue("@HoraFim", model.HoraFim);
                cmd.Parameters.AddWithValue("@DescricaoResumida", model.DescricaoResumida);
                cmd.Parameters.AddWithValue("@DescricaoLonga", model.DescricaoLonga);
                cmd.Parameters.AddWithValue("@TipoTarefa", model.TipoTarefa);
                if (cmd.ExecuteNonQuery() == 0)
                    throw new ValidacaoException($"Nenhum registro afetado para o IdentificadorTarefa {model.IdentificadorTarefa}");
            }
        }
        public List<Tarefa> ListarTarefas()
        {
            string comandoSql = @"SELECT IdentificadorTarefa, HoraInicio, HoraFim, DescricaoResumida, DescricaoLonga, TipoTarefa FROM Tarefa";

            using (var cmd = new SqlCommand(comandoSql, _conn))
            {
                using (var rdr = cmd.ExecuteReader())
                {
                    var tarefas = new List<Tarefa>();
                    while (rdr.Read())
                    {
                        var tarefa = new Tarefa();
                        tarefa.IdentificadorTarefa = Convert.ToInt32(rdr["IdentificadorTarefa"]);
                        tarefa.HoraInicio = Convert.ToDateTime(rdr["HoraInicio"]);
                        tarefa.HoraFim = Convert.ToDateTime(rdr["HoraFim"]);
                        tarefa.DescricaoResumida = Convert.ToString(rdr["DescricaoResumida"]);
                        tarefa.DescricaoLonga = Convert.ToString(rdr["DescricaoLonga"]);
                        tarefa.TipoTarefa = (EnumTipoTarefa)(rdr["TipoTarefa"]);
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
            string comandoSql = @"SELECT IdentificadorTarefa, HoraInicio, HoraFim, DescricaoResumida, DescricaoLonga, TipoTarefa FROM Tarefa WHERE IdentificadorTarefa = @IdentificadorTarefa";

            using (var cmd = new SqlCommand(comandoSql, _conn))
            {
                cmd.Parameters.AddWithValue("@IdentificadorTarefa", IdentificadorTarefa);

                using (var rdr = cmd.ExecuteReader())
                {
                    var tarefa = new Tarefa();

                    if (rdr.Read())
                    {
                        tarefa.IdentificadorTarefa = Convert.ToInt32(rdr["IdentificadorTarefa"]);
                        tarefa.HoraInicio = Convert.ToDateTime(rdr["HoraInicio"]);
                        tarefa.HoraFim = Convert.ToDateTime(rdr["HoraFim"]);
                        tarefa.DescricaoResumida = Convert.ToString(rdr["DescricaoResumida"]);
                        tarefa.DescricaoLonga = Convert.ToString(rdr["DescricaoLonga"]);
                        tarefa.TipoTarefa = (EnumTipoTarefa)(rdr["TipoTarefa"]);
                        return tarefa;
                    }
                    else
                        return null;
                }
            }
        }
    }
}
