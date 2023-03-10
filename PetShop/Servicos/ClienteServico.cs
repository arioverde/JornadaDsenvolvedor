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
                Console.WriteLine("");
                Console.WriteLine(" PET SHOP - Gerenciamento de Clientes");
                Console.WriteLine(" ====================================");
                Console.WriteLine("");
                Console.WriteLine(" Escolha uma opção:");
                Console.WriteLine("");
                Console.WriteLine(" 1 - Cadastrar clientes");
                Console.WriteLine(" 2 - Listar clientes");
                Console.WriteLine(" 3 - Buscar cliente pelo CPF");
                Console.WriteLine(" 4 - Listar aniversariantes do mês atual");
                Console.WriteLine(" 5 - Atualizar cadastro do cliente");
                Console.WriteLine(" 6 - FECHAR O PROGRAMA");
                var opcao = Console.ReadLine();

                try
                {
                    switch (opcao)
                    {
                        case "1":
                            Cadastrar();
                            break;
                        case "2":
                            Listar();
                            break;
                        case "3":
                            ExibirPorCPF();
                            break;
                        case "4":
                            ExibirAniversariantesMes();
                            break;
                        case "5":
                            Atualizar();
                            break;
                        case "6":
                            finaliza = true;
                            break;
                        default:
                            {
                                Console.Write("Opção inválida. Tecle <Enter> para tentar novamente...");
                                Console.ReadKey();
                                break;
                            }
                    }
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine("");
                    Console.WriteLine(ex.Message);
                    Console.Write("Tecle <Enter> para retornar ao Menu...");
                    Console.ReadKey();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("");
                    Console.WriteLine("Erro inesperado! Entre com contato com o Administrador e informe o erro abaixo:");
                    Console.WriteLine($"===> {ex.Message}");
                    Console.WriteLine("");
                    Console.Write("Tecle <Enter> para retornar ao Menu...");
                    Console.ReadKey();
                }
            }
        }
        #region (Métodos privados)
        private void Cadastrar()
        {
            Console.Clear();
            Console.WriteLine("Cadastro de clientes");
            Console.WriteLine("====================" + Environment.NewLine);
            Console.Write("Informe o nome: ");
            var nome = Console.ReadLine();
            if (!Validacoes.ValidarNome(nome, 3, 80))
                throw new InvalidOperationException("Nome inválido. O número de caracteres deve estar entre 3 e 80!");

            Console.Write("Informe o CPF: ");
            var CPF = Console.ReadLine();
            if (!(Validacoes.ValidarCPF(CPF)))
                throw new InvalidOperationException("CPF inválido!");

            CPF = Convert.ToUInt64(CPF).ToString(@"000\.000\.000\-00");

            if (_repositorio.ExisteCPF(CPF))
                throw new InvalidOperationException("CPF já cadastrado!");

            Console.Write("Informe a data de nascimento (formato 99/99/9999): ");
            var nascimentoString = Console.ReadLine();
            if (!(Validacoes.ValidarData(nascimentoString)))
                throw new InvalidOperationException("Data ou formato inválido!");

            var nascimento = DateTime.ParseExact(nascimentoString, "dd/MM/yyyy", null);
            if (!Validacoes.ValidarFaixaEtaria(nascimento))
                throw new InvalidOperationException("Cadastro permitido somente para pessoas entre 16 e 120 anos!");

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
        private void Atualizar()
        {
            var clientes = _repositorio.RetornaListaAtualizada();

            Console.Clear();
            Console.WriteLine("Atualizar o cadastro");
            Console.WriteLine("====================" + Environment.NewLine);
            Console.Write("Informe o CPF: ");
            var CPFinformado = Console.ReadLine();

            if (!(Validacoes.ValidarCPF(CPFinformado)))
                throw new InvalidOperationException("CPF inválido!");

            var CPFvalido = Convert.ToUInt64(CPFinformado).ToString(@"000\.000\.000\-00");

            if (!(_repositorio.ExisteCPF(CPFvalido)))
                throw new InvalidOperationException("CPF não cadastrado!");

            int posicao = clientes.FindIndex(x => x.CPF == CPFvalido);
            var cliente = clientes[posicao];

            var opcao = "";

            do
            {
                Console.Clear();

                Console.WriteLine("Dados atuais:");
                Console.WriteLine("===========================");
                Console.WriteLine($"CPF: {clientes[posicao].CPF}");
                Console.WriteLine($"Nome: {clientes[posicao].Nome.ToUpper()}");
                Console.WriteLine($"Data Nascimento: {clientes[posicao].Nascimento.ToString(format: "dd/MM/yyyy")}");
                Console.WriteLine("===========================");

                Console.WriteLine("");
                Console.WriteLine("Qual dado deseja alterar?" + Environment.NewLine);
                Console.WriteLine(" 1 - Nome");
                Console.WriteLine(" 2 - Data de Nascimento");
                Console.WriteLine(" 3 - SAIR");
                opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        clientes[posicao].Nome = AlterarNome();
                        break;
                    case "2":
                        clientes[posicao].Nascimento = AlterarDataNascimento();
                        break;
                    case "3":
                        break;
                    default:
                        continue;
                }
            } while (opcao != "3");

            _repositorio.Atualizar(clientes);
        }
        private string AlterarNome()
        {
            Console.Write("");
            Console.Write("Digite o nome: ");
            var nome = Console.ReadLine();
            if (!Validacoes.ValidarNome(nome, 3, 80))
                throw new InvalidOperationException("Nome inválido. O número de caracteres deve estar entre 3 e 80!");

            return nome;
        }
        private DateTime AlterarDataNascimento()
        {
            Console.Write("Altere a data de nascimento (formato 99/99/9999): ");
            var nascimentoString = Console.ReadLine();
            if (!(Validacoes.ValidarData(nascimentoString)))
                throw new InvalidOperationException("Data ou formato inválido!");

            var nascimento = DateTime.ParseExact(nascimentoString, "dd/MM/yyyy", null);
            if (!Validacoes.ValidarFaixaEtaria(nascimento))
                throw new InvalidOperationException("Cadastro permitido somente para pessoas entre 16 e 120 anos!");

            return nascimento;
        }
        private void ExibirPorCPF()
        {
            var clientes = _repositorio.RetornaListaAtualizada();

            Console.Clear();
            Console.WriteLine("Busca cliente pelo CPF");
            Console.WriteLine("======================" + Environment.NewLine);
            Console.Write("Informe o CPF: ");
            var CPFinformado = Console.ReadLine();

            if (!(Validacoes.ValidarCPF(CPFinformado)))
                throw new InvalidOperationException("CPF inválido!");

            var CPFvalido = Convert.ToUInt64(CPFinformado).ToString(@"000\.000\.000\-00");

            if (!(_repositorio.ExisteCPF(CPFvalido)))
                throw new InvalidOperationException("CPF não cadastrado!");

            Console.WriteLine("");
            foreach (var cliente in clientes)
            {
                if (cliente.CPF == CPFvalido)
                {
                    Console.WriteLine($"Nome: {cliente.Nome.ToUpper()}");
                    Console.WriteLine($"Data Nascimento: {cliente.Nascimento.ToString(format: "dd/MM/yyyy")}");
                    Console.WriteLine("");
                    break;
                }
            }

            Console.Write("Tecle <Enter> para retornar ao Menu...");
            Console.ReadKey();
            return;
        }
        private void ExibirAniversariantesMes()
        {
            var clientes = _repositorio.RetornaListaAtualizada();

            Console.Clear();
            Console.WriteLine($"Aniversariantes do mês de {DateTime.Now.ToString("MMMM")}");
            Console.WriteLine("===================================" + Environment.NewLine);

            clientes = clientes.FindAll(x => x.Nascimento.Month == DateTime.Now.Month);

            if (!(clientes.Count == 0))
            {
                foreach (var cliente in clientes.OrderBy(x => x.Nascimento.Day))
                {
                    Console.WriteLine($"Dia {cliente.Nascimento.Day.ToString("D2")} - {cliente.Nome.ToUpper()}");
                }
                Console.WriteLine(Environment.NewLine);
            }
            else
                Console.WriteLine("Não há aniversariantes este mês!");

            Console.Write("Tecle <Enter> para retornar ao Menu...");
            Console.ReadKey();
            return;
        }
        #endregion
    }
}
