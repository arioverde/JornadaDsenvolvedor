using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aula_3_Atividade_Matriz3
{
    public class MaisVelho
    {
        public void Executar()
        {
            ExibeCabecalho();
            List<Pessoa> pessoas = SolicitaNomeIdade();
            ApresentaMaisVelho(pessoas);
        }
        private void ExibeCabecalho()
        {
            Console.WriteLine("************************************************");
            Console.WriteLine("Mostra o mais velho(a) de um grupo de 10 pessoas");
            Console.WriteLine("************************************************");
        }
        private List<Pessoa> SolicitaNomeIdade()
        {
            List<Pessoa> Pessoas = new List<Pessoa>();

            for (int i = 0; i < 10; i++)
            {
                Pessoa pessoa = new Pessoa();

                Console.Write($"Informe o nome da {i + 1}ª pessoa: ");
                pessoa.Nome = Console.ReadLine();

                Console.Write($"Informe a idade: ");
                pessoa.Idade = Convert.ToInt32(Console.ReadLine());

                Pessoas.Add(pessoa);
            }
            return Pessoas;
        }
        private void ApresentaMaisVelho(List<Pessoa> pessoas)
        {
            int comparaIdade = 0;
            string nomeMaisVelho = "";

            foreach (var pessoa in pessoas)
            {
                if (comparaIdade < pessoa.Idade)
                {
                    nomeMaisVelho = pessoa.Nome;
                    comparaIdade = pessoa.Idade;
                }
            }
            // o código acima poderia ser resumido em apenas uma linha, com expressão lambda:
            // Console.WriteLine($"O(A) mais velho(a) é {pessoas.OrderByDescending(x => x.Nome).First().Nome}");

            Console.WriteLine($"O(A) mais velho(a) é {nomeMaisVelho}");
        }
    }
}
