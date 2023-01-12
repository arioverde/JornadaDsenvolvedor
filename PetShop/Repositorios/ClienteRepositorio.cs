using PetShop.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Repositorios
{
    using Modelos;
    internal class ClienteRepositorio
    {
        private readonly string _caminhoArquivo = $"C:{Path.DirectorySeparatorChar}RumoAtividades{Path.DirectorySeparatorChar}PetShop{Path.DirectorySeparatorChar}Clientes.csv";
        private List<Cliente> clientes = new List<Cliente>();

        // se arquivo base não existe, deve ser criado
        public ClienteRepositorio()
        {
            if (!File.Exists(_caminhoArquivo))
            {
                var file = File.Create(_caminhoArquivo);
                file.Close();
            }
        }
        public void Cadastro()
        {
            StreamWriter sw = new StreamWriter(_caminhoArquivo, true);
            var cliente = new Cliente();
            string nome = "Carlos Santana3";
            string CPF = "45697868598";
            var nascimento = new DateTime(1965, 09, 15);

            string registro = $"{nome};{CPF};{nascimento}";

            sw.WriteLine(registro);
            sw.Close();

        }
        private void Leitura()
        {
            StreamReader sr = new StreamReader(_caminhoArquivo);
            string? linha;
            clientes.Clear();

            while ((linha = sr.ReadLine()) != null)
            {
                var colunas = linha.Split(';');
                var cliente = new Cliente();
                cliente.Nome = colunas[0];
                cliente.CPF = colunas[1];
                cliente.Nascimento = Convert.ToDateTime(colunas[2]);

                clientes.Add(cliente);
            }
            sr.Close();
        }
        public void Listar()
        {
            Leitura();
            foreach (var cliente in clientes)
            {
                Console.WriteLine(cliente.Nome.ToUpper());
                Console.WriteLine(cliente.CPF);
                Console.WriteLine(cliente.Nascimento.ToString(format: "dd/MM/yyyy"));
                Console.WriteLine("");
            }
        }
        public void ExibePorCPF(string CPF)
        {
            var cliente = new Cliente();

            if ((cliente = BuscaPorCPF(CPF)) == null)
                Console.WriteLine("CPF não cadastrado");
            else
            {
                Console.WriteLine(cliente.Nome.ToUpper());
                Console.WriteLine(cliente.CPF);
                Console.WriteLine(cliente.Nascimento);
            }
        }
        private Cliente BuscaPorCPF(string CPF)
        {
            Leitura();
            foreach (var cliente in clientes)
            {
                if (cliente.CPF == CPF)
                    return cliente;
            }
            return null;
        }
        public void AniversariantesMes()
        {
            Leitura();
            Console.WriteLine("Aniversariantes do mês: ");
            Console.WriteLine("=======================");

            clientes = clientes.OrderBy(x => x.Nascimento.Day).ToList();

            int count = 0;
            int idade;
            foreach (var cliente in clientes)
            {                            
                if (cliente.Nascimento.Month == DateTime.Now.Month)
                {
                    // impreciso. Revisar.
                    idade = DateTime.Now.Year - cliente.Nascimento.Year;
                    Console.WriteLine($"Dia {cliente.Nascimento.Day} - {cliente.Nome.ToUpper()} - {idade} anos");
                    count++;
                }
            }
            if (count == 0)
                Console.WriteLine("Não há aniversariantes no mês corrente.");
        }
    }
}

