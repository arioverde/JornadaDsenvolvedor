using System.Globalization;

namespace Exercicio_Avaliativo_01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //======================== PROGRAMA 1 ========================================

            Console.WriteLine("Digite o valor em reais:");
            var real = Convert.ToDecimal(Console.ReadLine());

            var dolar = real / 5.10M;
            var euro = real / 5.50M;
            var pesosArgentinos = real / 0.028M;
            var bahtTailandes = real / 0.15M;

            Console.WriteLine($"{real} reais equivalem a: {dolar.ToString("F")} dólares");
            Console.WriteLine($"{real} reais equivalem a: {euro.ToString("F")} euros");
            Console.WriteLine($"{real} reais equivalem a: {pesosArgentinos.ToString("F")} pesos argentinos");
            Console.WriteLine($"{real} reais equivalem a: {bahtTailandes.ToString("F")} Baht Tailandês");

          
            //======================== PROGRAMA 2 ========================================
            
            try
            {
                Console.Write("Informe a idade do nadador: ");

                int idadeNadador = Convert.ToInt32(Console.ReadLine());
                if ((idadeNadador < 5) || (idadeNadador > 25))
                    throw new Exception($"A idade de {idadeNadador} anos está fora da faixa permitida.");

                if ((idadeNadador >= 5) & (idadeNadador <= 7))
                    Console.WriteLine("Infantil A");

                if ((idadeNadador >= 8) & (idadeNadador <= 10))
                    Console.WriteLine("Infantil B");

                if ((idadeNadador >= 11) & (idadeNadador <= 13))
                    Console.WriteLine("Juvenil A");

                if ((idadeNadador >= 14) & (idadeNadador <= 17))
                    Console.WriteLine("Juvenil B");

                if ((idadeNadador >= 18) & (idadeNadador <= 25))
                    Console.WriteLine("Senior");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine("Tecle <Enter> para continuar ...");
            Console.ReadKey();
            Console.Clear();


            //======================== PROGRAMA 3 ========================================

            var nivel1 = 12.00;
            var nivel2 = 18.00;
            var nivel3 = 25.00;

            Console.Write("Informe o nível do professor (1,2 ou 3): ");
            var nivelInformado = Console.ReadLine();

            Console.Write("Informe o tempo trabalhodo em horas: ");
            var horasTrabalhadas = Convert.ToDouble(Console.ReadLine());
            var pagamento = 0.0;
            Console.WriteLine(Environment.NewLine);

            switch (nivelInformado)
            {
                case "1":
                    pagamento = nivel1 * horasTrabalhadas;
                    Console.WriteLine($"Pagamento professor Nível 1 ({nivel1.ToString("C")}/hora):");
                    Console.WriteLine("============================================");
                    Console.WriteLine($"Horas trabalhadas: {horasTrabalhadas}");
                    Console.WriteLine($"Valor a receber: {pagamento.ToString("C")}");
                    break;

                case "2":
                    pagamento = nivel2 * horasTrabalhadas;
                    Console.WriteLine($"Pagamento professor Nível 2 ({nivel2.ToString("C")}/hora):");
                    Console.WriteLine("============================================");
                    Console.WriteLine($"Horas trabalhadas: {horasTrabalhadas}");
                    Console.WriteLine($"Valor a receber: {pagamento.ToString("C")}");
                    break;

                case "3":
                    pagamento = nivel3 * horasTrabalhadas;
                    Console.WriteLine($"Pagamento professor Nível 3 ({nivel3.ToString("C")}/hora):");
                    Console.WriteLine("============================================");
                    Console.WriteLine($"Horas trabalhadas: {horasTrabalhadas}");
                    Console.WriteLine($"Valor a receber: {pagamento.ToString("C")}");
                    break;

                default:
                    Console.WriteLine("Informe o nível correto");
                    break;
            }


            //======================== PROGRAMA 4 ========================================

            Console.Write("Informe o preço do Kilo Watt: ");
            var valorKiloWatt = Convert.ToDouble(Console.ReadLine());

            Console.Write("Informe a potência do equipamento: ");
            var potencia = Convert.ToInt32(Console.ReadLine());

            Console.Write("Informe o tempo ligado por dia: ");
            var tempoLigadoPorDia = Convert.ToDouble(Console.ReadLine());

            Console.Write("Informe quantos dias fica ligado por mês: ");
            var diasLigadoPorMes = Convert.ToInt32(Console.ReadLine());

            var consumoDia = potencia * tempoLigadoPorDia / 1000;
            var consumoMes = consumoDia * diasLigadoPorMes;
            var totalConta = consumoMes * valorKiloWatt;

            Console.WriteLine(Environment.NewLine);
            Console.WriteLine($"Total a pagar pelo uso do equipamento: {totalConta.ToString("C")}");
        }
    }
}