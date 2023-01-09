programa
{
	cadeia nomeVendedor
	real salarioFixo, salarioFinal, totalVendas
	
	funcao inicio()
	{
		ExibeCabecalho()
		SolicitaDadosSalario()
		CalculaSalario()
		ExibeRelatorio()		
	}
	
	funcao ExibeCabecalho()
	{
		escreva("*********************************\n")	
		escreva("******* Cálculo de Salário ******\n")		
		escreva("*********************************\n\n")			
	}

	funcao SolicitaDadosSalario()
	{
		escreva("Informe o nome do vendedor: ")
		leia(nomeVendedor)

		escreva("Informe o salário fixo do vendedor: ")
		leia(salarioFixo)

		escreva("Informe o total de vendas: ")
		leia(totalVendas)
	}

	funcao CalculaSalario()
	{
		salarioFinal = (totalVendas * 0.15) + salarioFixo
	}

	funcao ExibeRelatorio()
	{
		limpa()
		escreva("*********************************\n")
		escreva("******* Folha de Pagamento ******\n")
		escreva("*********************************\n")
		escreva("\nNome do vendedor: " + nomeVendedor)
		escreva("\nSalário fixo:  " + salarioFixo)
		escreva("\nSalário final: " + salarioFinal)
	}		
}
/* $$$ Portugol Studio $$$ 
 * 
 * Esta seção do arquivo guarda informações do Portugol Studio.
 * Você pode apagá-la se estiver utilizando outro editor.
 * 
 * @POSICAO-CURSOR = 13; 
 * @PONTOS-DE-PARADA = ;
 * @SIMBOLOS-INSPECIONADOS = ;
 * @FILTRO-ARVORE-TIPOS-DE-DADO = inteiro, real, logico, cadeia, caracter, vazio;
 * @FILTRO-ARVORE-TIPOS-DE-SIMBOLO = variavel, vetor, matriz, funcao;
 */