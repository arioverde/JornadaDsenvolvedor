using PetShop.Modelos;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace PetShop.Utilitarios
{
    internal static class Validacoes
    {
        public static bool ValidarCPF(string? texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
                  return false;

            return true;
        }

        public static bool ValidarNome(string? texto, short min, short max)
        {
            if (string.IsNullOrWhiteSpace(texto)
                || texto.Trim().Length < min
                || texto.Trim().Length > max)
                return false;

            return true;
        }
        public static bool ValidarFaixaEtaria(DateTime nascimento)
        {
            var idade = DateTime.Now.Year - nascimento.Year;
            if (DateTime.Now.DayOfYear < nascimento.DayOfYear)
                idade--;

            if (!(idade >= 16) & (idade < 120))
                return false;

            return true;           
        }
        public static bool ValidaFormatoData(string? data)
        {
            return DateTime.TryParseExact(data, "dd/MM/yyyy", new CultureInfo("pt-BR"), DateTimeStyles.None, out _);  
        }
    }
}

