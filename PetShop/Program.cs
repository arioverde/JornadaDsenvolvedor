using PetShop.Repositorios;

namespace PetShop
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var clienteRepositorio = new ClienteRepositorio();

            //clienteRepositorio.Cadastro();
            clienteRepositorio.AniversariantesMes();


        }
    }
}