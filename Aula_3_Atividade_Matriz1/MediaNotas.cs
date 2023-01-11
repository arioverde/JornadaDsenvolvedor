using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aula_3_Atividade_Matriz1
{
    public class MediaNotas
    {
        public void Executar()
        {
            ExibeCabecalho();
            var alunos = SolicitaNotaProvas();
            if (alunos.Count == 0)
            {
                Console.WriteLine("As notas não foram informadas.");
                return;
            }
            ExibeMediaNotas(alunos);
        }
        private void ExibeCabecalho()
        {
            Console.WriteLine("*************************************************");
            Console.WriteLine("****** Calculo da média de notas de provas ******");
            Console.WriteLine("*************************************************");
        }
        private List<Aluno> SolicitaNotaProvas()
        {
            var alunos = new List<Aluno>();

            while (true)
            {
                Console.WriteLine("1 - Cadastrar Notas");
                Console.WriteLine("2 - Finalizar");

                var opcao = Convert.ToInt16(Console.ReadLine());
                if (opcao == 2)
                    break;

                var aluno = new Aluno();

                Console.Write("Informe a NOTA: ");
                aluno.Nota = Convert.ToInt16(Console.ReadLine());
                Console.Write("Informe o NOME do aluno: ");
                aluno.Nome = Console.ReadLine();

                alunos.Add(aluno);

                Console.Clear();
            }
            return alunos;
        }
        private void ExibeMediaNotas(List<Aluno> alunos)
        {
            var somaNotas = 0F;
            foreach (var aluno in alunos)
            {
                Console.Write($"{aluno.Nome}: {aluno.Nota}" + Environment.NewLine);
                somaNotas += aluno.Nota;
            }

            Console.WriteLine($"A média das notas é {somaNotas / alunos.Count}");
        }
    }
}
