using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aula_3_Atividade_Matriz4
{
    public class LucroProdutos
    {
        public void Executar()
        {
            ExibeCabecalho();
            Menu();
        }
        private void ExibeCabecalho()
        {
            Console.WriteLine("****************************************");
            Console.WriteLine("***** Cálculo de lucro sobre vendas*****");
            Console.WriteLine("****************************************");
        }
        private void Menu()
        {
            Console.WriteLine("Escolha uma opção:");
            Console.WriteLine("1 - Informar dados da venda");
            Console.WriteLine("2 - Finalizar");

            int opcao = Convert.ToInt32(Console.ReadLine());

            if (opcao == 1)
            {
                SolicitaDadosVenda();
            }
            else if (opcao == 2)
            {
                return;
            }
            else
                Menu();
        }
        private void SolicitaDadosVenda()
        {
            int quantidadeProdutos;

            Console.Write("Quantos produtos deseja calcular o lucro? ");
            quantidadeProdutos = Convert.ToInt32(Console.ReadLine());
            Console.Clear();

            string[] produtos = new string[quantidadeProdutos];
            double[] lucroProduto = new double[quantidadeProdutos]; 

            for (int i = 0; i < quantidadeProdutos; i++)
            {
                Console.Write($"Informe o nome do {i + 1}º produto: ");
                produtos[i] = Console.ReadLine();

                Console.Write($"Informe o preço de compra de {produtos[i]}: ");
                double precoCompra = Convert.ToDouble(Console.ReadLine());

                Console.Write($"Informe o preço de venda de {produtos[i]}: ");
                double precoVenda = Convert.ToDouble(Console.ReadLine());

                Console.Write($"Informe a quantidade vendida de {produtos[i]}: ");
                double quantidadeVenda = Convert.ToInt32(Console.ReadLine());

                lucroProduto[i] = quantidadeVenda * (precoVenda - precoCompra);

                Console.WriteLine("");
            }

            double lucroTotal = 0;
            Console.WriteLine("Lucro por produto: " + Environment.NewLine);

            for (int i = 0; i < quantidadeProdutos; i++)
            {              
                Console.WriteLine($"{produtos[i]}: {lucroProduto[i]}");
            }

            Console.WriteLine("");
            Console.WriteLine($"Lucro total: {lucroTotal}");
        }
    }
}
