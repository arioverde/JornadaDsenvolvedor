using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiPonto.Models.Models
{
    public class Funcionario
    {
        public int FunconarioId { get; set; }
        public string NomeDoFuncionario { get; set; }
        public string Cpf { get; set; }
        public DateTime DataDeAdmissao { get; set; }
        public string CelularFuncionario { get; set; }
        public string EmailFuncionario { get; set; }
        public int CargoId { get; set; }
    }
}
