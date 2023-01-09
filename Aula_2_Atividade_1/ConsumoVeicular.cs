using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aula_2_Atividade_1
{
    public class ConsumoVeicular
    {
        public void Menu()
        {
            Console.WriteLine("=========================================================");
            Console.WriteLine("===== CÁLCULO DE CONSUMO MÉDIO E AUTONOMIA VEICULAR =====");
            Console.WriteLine("=========================================================\n");

            Console.WriteLine("Escolha o cálculo desejado:\n");
            Console.WriteLine("1 - Consumo médio");
            Console.WriteLine("2 - Autonomia");
            Console.WriteLine("3 - SAIR\n");

            Console.Write("Opção: ");
            int opcao = Convert.ToInt32(Console.ReadLine());

            if (opcao == 1)
            {
                Consumo();
            }
            else if (opcao == 2)
            {
                Autonomia();
            }
            else if (opcao == 3)
            {
                return;
            }
        }
        private void Consumo()
        {
            Console.WriteLine("======== CALCULO DO CONSUMO MÉDIO ========");
            Console.WriteLine("Informe a kilometragem com o tanque cheio:");
            int kmInicial = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Informe a kilometragem com meio tanque:");
            int kmFinal = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Após completar o tanque, informe quantidade abastecida:");
            double combustivelConsumido = Convert.ToDouble(Console.ReadLine());
            double consumoMedio = (kmFinal - kmInicial) / combustivelConsumido;
            Console.WriteLine($"O consumo médio do veículo é {consumoMedio}");
        }
        private void Autonomia()
        {
            Console.WriteLine("======== CALCULO DA AUTONOMIA ========");
            Console.WriteLine("Informe a quantidade de combustível disponível: ");
            double combustivelDisponivel = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Informe o consumo médio (Km/L):");
            double consumoMedio = Convert.ToDouble(Console.ReadLine());
            double distanciaMaxima = consumoMedio * combustivelDisponivel;
            Console.WriteLine($"Distância máxima a percorrer: {distanciaMaxima} Km");
        }
    }
}
