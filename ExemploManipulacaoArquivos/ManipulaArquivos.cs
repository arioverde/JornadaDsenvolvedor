using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ExemploManipulacaoArquivos
{
    internal class ManipulaArquivos
    {
        // Path.DirectorySeparatorChar => utiliza o separador da plataforma atual, evita conflito windows vs linux, por exemplo
        string caminhoEntrada = $"C:{Path.DirectorySeparatorChar}RumoAtividades{Path.DirectorySeparatorChar}ExemploManipulacaoArquivos{Path.DirectorySeparatorChar}Entrada{Path.DirectorySeparatorChar}Sample.txt";
        string caminhoSaida = $"C:{Path.DirectorySeparatorChar}RumoAtividades{Path.DirectorySeparatorChar}ExemploManipulacaoArquivos{Path.DirectorySeparatorChar}Saida{Path.DirectorySeparatorChar}Sample.txt";
        // abre, lê e fecha o arquivo de texto
        public void Leitura()
        {         
            String line;
            try
            {
                //Passe o caminho do arquivo e o nome do arquivo para o construtor StreamReader
                StreamReader sr = new StreamReader(caminhoEntrada);
                //Lê a primeira linha do texto
                line = sr.ReadLine();            
                //Continue a ler até chegar ao final do arquivo
                while (line != null)
                {
                    //escreva a linha na janela do console
                    Console.WriteLine(line);
                    //Leia a próxima linha
                    line = sr.ReadLine();
                }
                //fecha o arquivo
                sr.Close();
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }

        }
        // abre, grava e fecha o arquivo de texto
        public void Gravacao()
        {
            try
            {
                //Passe o caminho do arquivo e o nome do arquivo para o Construtor StreamWriter (segundo parametro 'true', acrescenta dados)
                StreamWriter sw = new StreamWriter(caminhoSaida);
                //Escreva uma linha de texto
                sw.WriteLine("inserido a linha 1");
                //Escreva uma segunda linha de texto
                sw.WriteLine("inserido a linha 2");
                //fecha o arquivo
                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }
        }
        public void MoveArquivo()
        {
            if (File.Exists(caminhoEntrada))
                File.Delete(caminhoEntrada);
            // para manter arquivo atualizado, iremos mover arquivo da saída (atualizado) para a entrada   
            File.Move(caminhoSaida, caminhoEntrada);
        }

    }
}
