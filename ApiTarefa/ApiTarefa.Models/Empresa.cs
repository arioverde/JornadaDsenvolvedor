using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiTarefa.Models
{
    public class Empresa
    {
        public string Cnpj { get; set; }
        public string RazaoSocial { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
