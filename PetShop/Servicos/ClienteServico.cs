using Microsoft.VisualBasic;
using PetShop.Modelos;
using PetShop.Utilitarios;
using System;
using System.Collections.Generic;
using System.Globalization;
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
            if (!Validacoes.ValidarNome(nome, 3, 80))
            {
                Console.WriteLine("");
                Console.WriteLine("Nome inválido. O número de caracteres deve estar entre 3 e 80!");
                Console.WriteLine("Tecle <Enter> para continuar...");
                Console.ReadKey();
                Console.Clear();
                Cadastrar();
            }

            Console.WriteLine("Informe o CPF do cliente: ");
            var CPF = Console.ReadLine();

            Console.WriteLine("Informe a data de Nascimento do cliente (dd/mm/aaaa): ");
            var nascimentoString = Console.ReadLine();

            if (!Validacoes.ValidaFormatoData(nascimentoString))
            {
                Console.WriteLine("Formato inválido. Tecle <Enter> para continuar...");
                Console.ReadKey();
                Console.Clear();
                Cadastrar();
            }
            var nascimento = new DateTime();
            nascimento = DateTime.ParseExact(nascimentoString, "dd/MM/yyyy", null);

            if (!Validacoes.ValidarFaixaEtaria(nascimento))
            {
                Console.WriteLine("Cadastro permitido somente para pessoas entre 16 e 120 anos.");
                Console.WriteLine("Tecle <Enter> para continuar...");
                Console.ReadKey();
                return;
            }

            _repositorio.Inserir(new Modelos.Cliente()
            {
                Nome = nome,
                CPF = CPF,
                Nascimento = nascimento
            });
        }
        public void Listar()
        {
            var clientes = _repositorio.RetornaListaAtualizada();

            Console.WriteLine("Lista de clientes:");
            Console.WriteLine("==================" + Environment.NewLine);

            foreach (var cliente in clientes.OrderBy(x => x.Nome))
            {
                Console.WriteLine($"Nome: {cliente.Nome.ToUpper()}");
                Console.WriteLine($"CPF: {cliente.CPF}");
                Console.WriteLine($"Data Nascimento: {cliente.Nascimento.ToString(format: "dd/MM/yyyy")}");
                Console.WriteLine("");
            }
            Console.WriteLine("Tecle <Enter> para continuar...");
            Console.ReadKey();
        }
        public void ExibePorCPF()
        {
            var clientes = _repositorio.RetornaListaAtualizada();

            Console.Write("Informe o CPF: ");
            var CPFinformado = Console.ReadLine();
            Console.WriteLine("");

            foreach (var cliente in clientes)
            {
                if (cliente.CPF == CPFinformado)
                {
                    Console.WriteLine($"Nome: {cliente.Nome.ToUpper()}");
                    Console.WriteLine($"Data Nascimento: {cliente.Nascimento.ToString(format: "dd/MM/yyyy")}");
                    break;
                }
                else
                {
                    Console.WriteLine("CPF não cadastrado");
                    break;
                }
            }
        }
        public void AniversariantesMes()
        {
            var clientes = _repositorio.RetornaListaAtualizada();

            Console.WriteLine($"Aniversariantes do mês:");
            Console.WriteLine("=======================");
            Console.WriteLine("");

            clientes = clientes.OrderBy(x => x.Nascimento.Day).ToList();

            int count = 0;
            int idade;

            foreach (var cliente in clientes)
            {
                if (cliente.Nascimento.Month == DateTime.Now.Month)
                {
                    Console.WriteLine($"Dia {cliente.Nascimento.Day} - {cliente.Nome.ToUpper()}");
                    count++;
                }
            }
            if (count == 0)
                Console.WriteLine("Não há aniversariantes no mês corrente.");
        }
    }
}
