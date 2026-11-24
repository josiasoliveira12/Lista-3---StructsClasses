using System; 
using System.Collections.Generic;
using System.IO;
using CadastroGado; 

class Program{ 
    static void carregarDados(List<Gado> listaGado, string nomeArquivo) { 
        if (File.Exists(nomeArquivo)) { 
            listaGado.Clear();
            string[] linhasDoArquivo = File.ReadAllLines(nomeArquivo); 
            foreach (string linha in linhasDoArquivo) { 
                if (string.IsNullOrWhiteSpace(linha)) continue;
                string[] campos = linha.Split(';'); 
                Gado gado = new Gado(); 
                gado.codigoGado = int.Parse(campos[0]); 
                gado.litrosLeite = double.Parse(campos[1]); 
                gado.alimento = double.Parse(campos[2]); 
                gado.abate = char.Parse(campos[3]); 
                if (campos.Length >= 6) { 
                    gado.nasc.mes = campos[4]; 
                    gado.nasc.ano = int.Parse(campos[5]); 
                } 
                verificarAbate(gado);
                listaGado.Add(gado); 
            } 
            Console.WriteLine("Dados carregados com sucesso!"); 
        }else{ 
            Console.WriteLine("Arquivo não encontrado :("); 
        } 
    } 

    static void salvarDados(List<Gado> listaGado, string nomeArquivo){ 
        StreamWriter writer = new StreamWriter(nomeArquivo); 
        foreach (Gado gado in listaGado) { 
            writer.WriteLine($"{gado.codigoGado};{gado.litrosLeite};{gado.alimento};{gado.abate};{gado.nasc.mes};{gado.nasc.ano}"); 
        } 
        Console.WriteLine("Dados salvos com sucesso!"); 
        writer.Dispose(); 
    } 

    static void verificarAbate(Gado gado){
        int anoAtual = DateTime.Now.Year;
        int idade = anoAtual - gado.nasc.ano;

        if(int.TryParse(gado.nasc.mes, out int mesNasc)){
            if(DateTime.Now.Month < mesNasc){
                idade--;
            }
        }

        if(idade > 5 || gado.litrosLeite < 40){
            gado.abate = 'S';
        }else{
            gado.abate = 'N';
        }
    }

    static void addGado(List<Gado> listaGado){ 
        Gado novoGado = new Gado(); 
        Console.WriteLine("=== Cadastrar novo gado ===\n"); 
        Console.Write("Codigo da cabeça do gado: "); 
        novoGado.codigoGado = int.Parse(Console.ReadLine()); 
        Console.Write("Litros de Leite produzidos: "); 
        novoGado.litrosLeite = double.Parse(Console.ReadLine()); 
        Console.Write("Quantidade de alimentos consumidos: "); 
        novoGado.alimento = double.Parse(Console.ReadLine()); 
        Console.Write("Mes do nascimento (numero): "); 
        novoGado.nasc.mes = Console.ReadLine(); 
        Console.Write("Ano do nascimento: "); 
        novoGado.nasc.ano = int.Parse(Console.ReadLine()); 

        verificarAbate(novoGado);
        listaGado.Add(novoGado); 
        Console.WriteLine($"Gado cadastrado! Abate definido como: {novoGado.abate}");
    } 

    static void totalLeite(List<Gado> listaGado){
        double total = 0;
        foreach(Gado gado in listaGado){
            total += gado.litrosLeite;
        }
        Console.WriteLine($"\nQuantidade total de leite produzida por semana: {total} litros");
    }

    static void totalAlimento(List<Gado> listaGado){
        double total = 0;
        foreach(Gado gado in listaGado){
            total += gado.alimento;
        }
        Console.WriteLine($"\nQuantidade total de alimento consumido por semana: {total} kg");
    }

    static void listarAbate(List<Gado> listaGado){
        Console.WriteLine("\n=== Animais para o Abate ===\n");
        bool temAbate = false;
        foreach(Gado gado in listaGado){
            if(gado.abate == 'S'){
                Console.WriteLine($"Código: {gado.codigoGado} | Leite: {gado.litrosLeite}L | Alimento: {gado.alimento}kg | Nasc: {gado.nasc.mes}/{gado.nasc.ano}");
                temAbate = true;
            }
        }
        if(!temAbate){
            Console.WriteLine("Nenhum animal para o abate.");
        }
    }

    static int menu(){ 
        Console.WriteLine("\n=== CONTROLE DE GADO ===\n"); 
        Console.WriteLine("1 - Cadastrar novo Gado"); 
        Console.WriteLine("2 - Total de leite produzido por semana");
        Console.WriteLine("3 - Total de alimento consumido por semana");
        Console.WriteLine("4 - Listar animais para o abate");
        Console.WriteLine("5 - Salvar dados em arquivo");
        Console.WriteLine("6 - Carregar dados do arquivo");
        Console.WriteLine("0 - Sair e salvar"); 
        Console.Write("\nEscolha uma opção: "); 
        return int.Parse(Console.ReadLine()); 
    } 

    static void Main(){ 
        List<Gado> listaGado = new List<Gado>(); 
        carregarDados(listaGado, "dadosGado.txt"); 
        int opcao; 
        do{ 
            opcao = menu(); 
            switch(opcao){ 
                case 1: 
                    addGado(listaGado); 
                    break; 
                case 2:
                    totalLeite(listaGado);
                    break;
                case 3:
                    totalAlimento(listaGado);
                    break;
                case 4:
                    listarAbate(listaGado);
                    break;
                case 5:
                    salvarDados(listaGado, "dadosGado.txt");
                    break;
                case 6:
                    carregarDados(listaGado, "dadosGado.txt");
                    break;
                case 0:
                    salvarDados(listaGado, "dadosGado.txt");
                    break;
            } 
            Console.ReadKey(); 
            Console.Clear(); 
        }while(opcao != 0); 
    } 
}
