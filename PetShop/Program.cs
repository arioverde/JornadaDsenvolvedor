using PetShop.Modelos;
using PetShop.Repositorios;
using PetShop.Servicos;

namespace PetShop
{
    public class Program
    {
        static void Main(string[] args)
        {
            new Servicos.ClienteServico().Menu();
              
        }
    }
}