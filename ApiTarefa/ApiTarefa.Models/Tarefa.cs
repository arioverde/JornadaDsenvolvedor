using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiTarefa.Models
{
    public enum EnumTipoTarefa
    {
        Reuniao = 1,
        QuebraContexto,
        Tarefa
    }
    public class Tarefa
    {
        public int IdentificadorTarefa { get; set; }
        public DateTime HorarioInicio { get; set; }
        public DateTime? HorarioFim { get; set; }
        public string DescricaoResumida { get; set; }
        public string DescricaoLonga { get; set; }
        public EnumTipoTarefa TipoTarefa { get; set; }
        public string Email { get; set; }
        public string Cnpj { get; set; }    
    }
}
