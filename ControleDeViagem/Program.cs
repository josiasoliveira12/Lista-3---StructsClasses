using System;
using ControleDeViagem;
using System.Collections.Generic;
using System.IO;

class Program {
	
	 static void salvarDados(List<Viagem> listaViagens, string nomeArquivo)
    {
        StreamWriter writer = new StreamWriter(nomeArquivo);
        foreach (Viagem v in listaViagens)
        {
            writer.WriteLine($"{v.modelo};{v.placa};{v.destino};{v.kmRodados};{v.consumoMedio}");
        }
        Console.WriteLine("Dados salvos com sucesso!");
        writer.Dispose();
    }

    static void carregarDados(List<Viagem> listaViagens, string nomeArquivo)
    {
        if (File.Exists(nomeArquivo))
        {
            string[] linhasDoArquivo = File.ReadAllLines(nomeArquivo);
            foreach (string linha in linhasDoArquivo)
            {
                string[] campos = linha.Split(';');
                Viagem v = new Viagem();
                v.modelo = campos[0];
                v.placa = campos[1];
                v.destino = campos[2];
                v.kmRodados = double.Parse(campos[3]);
                v.consumoMedio = double.Parse(campos[4]);
                listaViagens.Add(v);
            }
            Console.WriteLine("Dados carregados com sucesso!");
        }
        else
        {
            Console.WriteLine("Arquivo não encontrado :(");
        }
    }
	
	static void addViagem(List<Viagem> listaViagem)
    {
        Viagem novaViagem = new Viagem();
		Console.Write("Modelo do carro: ");
		novaViagem.modelo = Console.ReadLine();
		
		Console.Write("Placa do carro: ");
		novaViagem.placa = Console.ReadLine();
		
		Console.Write("Destino da Viagem: ");
		novaViagem.destino = Console.ReadLine();
		
		Console.Write("Kilometragem rodada (km):");
		novaViagem.kmRodados = double.Parse(Console.ReadLine());
		
		Console.Write("Consumo medio (km/L): ");
		novaViagem.consumoMedio = double.Parse(Console.ReadLine());
		
		listaViagem.Add(novaViagem);
    }
	
	static void mostrarViagens(List<Viagem> listaViagem) {
		 Console.WriteLine("\n*** VIAGENS CADASTRADAS ***");
       for(int i = 0; i < listaViagem.Count; i++){
		   Console.WriteLine($"Viagem {i+1}");
		   Console.WriteLine($"Modelo do carro: {listaViagem[i].modelo}");
		   Console.WriteLine($"Placa do carro: {listaViagem[i].placa}");
		   Console.WriteLine($"Destino da viagem: {listaViagem[i].destino}");
		   Console.WriteLine($"kilometragem rodada: {listaViagem[i].kmRodados}");
		   Console.WriteLine($"Consumo medio: {listaViagem[i].consumoMedio}\n");
		   
	   }
    }
	
	static bool buscarPorPlaca(List<Viagem> listaViagem, string placaBusca) {
        bool encontrou = false;
		for(int i = 0; i < listaViagem.Count; i++){
			if(listaViagem[i].placa.ToUpper().Equals(placaBusca.ToUpper())){
				Console.WriteLine($"\nModelo do carro: {listaViagem[i].modelo}");
				Console.WriteLine($"Placa do carro: {listaViagem[i].placa}");
				Console.WriteLine($"Destino da viagem: {listaViagem[i].destino}");
				Console.WriteLine($"kilometragem rodada: {listaViagem[i].kmRodados}");
				Console.WriteLine($"Consumo medio: {listaViagem[i].consumoMedio}\n");
				encontrou = true;
			}
		}
		return encontrou;
    }
	
	static bool buscarPorConsumo(List<Viagem> listaViagem, double litrosX) {
		bool encontrado = false;
		for(int i = 0; i < listaViagem.Count; i++){
			if(listaViagem[i].kmRodados / listaViagem[i].consumoMedio > litrosX){
				Console.WriteLine($"\nModelo do carro: {listaViagem[i].modelo}");
				Console.WriteLine($"Placa do carro: {listaViagem[i].placa}");
				Console.WriteLine($"Destino da viagem: {listaViagem[i].destino}");
				Console.WriteLine($"kilometragem rodada: {listaViagem[i].kmRodados}");
				Console.WriteLine($"Consumo medio: {listaViagem[i].consumoMedio}\n");
				Console.WriteLine($"Gasto de combustivel da viagem: {listaViagem[i].kmRodados / listaViagem[i].consumoMedio:f2} Litros\n");
				encontrado = true;
			}
		}
		return encontrado;
    }
	
	static void calcularGastoPorViagem(List<Viagem> listaViagem, double precoLitro) {	
		for(int i = 0; i < listaViagem.Count; i++){
			Console.WriteLine($"\nViagem {i+1}");
			Console.WriteLine($"Modelo do carro: {listaViagem[i].modelo}");
			Console.WriteLine($"Placa do carro: {listaViagem[i].placa}");
			Console.WriteLine($"Destino da viagem: {listaViagem[i].destino}");
			Console.WriteLine($"kilometragem rodada: {listaViagem[i].kmRodados}");
			Console.WriteLine($"Consumo medio: {listaViagem[i].consumoMedio}\n");
			Console.WriteLine($"Gasto de combustivel da viagem: {listaViagem[i].kmRodados / listaViagem[i].consumoMedio:f2} Litros");
			Console.WriteLine($"Gasto total da viagem em R${(listaViagem[i].kmRodados / listaViagem[i].consumoMedio) * precoLitro:f2}\n");
			
		}
    }
	
	static void calcularTotalGeral(List<Viagem> listaViagem, double precoLitro) {
		double totalLitros = 0;
		for(int i = 0; i < listaViagem.Count; i++){
			totalLitros += listaViagem[i].kmRodados / listaViagem[i].consumoMedio;
		}
		double totalReais = totalLitros * precoLitro;
		Console.WriteLine($"\nConsumo total em Litros {totalLitros:f2}");
		Console.WriteLine($"Total gasto em R$ {totalReais:f2}\n");
    }

	
	static int menu(){
		Console.WriteLine("\n****** CONTROLE DE VIAGENS ********");
		Console.WriteLine("1 - Cadastrar nova viagem");
		Console.WriteLine("2 - Mostrar todas as viagens");
		Console.WriteLine("3 - Buscar viagem pela placa do veiculo");
		Console.WriteLine("4 - Buscar viagem que consumio mais que X litros");
		Console.WriteLine("5 - Exibir consumo (L) e gasto (R$) de cada viagem");
		Console.WriteLine("6 - Exibir consumo total e gasto total de todas as viagens");
		Console.WriteLine("0 - Sair\n");
		Console.Write("Escolha uma opção: ");
		return int.Parse(Console.ReadLine());
	}
	static void Main(){
		List<Viagem> listaViagem = new List<Viagem>();
		carregarDados(listaViagem, "Viagens.txt");
		
		int opcao;
		do{
			opcao = menu();
			switch (opcao){
				
				case 1:
					addViagem(listaViagem);
				break;
				case 2:
					mostrarViagens(listaViagem);
				break;
				case 3:
					Console.Write("Placa que deseja buscar: ");
					string placaBusca = Console.ReadLine();
					bool encontrado = buscarPorPlaca(listaViagem, placaBusca);
					if(!encontrado)
						Console.WriteLine("Placa nao encontrada!");

				break;
				case 4:
					Console.Write("Litros: ");
					double x = double.Parse(Console.ReadLine());
					bool encontrou = buscarPorConsumo(listaViagem, x);
					if(!encontrou)
						Console.WriteLine("Não houve correspondencia!");
			
				break;
				case 5:
					Console.Write("Preço do litro: R$");
					double preco = double.Parse(Console.ReadLine());
					calcularGastoPorViagem(listaViagem, preco);

				break;
				case 6:
					Console.Write("Preço do litro: R$");
					double Preco = double.Parse(Console.ReadLine());
					Console.WriteLine("\nConsumo de todas as viagens: ");
					calcularTotalGeral(listaViagem, Preco);

				break;
					
				case 0:
					Console.WriteLine("Saindooooo...");
					salvarDados(listaViagem, "viagens.txt");
				break;
				
				
				
			}
			Console.ReadKey();
            Console.Clear();			
		}while (opcao != 0);
		
	}
}
