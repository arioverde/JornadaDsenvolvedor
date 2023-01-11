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
                Console.WriteLine("Informe a nota ou digite PARE para finalizar");
                var opcao = Console.ReadLine();

                if (opcao.Trim().ToUpper() == "PARE")
                    break;

                var seValorCorreto = ValidarSeNumeroFloat(opcao);
               
                if (!seValorCorreto)
                {
                    Console.Clear();
                    Console.WriteLine("Número inválido, tecle <Enter> para tentar novamente.");
                    Console.ReadKey();
                    continue;
                }             
                    var aluno = new Aluno();

                    aluno.Nota = Convert.ToDouble(opcao);
                    Console.Write("Informe o NOME do aluno: ");
                    aluno.Nome = Console.ReadLine();

                    alunos.Add(aluno);

                Console.Clear();
            }
            return alunos;
        }
        private void ExibeMediaNotas(List<Aluno> alunos)
        {
            var somaNotas = 0.0;
            foreach (var aluno in alunos)
            {
                Console.Write($"{aluno.Nome}: {aluno.Nota}" + Environment.NewLine);
                somaNotas += aluno.Nota;
            }

            Console.WriteLine($"A média das notas é {somaNotas / alunos.Count}");
        }
        private bool ValidarSeNumeroFloat(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
                return false;

            if (texto.Contains("."))
                return false;

            var isDouble = double.TryParse(texto, out _);

            if (!isDouble)
                return false;

            return true;              
        }
    }
}
