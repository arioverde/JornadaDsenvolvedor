using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiTarefa.Models
{
    public enum EnumCargoUsuario
    {
        Desenvolvedor = 1,
        Gestor
    }
    public class Usuario
    {
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Nome { get; set; }
        public EnumCargoUsuario CargoUsuario { get; set; }
    }
}
