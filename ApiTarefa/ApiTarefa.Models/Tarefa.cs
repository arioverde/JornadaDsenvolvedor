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
        public DateTime HoraInicio { get; set; }
        public DateTime HoraFim { get; set; }
        public string DescricaoResumida { get; set; }
        public string DescricaoLonga { get; set; }
        public EnumTipoTarefa TipoTarefa { get; set; }
    }
}
