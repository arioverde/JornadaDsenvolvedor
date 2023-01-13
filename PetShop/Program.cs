using PetShop.Modelos;
using PetShop.Repositorios;
using PetShop.Servicos;

namespace PetShop
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var clienteRepositorio = new ClienteRepositorio();
            clienteRepositorio.Listar();

            var clienteServico = new ClienteServico();
            //clienteServico.Cadastrar();
               
           


        }
    }
}