using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aula_3_Atividade_Matriz3
{
    public class MaisVelho
    {
        public List<Pessoa> Pessoas = new List<Pessoa>();

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
            for (int i = 0; i < 3; i++)
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
            int comparaIdade = pessoas[0].Idade, idadeMaisVelho;
            string? nomeMaisVelho = pessoas[0].Nome;

            foreach (var pessoa in pessoas)
            {
                if (comparaIdade <= pessoa.Idade)
                {
                    nomeMaisVelho = pessoa.Nome;
                    comparaIdade = pessoa.Idade;
                }
            }
            Console.WriteLine($"O(A) mais velho(a) é {nomeMaisVelho}");
        }
    }
}
