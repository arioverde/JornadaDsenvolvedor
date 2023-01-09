programa
{	
	inteiro vetor[15]
	real resto
	
	funcao inicio()
	{
		ExibeCabecalho()
		SolicitaNumeros()
		ApresentaSomentePositivos()
	}

	funcao ExibeCabecalho()
	{
		escreva("******************************************************\n")
		escreva("*** Recebe 15 números e exibe somente os positivos ***\n")
		escreva("******************************************************\n\n")
	}

	funcao SolicitaNumeros()
	{
		inteiro numero = 0
		para (inteiro posicao = 0; posicao < 15; posicao++)
		{
			escreva("Informe o " + (posicao + 1) + "º número: ")
			leia(numero)
			vetor[posicao] = numero
		}
	}
	
	funcao ApresentaSomentePositivos()
	{				
		escreva("\nOs números positivos são: \n")
		
		para (inteiro posicao = 0; posicao < 15; posicao++)
		{
			se(vetor[posicao] > 0)
			{
				escreva("\n" + vetor[posicao])
			}
		}
	}	
}
/* $$$ Portugol Studio $$$ 
 * 
 * Esta seção do arquivo guarda informações do Portugol Studio.
 * Você pode apagá-la se estiver utilizando outro editor.
 * 
 * @POSICAO-CURSOR = 597; 
 * @PONTOS-DE-PARADA = ;
 * @SIMBOLOS-INSPECIONADOS = ;
 * @FILTRO-ARVORE-TIPOS-DE-DADO = inteiro, real, logico, cadeia, caracter, vazio;
 * @FILTRO-ARVORE-TIPOS-DE-SIMBOLO = variavel, vetor, matriz, funcao;
 */