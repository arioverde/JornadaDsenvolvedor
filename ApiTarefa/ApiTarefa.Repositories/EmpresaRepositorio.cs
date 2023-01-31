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
    public class EmpresaRepositorio : Contexto
    {
        public EmpresaRepositorio(IConfiguration configuration) : base(configuration)
        {
        }

        public void Inserir(Empresa model)
        {
            string comandoSql = @"INSERT INTO Empresa (Cnpj, RazaoSocial, DataCadastro) 
                                VALUES 
                                (@Cnpj, @RazaoSocial, @DataCadastro)";

            using (var cmd = new SqlCommand(comandoSql, _conn))
            {
                cmd.Parameters.AddWithValue("@Cnpj", model.Cnpj);
                cmd.Parameters.AddWithValue("@RazaoSocial", model.RazaoSocial);
                cmd.Parameters.AddWithValue("@DataCadastro", model.DataCadastro);
                cmd.ExecuteNonQuery();
            }
        }
        public void Atualizar(Empresa model)
        {
            string comandoSql = @"UPDATE Empresa 
                                SET RazaoSocial = @Cnpj, DataCadastro = @DataCadastro 
                                WHERE Cnpj = @Cnpj";

            using (var cmd = new SqlCommand(comandoSql, _conn))
            {
                cmd.Parameters.AddWithValue("@Cnpj", model.Cnpj);
                cmd.Parameters.AddWithValue("@RazaoSocial", model.RazaoSocial);
                cmd.Parameters.AddWithValue("@DataCadastro", model.DataCadastro);
                if (cmd.ExecuteNonQuery() == 0)
                    throw new ValidacaoException($"Nenhum registro afetado para o Cnpj {model.Cnpj}");
            }
        }
        public List<Empresa> ListarEmpresas(string? nome)
        {
            string comandoSql = @"SELECT Cnpj, RazaoSocial, DataCadastro FROM Empresa";

            if (!(string.IsNullOrWhiteSpace(nome)))
                comandoSql += " WHERE Nome LIKE @nome";


            using (var cmd = new SqlCommand(comandoSql, _conn))
            {
                if (!(string.IsNullOrWhiteSpace(nome)))
                    cmd.Parameters.AddWithValue("@nome", "%" + nome + "%");

                using (var rdr = cmd.ExecuteReader())
                {
                    var empresas = new List<Empresa>();
                    while (rdr.Read())
                    {
                        var empresa = new Empresa();
                        empresa.Cnpj = Convert.ToString(rdr["Cnpj"]);
                        empresa.RazaoSocial = Convert.ToString(rdr["RazaoSocial"]);
                        empresa.DataCadastro = Convert.ToDateTime(rdr["DataCadastro"]);
                        empresas.Add(empresa);
                    }
                    return empresas;
                }
            }
        }
        public void Deletar(string Cnpj)
        {
            string comandoSql = "DELETE FROM Empresa WHERE Cnpj = @Cnpj";

            using (var cmd = new SqlCommand(comandoSql, _conn))
            {
                cmd.Parameters.AddWithValue("@Cnpj", Cnpj);
                if (cmd.ExecuteNonQuery() == 0)
                    throw new ValidacaoException($"Nenhum registro afetado para o Cnpj {Cnpj}");
            }
        }
        public Empresa? Obter(string Cnpj)
        {
            string comandoSql = @"SELECT Cnpj, RazaoSocial, DataCadastro FROM Empresa WHERE Cnpj = @Cnpj";

            using (var cmd = new SqlCommand(comandoSql, _conn))
            {
                cmd.Parameters.AddWithValue("@Cnpj", Cnpj);

                using (var rdr = cmd.ExecuteReader())
                {
                    var empresa = new Empresa();

                    if (rdr.Read())
                    {
                        empresa.Cnpj = Convert.ToString(rdr["Cnpj"]);
                        empresa.RazaoSocial = Convert.ToString(rdr["RazaoSocial"]);
                        empresa.DataCadastro = Convert.ToDateTime(rdr["DataCadastro"]);
                        return empresa;
                    }
                    else
                        return null;
                }
            }
        }
    }
}
