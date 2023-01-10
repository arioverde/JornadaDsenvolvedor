using System;
using System.Collections.Generic;
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

            string[] produtos = new string[quantidadeProdutos];
            int[] quantidadeVenda = new int[quantidadeProdutos];
            double[] precoCompra = new double[quantidadeProdutos];
            double[] precoVenda = new double[quantidadeProdutos];
            double lucroTotal = 0;

            for (int i = 0; i < quantidadeProdutos; i++)
            {
                Console.Write($"Informe o nome do {i + 1}º produto: ");
                produtos[i] = Console.ReadLine();

                Console.Write($"Informe o preço de compra de {produtos[i]}: ");
                precoCompra[i] = Convert.ToDouble(Console.ReadLine());

                Console.Write($"Informe o preço de venda de {produtos[i]}: ");
                precoVenda[i] = Convert.ToDouble(Console.ReadLine());

                Console.Write($"Informe a quantidade vendida de {produtos[i]}: ");
                quantidadeVenda[i] = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("");
            }

            Console.WriteLine("Lucro por produto: " + Environment.NewLine);

            for (int i = 0; i < quantidadeProdutos; i++)
            {
                double lucro = quantidadeVenda[i] * (precoVenda[i] - precoCompra[i]);
                lucroTotal += lucro;
                Console.WriteLine($"{produtos[i]}: {lucro}");
            }

            Console.WriteLine("");
            Console.WriteLine($"Lucro total: {lucroTotal}");

        }

    }
}
