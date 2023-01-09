using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aula_3_Atividade_Matriz3
{
    public class MaisVelho
    {
        private int[] Idades = new int[10];
        private string?[] Nomes = new string[10];
        public void Executar()
        {
            ExibeCabecalho();
            SolicitaNomeIdade();
            ApresentaMaisVelho();
        }
        private void ExibeCabecalho()
        {
            Console.WriteLine("************************************************");
            Console.WriteLine("Mostra o mais velho(a) de um grupo de 10 pessoas");
            Console.WriteLine("************************************************");
        }
        private void SolicitaNomeIdade()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.Write($"Informe o nome da {i + 1}ª pessoa: ");
                Nomes[i] = Console.ReadLine();

                Console.Write($"Informe a idade: ");
                Idades[i] = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("");
            }
        }
        private void ApresentaMaisVelho()
        {
            int comparaIdade = Idades[0], idadeMaisVelho;
            string? nomeMaisVelho = Nomes[0];

            for (int i = 0; i < 10; i++)
            {
                if (comparaIdade <= Idades[i])
                {
                    idadeMaisVelho = Idades[i];
                    nomeMaisVelho = Nomes[i];
                    comparaIdade = Idades[i];
                }                
            }
            Console.WriteLine($"O(A) mais velho(a) é {nomeMaisVelho}");
        }
    }
}
