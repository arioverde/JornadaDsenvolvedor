programa
{
	
	cadeia vetorNome[10], nomeMaisVelho
	inteiro vetorIdade[10], idadeMaisVelho, comparaIdade
	
	funcao inicio()
	{	
		ExibeCabecalho()
		SolicitaNomeIdade()
		ComparaIdade()
		ApresentaMaisVelho()
	}

	funcao ExibeCabecalho()
	{
		escreva("************************************************\n")
		escreva("Mostra o mais velho(a) de um grupo de 10 pessoas\n")
		escreva("************************************************\n")
	}

	funcao SolicitaNomeIdade()
	{
		para(inteiro posicao = 0;posicao < 10;posicao++)
		{
			escreva("\nInforme o nome da "+ (posicao + 1)+ "ª pessoa: ")
			cadeia nome
			leia(nome)
			vetorNome[posicao] = nome
			
			escreva("Informe a idade: ")
			inteiro idade
			leia(idade)
			vetorIdade[posicao] = idade
		}
	}

	funcao ComparaIdade()
	{					
		comparaIdade = vetorIdade[0]
		
		para(inteiro posicao = 0;posicao < 10;posicao++)
		{																		
			se(comparaIdade <= vetorIdade[posicao])
			{
				idadeMaisVelho = vetorIdade[posicao]					
				nomeMaisVelho = vetorNome[posicao]	
				comparaIdade = vetorIdade[posicao]								
			}									
		}	
	}

	funcao ApresentaMaisVelho()
	{
		escreva("\nO mais velho é " + nomeMaisVelho + " com " + idadeMaisVelho + " anos.")	
	}
	
}
/* $$$ Portugol Studio $$$ 
 * 
 * Esta seção do arquivo guarda informações do Portugol Studio.
 * Você pode apagá-la se estiver utilizando outro editor.
 * 
 * @POSICAO-CURSOR = 463; 
 * @PONTOS-DE-PARADA = ;
 * @SIMBOLOS-INSPECIONADOS = {vetorNome, 4, 8, 9}-{nomeMaisVelho, 4, 23, 13}-{vetorIdade, 5, 9, 10}-{idadeMaisVelho, 5, 25, 14}-{comparaIdade, 5, 41, 12};
 * @FILTRO-ARVORE-TIPOS-DE-DADO = inteiro, real, logico, cadeia, caracter, vazio;
 * @FILTRO-ARVORE-TIPOS-DE-SIMBOLO = variavel, vetor, matriz, funcao;
 */