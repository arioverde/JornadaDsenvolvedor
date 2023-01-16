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
        private readonly string _caminhoArquivo = $"C:{Path.DirectorySeparatorChar}DadosPetShop{Path.DirectorySeparatorChar}Clientes.csv";
        private List<Cliente> clientes = new List<Cliente>();
        public ClienteRepositorio()
        {
            //if (!File.Exists(_caminhoArquivo))
            //{
            //    var file = File.Create(_caminhoArquivo);
            //    file.Close();
            //}
        }

        public void Inserir(Cliente cliente)
        {
            var sw = new StreamWriter(_caminhoArquivo, true);

            string registro = $"{cliente.Nome};{cliente.CPF};{cliente.Nascimento}";

            sw.WriteLine(registro);
            sw.Close();

            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Cliente cadastrado com sucesso!");
            Console.Write("Tecle <Enter> para retornar ao Menu...");
            Console.ReadKey();
        }
        private void Carregar()
        {
            clientes.Clear();

            var sr = new StreamReader(_caminhoArquivo);
            string? linha;

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
        public List<Cliente> RetornaListaAtualizada()
        {
            Carregar();
            return clientes;
        }

        public bool ExisteCPF(string CPF)
        {
            Carregar();
            if (clientes.Exists(x => x.CPF == CPF))
                return true;

            return false;
        }
    }
}

