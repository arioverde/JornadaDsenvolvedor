using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aula_3_Atividade_1___Salario
{
    public class PagamentoFuncionario
    {
        private string? NomeVendedor { get; set; }
        private double SalarioFixo { get; set; }
        private double TotalVendas { get; set; }
        private double SalarioFinal { get; set; }

        public void Executar()
        {
            ExibeCabecalho();
            SolicitaDadosSalario();
            CalculaSalario();
            ExibeRelatorio();
        }
        private void ExibeCabecalho()
        {
            Console.WriteLine("*********************************");
            Console.WriteLine("******* Cálculo de Salário ******");
            Console.WriteLine("*********************************");
        }
        private void SolicitaDadosSalario()
        {
            Console.WriteLine("Informe o nome do vendedor: ");
            NomeVendedor = Console.ReadLine();
            Console.WriteLine("Informe o salário fixo do vendedor: ");
            SalarioFixo = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Informe o total de vendas: ");
            TotalVendas = Convert.ToDouble(Console.ReadLine());
        }
        private void CalculaSalario()
        {
            SalarioFinal = (TotalVendas * 0.15) + SalarioFixo;
        }
        private void ExibeRelatorio()
        {
            Console.WriteLine("*********************************");
            Console.WriteLine("******* Folha de Pagamento ******");
            Console.WriteLine("*********************************");
            Console.WriteLine($"Nome do vendedor {NomeVendedor}");
            Console.WriteLine($"Salário Fixo: {SalarioFixo}");
            Console.WriteLine($"Salário Final: {SalarioFinal}");
        }
    }
}
