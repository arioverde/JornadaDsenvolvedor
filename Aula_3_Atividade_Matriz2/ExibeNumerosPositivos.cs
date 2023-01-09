using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aula_3_Atividade_Matriz2
{
    public class ExibeNumerosPositivos
    {
        int[] numeros = new int[15];
        public void Executar()
        {
            ExibeCabecalho();
            SolicitaNumeros();
            ApresentaSomentePositivos();
        }       
        private void ExibeCabecalho()
        {
            Console.WriteLine("******************************************************");
            Console.WriteLine("*** Recebe 15 números e exibe somente os positivos ***");
            Console.WriteLine("******************************************************");
        }
        private void SolicitaNumeros()
        {
            int numero;

            for (int i = 0; i < 15; i++)
            {
                Console.WriteLine($"Informe o {i+1}º número:");
                numero = Convert.ToInt32(Console.ReadLine());
                numeros[i] = numero;

            }
        }
        private void ApresentaSomentePositivos()
        {
            Console.WriteLine("Os números positivos são:");
            for (int i = 0; i < 15; i++)
            {
                if (numeros[i] < 0)
                    continue;
                Console.WriteLine(numeros[i]);
            }
        }
    }
}
