using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiPonto.Models.Models
{
    public class Ponto
    {
        public long PontoId { get; set; }
        public DateTime DataHorarioPonto { get; set; }
        public string? Justificativa { get; set; }
        public int FuncionarioId { get; set; }
    }
}