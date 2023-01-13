using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Servicos
{

    public class ClienteServico
    {
        private readonly Repositorios.ClienteRepositorio _repositorio;
        public ClienteServico()
        {
            _repositorio = new Repositorios.ClienteRepositorio();
        }

        public void Cadastrar()
        {
            Console.WriteLine("Informe o nome do cliente: ");
            var nome = Console.ReadLine();

            Console.WriteLine("Informe o CPF do cliente: ");
            var CPF = Console.ReadLine();

            //// ERRO VALIDAÇÃO DATA
            //Console.WriteLine("Informe a data de Nascimento do cliente: ");
            var nascimento = new DateTime(2002, 12, 15);

            //inicialização simplificada do objeto:
            _repositorio.Inserir(new Modelos.Cliente()
            {
                Nome = nome,
                CPF = CPF,
                Nascimento = nascimento
            });

        }
    }
}
