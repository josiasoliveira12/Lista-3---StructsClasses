using System;
using System.Collections.Generic;
using System.IO;
using CadastroJogos;

class Program{
	static void carregarDados(List<Jogo> listaJogo, string nomeArquivo) {
        if (File.Exists(nomeArquivo))
        {
            string[] linhasDoArquivo = File.ReadAllLines(nomeArquivo);
            foreach (string linha in linhasDoArquivo)
            {
                string[] campos = linha.Split(';');
                Jogo jogo = new Jogo();
                jogo.nome = (campos[0]);
				jogo.console = (campos[1]);
                jogo.ano = int.Parse(campos[2]);
				jogo.ranking = int.Parse(campos[3]);
				if (campos.Length >= 7)
				{
					jogo.DadosEmprestimo.emprestado = char.Parse(campos[4]);
					jogo.DadosEmprestimo.nomePessoa = (campos[5]);
					jogo.DadosEmprestimo.data = (campos[6]);
				}
                listaJogo.Add(jogo);
            }
            Console.WriteLine("Dados carregados com sucesso!");
        }
        else
        {
            Console.WriteLine("Arquivo não encontrado :(");
        }
    }
	static void salvarDados(List<Jogo> listaJogo, string nomeArquivo){
        StreamWriter writer = new StreamWriter(nomeArquivo);
        foreach (Jogo jogo in listaJogo)
        {
            writer.WriteLine($"{jogo.nome};{jogo.console};{jogo.ano};{jogo.ranking};{jogo.DadosEmprestimo.emprestado};{jogo.DadosEmprestimo.nomePessoa};{jogo.DadosEmprestimo.data}");
        }
        Console.WriteLine("Dados salvos com sucesso!");
        writer.Dispose();
    }
	
	static void addJogo(List<Jogo> listaJogo){
		Jogo novoJogo = new Jogo();
		Console.WriteLine("\n*** Dados para adicionar novo jogo ***\n");
		
		Console.Write("Nome do jogo: ");
		novoJogo.nome = Console.ReadLine();
		
		Console.Write("Console: ");
		novoJogo.console = Console.ReadLine();
		
		Console.Write("Ano:");
		novoJogo.ano = int.Parse(Console.ReadLine());
		
		Console.Write("Ranking: ");
		novoJogo.ranking = int.Parse(Console.ReadLine());
		
		novoJogo.DadosEmprestimo.emprestado = 'N';
		
		listaJogo.Add(novoJogo);
	}
	static void mostrarJogos(List<Jogo> listaJogo){
		Console.WriteLine($"\n*** Jogos Cadastrados ***");
		for(int i = 0; i < listaJogo.Count; i++){
			Console.WriteLine($"\nJogo {i + 1}");
			Console.WriteLine($"Nome do jogo: {listaJogo[i].nome}");
			Console.WriteLine($"Console: {listaJogo[i].console}");
			Console.WriteLine($"Ano: {listaJogo[i].ano}");
			Console.WriteLine($"Ranking: {listaJogo[i].ranking}");
			Console.WriteLine($"Emprestado: {listaJogo[i].DadosEmprestimo.emprestado}");
		}
	}
	
	static void emprestarJogo(List<Jogo> listaJogo){
		Console.WriteLine("\n*** Empréstimo de jogo ***\n");
		Console.Write("Nome do jogo: ");
		string nomeBusca = Console.ReadLine();
		bool ensontrou = false;
		for(int i = 0; i < listaJogo.Count; i++){
			if(listaJogo[i].nome.ToUpper().Equals(nomeBusca.ToUpper())){
				ensontrou = true;
				if(listaJogo[i].DadosEmprestimo.emprestado == 'S'){
					Console.WriteLine($"Jogo emprestado para {listaJogo[i].DadosEmprestimo.nomePessoa}");
				}else{
					Console.Write("Nome da pessoa que ta pegando o jogo: ");
					listaJogo[i].DadosEmprestimo.nomePessoa = Console.ReadLine();
					
					listaJogo[i].DadosEmprestimo.data = DateTime.Now.ToString("dd/MM/yyyy");
					listaJogo[i].DadosEmprestimo.emprestado = 'S';
					Console.WriteLine("Jogo emprestado com sucesso!");
				}
				break;
			}
		}
		if(!ensontrou)
			Console.WriteLine("Jogo nao cadastrado!");
		
		
	}
	static void mostrarJogosEmprestados(List<Jogo> listaJogo){
		Console.WriteLine("\n=== JOGOS EMPRESTADOS ===\n");
		for(int i = 0; i < listaJogo.Count; i++ ){
			if(listaJogo[i].DadosEmprestimo.emprestado == 'S'){
				Console.WriteLine($"\nJogo {i + 1}");
				Console.WriteLine($"Nome do jogo: {listaJogo[i].nome}");
				Console.WriteLine($"Console: {listaJogo[i].console}");
				Console.WriteLine($"Ano: {listaJogo[i].ano}");
				Console.WriteLine($"Ranking: {listaJogo[i].ranking}");
				Console.WriteLine($"Emprestado: {listaJogo[i].DadosEmprestimo.emprestado}");
				Console.WriteLine($"Nome da pessoa que pegou emprestado: {listaJogo[i].DadosEmprestimo.nomePessoa}");
				Console.WriteLine($"Data do emprestimo: {listaJogo[i].DadosEmprestimo.data}");
				
			}
		}	
	}
	static void procurarPorTitulo(List<Jogo> listaJogo) {
        Console.WriteLine("\n*** Procurar Jogo por Título ***\n");
        Console.Write("Digite o título do jogo: ");
        string nomeBusca = Console.ReadLine();
        bool encontrou = false;

        for (int i = 0; i < listaJogo.Count; i++) {
            if (listaJogo[i].nome.ToUpper().Equals(nomeBusca.ToUpper())){
                encontrou = true;
                Console.WriteLine($"\nJogo {i + 1}");
                Console.WriteLine($"Nome do jogo: {listaJogo[i].nome}");
                Console.WriteLine($"Console: {listaJogo[i].console}");
                Console.WriteLine($"Ano: {listaJogo[i].ano}");
                Console.WriteLine($"Ranking: {listaJogo[i].ranking}");
                Console.WriteLine($"Emprestado: {listaJogo[i].DadosEmprestimo.emprestado}");
            }
        }

        if (!encontrou){
            Console.WriteLine("Jogo não encontrado!");
        }
    }
	static void listarPorConsole(List<Jogo> listaJogo) {
        Console.WriteLine("\n*** Listar Jogos por Console ***\n");
        Console.Write("Digite o console: ");
        string consoleBusca = Console.ReadLine();
        bool encontrou = false;

        for (int i = 0; i < listaJogo.Count; i++) {
            if (listaJogo[i].console.ToUpper().Equals(consoleBusca.ToUpper())){
                encontrou = true;
                Console.WriteLine($"\nJogo {i + 1}");
                Console.WriteLine($"Nome do jogo: {listaJogo[i].nome}");
                Console.WriteLine($"Console: {listaJogo[i].console}");
                Console.WriteLine($"Ano: {listaJogo[i].ano}");
                Console.WriteLine($"Ranking: {listaJogo[i].ranking}");
                Console.WriteLine($"Emprestado: {listaJogo[i].DadosEmprestimo.emprestado}");
            }
        }

        if (!encontrou){
            Console.WriteLine("Nenhum jogo encontrado para este console!");
        }
    }
	
	static void devolverJogo(List<Jogo> listaJogo) {
        Console.WriteLine("\n*** Devolução de jogo ***\n");
        Console.Write("Nome do jogo: ");
        string nomeBusca = Console.ReadLine();
        bool encontrou = false;

        for (int i = 0; i < listaJogo.Count; i++) {
            if (listaJogo[i].nome.ToUpper().Equals(nomeBusca.ToUpper())) {
                encontrou = true;
                if (listaJogo[i].DadosEmprestimo.emprestado == 'S') {
                    Console.WriteLine($"\nJogo {i + 1}");
                    Console.WriteLine($"Nome do jogo: {listaJogo[i].nome}");
                    Console.WriteLine($"Console: {listaJogo[i].console}");
                    Console.WriteLine($"Ano: {listaJogo[i].ano}");
                    Console.WriteLine($"Ranking: {listaJogo[i].ranking}");
                    Console.WriteLine($"Emprestado: {listaJogo[i].DadosEmprestimo.emprestado}");
                    Console.WriteLine($"Nome da pessoa que pegou emprestado: {listaJogo[i].DadosEmprestimo.nomePessoa}");
                    Console.WriteLine($"Data do empréstimo: {listaJogo[i].DadosEmprestimo.data}");

                    listaJogo[i].DadosEmprestimo.nomePessoa = "";
                    listaJogo[i].DadosEmprestimo.data = "";
                    listaJogo[i].DadosEmprestimo.emprestado = 'N';

                    Console.WriteLine("\nJogo devolvido com sucesso!");
                }else
                    Console.WriteLine("Este jogo não consta como emprestado!");
                
                break;
            }
        }

        if (!encontrou)
            Console.WriteLine("Jogo não cadastrado!");
    }
	static int menu(){
		Console.WriteLine("\n=== CONTROLE DE CADASTRO DE JOGOS ===\n");
		Console.WriteLine("1 - Cadastrar Jogo");
		Console.WriteLine("2 - Mostrar jogos cadastrados");
		Console.WriteLine("3 - Pesquisar jogo por titulo");
		Console.WriteLine("4 - Listar jogos por console");
		Console.WriteLine("5 - Emprestimo de jogo");
		Console.WriteLine("6 - Devolucao de jogo");
		Console.WriteLine("7 - Mostrar jogos emprestados");
		Console.WriteLine("0 - Sair e Salvar");
		Console.Write("\nEscolha uma opção: ");
		return int.Parse(Console.ReadLine());
	}
	static void Main(){
		List<Jogo> listaJogo = new List<Jogo>();
		carregarDados(listaJogo, "dadosJogos.txt");
		
		int opcao;
		
		do{
			opcao = menu();
			switch(opcao){
				case 1:
					addJogo(listaJogo);
				break;
				case 2:
					mostrarJogos(listaJogo);
				break;
				case 3:
					procurarPorTitulo(listaJogo);
				break;
				case 4:
					listarPorConsole(listaJogo);
				break;
				case 5:
					emprestarJogo(listaJogo);
				break;
				case 6:
					devolverJogo(listaJogo);
				break;
				case 7:
					mostrarJogosEmprestados(listaJogo);
				break;
				case 0:
					Console.WriteLine("Saindoo e Salvando...");
					salvarDados(listaJogo, "dadosJogos.txt");
				break;
			}
			
		Console.ReadKey();
		Console.Clear();
		}while(opcao != 0);
	}
}
