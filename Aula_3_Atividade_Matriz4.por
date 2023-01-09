programa
{
	cadeia produto[2] = {"Ração", "Petisco"}
	inteiro quantidadeVenda[2]
	real precoCompra[2], precoVenda[2]
	real lucroProduto[2], lucroBruto, lucroLiquido, totalCompra, totalVenda, valorImposto, lucroTotal = 0.0
		
	funcao inicio()
	{
		ExibeCabecalho()
		Menu()				
	}

	funcao ExibeCabecalho()
	{
		escreva("****************************************\n")
		escreva("***** Cálculo de lucro sobre vendas*****\n")
		escreva("****************************************\n\n")
	}
	
	funcao Menu()
	{
		escreva("\nMenu: \n")
		escreva("\n1 - Informar dados da venda\n")
		escreva("2 - Finalizar\n")
		inteiro opcao = 0
		leia(opcao)

		se(opcao == 1)
		{
			SolicitaDadosVenda()
			CalculaLucro()
			ExibeLucro()		
		}
		senao se(opcao == 2)
		{
			retorne
		}
		senao
		{
			Menu()
		}		
	}

	funcao SolicitaDadosVenda()
	{
		para(inteiro posicao = 0; posicao < 2; posicao++)
		{	
								
			escreva("\nInforme o preço de compra de " + produto[posicao] + ": ")
			real precoCompraUnidade
			leia(precoCompraUnidade)
			precoCompra[posicao] = precoCompraUnidade
	
			escreva("Informe o preço de venda de " + produto[posicao] + ": ")
			real precoVendaUnidade
			leia(precoVendaUnidade)
			precoVenda[posicao]= precoVendaUnidade
			
			escreva("Informe a quantidade vendida de " + produto[posicao] + ": ")
			inteiro unidadesVendidas
			leia(unidadesVendidas)
		 	quantidadeVenda[posicao] = unidadesVendidas	
		
		}
	}
	
	funcao CalculaLucro()
	{
		para(inteiro posicao = 0; posicao < 2; posicao++)
		{	
			
			totalCompra = quantidadeVenda[posicao] * precoCompra[posicao]
			totalVenda = quantidadeVenda[posicao] * precoVenda[posicao]
			lucroBruto = totalVenda - totalCompra
			valorImposto = lucroBruto * 0.15
			lucroLiquido = lucroBruto - valorImposto
			lucroProduto[posicao] = lucroLiquido
			lucroTotal = lucroTotal + lucroProduto[posicao]
		}

	}

	funcao ExibeLucro()
	{
		escreva("\nLucro por produto vendido: \n")
		
		para(inteiro posicao = 0; posicao < 2; posicao++)
		{
			escreva("\n" + produto[posicao] + ": " + lucroProduto[posicao])
			
		}
		
		escreva("\n\nLucro total: " + lucroTotal + "\n")
		Menu()		
	}


	
	
}
/* $$$ Portugol Studio $$$ 
 * 
 * Esta seção do arquivo guarda informações do Portugol Studio.
 * Você pode apagá-la se estiver utilizando outro editor.
 * 
 * @POSICAO-CURSOR = 1510; 
 * @PONTOS-DE-PARADA = 73;
 * @SIMBOLOS-INSPECIONADOS = ;
 * @FILTRO-ARVORE-TIPOS-DE-DADO = inteiro, real, logico, cadeia, caracter, vazio;
 * @FILTRO-ARVORE-TIPOS-DE-SIMBOLO = variavel, vetor, matriz, funcao;
 */