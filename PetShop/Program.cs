using PetShop.Modelos;
using PetShop.Repositorios;
using PetShop.Servicos;

namespace PetShop
{
    internal class Program
    {
        static void Main(string[] args)
        {
            new Servicos.ClienteServico().Cadastrar();
                
        }
    }
}