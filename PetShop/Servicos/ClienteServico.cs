using Microsoft.VisualBasic;
using PetShop.Modelos;
using PetShop.Utilitarios;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
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

        public void Menu()
        {
            var finaliza = false;

            while (!finaliza)
            {
                Console.Clear();
                Console.WriteLine("======= PetShop Rio Verde ======= ");
                Console.WriteLine("");
                Console.WriteLine("Escolha uma opção:");
                Console.WriteLine("");
                Console.WriteLine("1 - Cadastrar clientes");
                Console.WriteLine("2 - Listar clientes");
                Console.WriteLine("3 - Busca cliente pelo CPF");
                Console.WriteLine("4 - Listar aniversariantes do mês atual");
                Console.WriteLine("5 - FECHA O PROGRAMA");
                var opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        Cadastrar();
                        break;
                    case "2":
                        Listar();
                        break;
                    case "3":
                        ExibePorCPF();
                        break;
                    case "4":
                        AniversariantesMes();
                        break;
                    case "5":
                        finaliza = true;
                        break;
                    default:
                        Console.WriteLine("Selecione uma opção válida");
                        break;
                }
            }
        }
        private void Cadastrar()
        {
            Console.Clear();
            Console.WriteLine("Cadastro de clientes");
            Console.WriteLine("====================" + Environment.NewLine);
            Console.Write("Informe o nome: ");
            var nome = Console.ReadLine();
            
            if (!Validacoes.ValidarNome(nome, 3, 80))
            {
                Console.WriteLine("");
                Console.WriteLine("Nome inválido. O número de caracteres deve estar entre 3 e 80!");
                Console.Write("Tecle <Enter> para retornar ao Menu...");
                Console.ReadKey();
                Console.Clear();
                return;
            }

            Console.Write("Informe o CPF: ");
            var CPF = Console.ReadLine();
            
            if (!(Validacoes.ValidarCPF(CPF)))
            {
                Console.WriteLine("");
                Console.WriteLine("CPF inválido!");
                Console.Write("Tecle <Enter> para retornar ao Menu...");
                Console.ReadKey();
                return;
            }

            CPF = Convert.ToUInt64(CPF).ToString(@"000\.000\.000\-00");

            if(_repositorio.ExisteCPF(CPF))
            {
                Console.WriteLine("");
                Console.WriteLine("CPF já cadastrado!");
                Console.Write("Tecle <Enter> para retornar ao Menu...");
                Console.ReadKey();
                return;
            }

            Console.Write("Informe a data de nascimento, no formato --/--/---): ");
            var nascimentoString = Console.ReadLine();

            if (!(Validacoes.ValidarData(nascimentoString)))
            {
                Console.WriteLine(Environment.NewLine);
                Console.WriteLine("Data ou formato inválido!");
                Console.Write("Tecle <Enter> para retornar ao Menu...");
                Console.ReadKey();
                Console.Clear();
                return;
            }

            var nascimento = DateTime.ParseExact(nascimentoString, "dd/MM/yyyy", null);

            if (!Validacoes.ValidarFaixaEtaria(nascimento))
            {
                Console.WriteLine("");
                Console.WriteLine("Cadastro permitido somente para pessoas entre 16 e 120 anos!");
                Console.Write("Tecle <Enter> para retornar ao Menu...");
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
        private void Listar()
        {
            var clientes = _repositorio.RetornaListaAtualizada();

            Console.Clear();
            Console.WriteLine("Lista de clientes");
            Console.WriteLine("=================" + Environment.NewLine);

            foreach (var cliente in clientes.OrderBy(x => x.Nome))
            {
                Console.WriteLine($"Nome: {cliente.Nome.ToUpper()}");
                Console.WriteLine($"CPF: {cliente.CPF}");
                Console.WriteLine($"Data Nascimento: {cliente.Nascimento.ToString(format: "dd/MM/yyyy")}");
                Console.WriteLine("");
            }
            Console.Write("Tecle <Enter> para retornar ao Menu...");
            Console.ReadKey();
        }
        private void ExibePorCPF()
        {
            var clientes = _repositorio.RetornaListaAtualizada();

            Console.Clear();
            Console.WriteLine("Busca cliente pelo CPF");
            Console.WriteLine("======================" + Environment.NewLine);
            Console.Write("Informe o CPF: ");
            var CPFinformado = Console.ReadLine();

            if (!(Validacoes.ValidarCPF(CPFinformado)))
            {
                Console.WriteLine("");
                Console.WriteLine("CPF inválido!");
                Console.Write("Tecle <Enter> para retornar ao Menu...");
                Console.ReadKey();
                return;
            }

            CPFinformado = Convert.ToUInt64(CPFinformado).ToString(@"000\.000\.000\-00");

            if (clientes.Exists(x => x.CPF == CPFinformado))
            {

                foreach (var cliente in clientes)
                {
                    if (cliente.CPF == CPFinformado)
                    {
                        Console.WriteLine($"Nome: {cliente.Nome.ToUpper()}");
                        Console.WriteLine($"Data Nascimento: {cliente.Nascimento.ToString(format: "dd/MM/yyyy")}");
                        Console.WriteLine("");
                        break;
                    }
                }
            }
            else
            {
                Console.WriteLine("");
                Console.WriteLine("CPF não cadastrado!");
            }

            Console.Write("Tecle <Enter> para retornar ao Menu...");
            Console.ReadKey();
            return;
        }
        private void AniversariantesMes()
        {
            var clientes = _repositorio.RetornaListaAtualizada();

            Console.Clear();
            Console.WriteLine("Aniversariantes do mês");
            Console.WriteLine("=======================" + Environment.NewLine);

            clientes = clientes.FindAll(x => x.Nascimento.Month == DateTime.Now.Month);

            if (!(clientes.Count == 0))
            {
                foreach (var cliente in clientes.OrderBy(x => x.Nascimento.Day))
                {
                    Console.WriteLine($"Dia {cliente.Nascimento.Day} - {cliente.Nome.ToUpper()}");
                }
                Console.WriteLine(Environment.NewLine);
            }
            else
                Console.WriteLine("Não há aniversariantes este mês!");

            Console.Write("Tecle <Enter> para retornar ao Menu...");
            Console.ReadKey();
            return;
        }
    }
}
