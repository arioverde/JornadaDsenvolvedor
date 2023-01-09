using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aula_3_Atividade_Matriz1
{
    public class MediaNotas
    {
        double[] notasProvas = new double[5];
        public void Executar()
        {
            ExibeCabecalho();
            SolicitaNotaProvas();
            ExibeMediaNotas();
        }
        private void ExibeCabecalho()
        {
            Console.WriteLine("*************************************************");
            Console.WriteLine("***** Calculo da média de 5 notas de provas *****");
            Console.WriteLine("*************************************************");
        }
        private void SolicitaNotaProvas()
        {
            double nota;
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"Informe a nota da prova {i + 1}");
                nota = Convert.ToDouble(Console.ReadLine());
                notasProvas[i] = nota;
            }
        }
        private void ExibeMediaNotas()
        {
            double somaNota = 0, media;
            for (int i = 0; i < 5; i++)
            {
                somaNota = somaNota + notasProvas[i];
            }
            
            media = somaNota / 5;
            Console.WriteLine($"A média das notas é {media}");
        }
    }
}
