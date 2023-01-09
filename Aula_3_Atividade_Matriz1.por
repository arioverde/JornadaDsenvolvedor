programa
{
	real notasProvas[5], nota, media
	real somaNota = 0.0		
	
	funcao inicio()
	{		
		ExibeCabecalho()
		SolicitaNotaProvas()
		SomaNotas()
		ExibeMediaNotas()	
	}

	funcao ExibeCabecalho()
	{
		escreva("*************************************************\n")
		escreva("***** Calculo da média de 5 notas de provas *****\n")
		escreva("*************************************************\n\n")
	}

	funcao SolicitaNotaProvas()
	{
		para (inteiro posicao = 0; posicao < 5; posicao++)
		{
			escreva ("Informe a nota da prova " + (posicao + 1) + ": ")
			leia(nota)
			notasProvas[posicao] = nota
		}		
	}

	funcao SomaNotas()
	{
		para (inteiro posicao = 0; posicao < 5; posicao++)
		{
			somaNota = somaNota + notasProvas[posicao]
		}
	}
	
	funcao ExibeMediaNotas()
	{	
		media = somaNota / 5
		escreva("\nA média das notas é: " + media)
	}	
}
/* $$$ Portugol Studio $$$ 
 * 
 * Esta seção do arquivo guarda informações do Portugol Studio.
 * Você pode apagá-la se estiver utilizando outro editor.
 * 
 * @POSICAO-CURSOR = 167; 
 * @PONTOS-DE-PARADA = ;
 * @SIMBOLOS-INSPECIONADOS = ;
 * @FILTRO-ARVORE-TIPOS-DE-DADO = inteiro, real, logico, cadeia, caracter, vazio;
 * @FILTRO-ARVORE-TIPOS-DE-SIMBOLO = variavel, vetor, matriz, funcao;
 */