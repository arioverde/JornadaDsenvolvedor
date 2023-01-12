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
                File.Create(_caminhoArquivo);
        }
        public void Cadastro()
        {

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
                cliente.CPF = Convert.ToInt64(colunas[1]);
                cliente.Aniversario = Convert.ToDateTime(colunas[2]);

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
                Console.WriteLine(cliente.Aniversario);
                Console.WriteLine("");

            }
        }
        public void PorCPF(long CPF)
        {
            Leitura();
            clientes.Contains(x)

        }
        public void AniversariantesMes()
        {
            Leitura();
        }
    }
}
