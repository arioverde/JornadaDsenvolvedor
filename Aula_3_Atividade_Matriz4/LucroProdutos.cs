using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aula_3_Atividade_Matriz4
{
    public class LucroProdutos
    {
        private string[] Produto = new string[3];
        private int[] QuantidadeVenda = new int[3];
        private double[] PrecoCompra = new double[3];
        private double[] PrecoVenda = new double[3];

        public void Executar()
        {
            Produto[0] = "Ração";
            Produto[1] = "Petisco";
            Produto[2] = "Shampoo";

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
                ExibeLucro();
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
            for (int i = 0; i < 3; i++)
            {
                Console.Write($"Informe o preço de compra de {Produto[i]}: ");
                PrecoCompra[i] = Convert.ToDouble(Console.ReadLine());

                Console.Write($"Informe o preço de venda de {Produto[i]}: ");
                PrecoVenda[i] = Convert.ToDouble(Console.ReadLine());

                Console.Write($"Informe a quantidade vendida de {Produto[i]}: ");
                QuantidadeVenda[i] = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("");
            }
        }
        private void ExibeLucro()
        {
            double totalCompra, totalVenda, lucroBruto, valorImposto, lucroLiquido, lucroTotal = 0;
            double[] lucroProduto = new double[3];

            for (int i = 0; i < 3; i++)
            {                            
                totalCompra = QuantidadeVenda[i] * PrecoCompra[i];
                totalVenda = QuantidadeVenda[i] * PrecoVenda[i];
                lucroBruto = totalVenda - totalCompra;
                valorImposto = lucroBruto * 0.15;
                lucroLiquido = lucroBruto - valorImposto;
                lucroProduto[i] = lucroLiquido;
                lucroTotal = lucroTotal + lucroProduto[i];
            }

            Console.Clear();
            Console.WriteLine("Lucro por produto vendido (Descontado imposto de 15%):");
            Console.WriteLine("");

            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine($"{Produto[i]}: {lucroProduto[i]}");             
            }

            Console.WriteLine("");
            Console.WriteLine($"Lucro total: {lucroTotal}");
        }
    }
}
