using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aula_3_Atividade_Matriz2
{
    public class ExibeNumerosPositivos
    {
        public void Executar()
        {
            ExibeCabecalho();
            var numeros = SolicitaNumeros();
            ApresentaSomentePositivos(numeros);
        }
        private void ExibeCabecalho()
        {
            Console.WriteLine("******************************************************");
            Console.WriteLine("*** Recebe 15 números e exibe somente os positivos ***");
            Console.WriteLine("******************************************************");
        }
        private List<int> SolicitaNumeros()
        {
            var numeros = new List<int>();

            for (int i = 0; i < 15; i++)
            {
                Console.WriteLine($"Informe o {i + 1}º número:");
                numeros.Add(Convert.ToInt32(Console.ReadLine()));
            }

            return numeros;
        }
        private void ApresentaSomentePositivos(List<int> numeros)
        {
            Console.WriteLine("Os números positivos são:");
            foreach (var numero in numeros)
            {
                if (numero < 0)
                    continue;
                Console.WriteLine(numero);
            }
        }
    }
}
