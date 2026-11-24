
using System;
using CadastroLivro;

class Program{
	static void salvarDados(List<Livro> listaLivro, string nomeArquivo){
		StreamWriter writer = new StreamWriter(nomeArquivo);
		foreach (Livro livro in listaLivro){
		writer.WriteLine($"{livro.titulo};{livro.autor};{livro.ano};{livro.plateleira}");
		}
		Console.WriteLine("Dados salvos com sucesso!");
		writer.Dispose();
		
	}
	static void carregarDados(List<Livro> listaLivro, string nomeArquivo){
        if (File.Exists(nomeArquivo)) {
            string[] linhasDoArquivo = File.ReadAllLines(nomeArquivo);
            foreach (string linha in linhasDoArquivo) {
                string[] campos = linha.Split(';');
                Livro livro = new Livro();
                livro.titulo = campos[0];
				livro.autor = campos[1];
                livro.ano = int.Parse(campos[2]);
                livro.plateleira = int.Parse(campos[3]);
                listaLivro.Add(livro);
            }
            Console.WriteLine("Dados carregados com sucesso!");
        }else{
            Console.WriteLine("Arquivo não encontrado :(");
        }
    }
	static void addLivro(List<Livro> listaLivro){
		Livro novoLivro = new Livro();
		Console.WriteLine("\n*** Dados para novo Livro ****");
		
		Console.Write("Titulo: ");
		novoLivro.titulo = Console.ReadLine();
		
		Console.Write("Autor: ");
		novoLivro.autor = Console.ReadLine();
		
		Console.Write("Ano da publicação: ");
		novoLivro.ano = int.Parse(Console.ReadLine()); 
		
		Console.Write("Prateleira: ");
		novoLivro.plateleira = int.Parse(Console.ReadLine());
		
		listaLivro.Add(novoLivro);
		
	}
	static void mostrarLivrosCadastrados(List<Livro> listaLivro){
		Console.WriteLine("\n*** LIVROS CADASTRADOS ***");
		
		for(int i = 0; i < listaLivro.Count; i++){
			Console.WriteLine($"\nLivro {i+1}\n");
			Console.WriteLine($"Titulo: {listaLivro[i].titulo}");
			Console.WriteLine($"Autor: {listaLivro[i].autor}");
			Console.WriteLine($"Ano: {listaLivro[i].ano}");
			Console.WriteLine($"Plateleira: {listaLivro[i].plateleira}");
		}
	}
	static bool pesquisarLivroPorTitulo(List<Livro> listaLivro){
		Console.Write("\nTitulo que deseja pesquisar: ");
		string titulo = Console.ReadLine();
		bool encontrado = false;
		for(int i = 0; i < listaLivro.Count; i++){
			if(titulo.ToUpper().Contains(listaLivro[i].titulo.ToUpper())){
				Console.WriteLine($"\nTitulo: {listaLivro[i].titulo}");
				Console.WriteLine($"Plateleira: {listaLivro[i].plateleira}");
				encontrado = true;
			}
		}
		return encontrado;
	}
	static bool livrosMaisNovosQueUmAno(List<Livro> listaLivro, int ano){
		
		bool encontrado = false;
		for(int i = 0; i < listaLivro.Count; i++){
			if(listaLivro[i].ano < ano){
				Console.WriteLine($"\nTitulo: {listaLivro[i].titulo}");
				Console.WriteLine($"Autor: {listaLivro[i].autor}");
				Console.WriteLine($"Ano: {listaLivro[i].ano}");
				Console.WriteLine($"Plateleira: {listaLivro[i].plateleira}");
				encontrado = true;
			}
		}
		return encontrado;
		
		
		
	}
	static int menu(){
		Console.WriteLine("\n====CONTROLE DE LIVROS=========");
		Console.WriteLine("1 - Cadastrar livro");
		Console.WriteLine("2 - Mostrar livros cadastrados");
		Console.WriteLine("3 - Pesquisar Livro por titulo");
		Console.WriteLine("4 - Encontrar livro mais novo que ano");
		Console.WriteLine("5 - Sair e Salvar");
		Console.Write("Escolha uma opção: ");
		return int.Parse(Console.ReadLine());
	}
	static void Main(){
		
		List<Livro> listaLivro = new List<Livro>();
		carregarDados(listaLivro, "dadosLivro.txt");
		int opcao;
		
		do{
			opcao = menu();
			switch(opcao){
				case 1:
					addLivro(listaLivro);
				break;
				
				case 2:
					mostrarLivrosCadastrados(listaLivro);
				break;
				
				case 3:
					bool encontrou = pesquisarLivroPorTitulo(listaLivro);
					if(!encontrou)
						Console.WriteLine("\nLivro Não encontrado");
				break;
				case 4:
					Console.Write("\nDigite o ano: ");
					int ano = int.Parse(Console.ReadLine());
					bool livroMaisNovo = livrosMaisNovosQueUmAno(listaLivro, ano);
					if(!livroMaisNovo)
						Console.WriteLine($"\nNao tem livros mais novos que {ano}");
				break;
				case 0:
					Console.WriteLine("Saindoooo e salvando...");
					salvarDados(listaLivro, "dadosLivro.txt");
				break;
			}
			Console.ReadKey();
			Console.Clear();
		}while(opcao != 0);
	}
}