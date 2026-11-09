﻿using System;
using CadastroDeEletrodomesticos;

class Program{
    static void addEletro(List<Eletro> listaEletro) {
        Eletro novoEletro = new Eletro();
        Console.WriteLine("*** Dados para novo Eletro ***");
        
        Console.Write("Nome: ");
        novoEletro.nome = Console.ReadLine();

        Console.Write("Potencia (Kw):");
        novoEletro.potencia = double.Parse(Console.ReadLine());

        Console.Write("Tempo de uso (Media diaria): ");
        novoEletro.tempo = int.Parse(Console.ReadLine());

        listaEletro.Add(novoEletro);

    }

    static void mostrarEletro(List<Eletro> listaEletro) {

        Console.WriteLine("\n*** Eletros Cadastradas *** ");

        for (int i = 0; i < listaEletro.Count; i++) {
            Console.WriteLine($"*** Eletro {i + 1} ***");
            Console.WriteLine($"Nome: {listaEletro[i].nome}");
            Console.WriteLine($"Potencia: {listaEletro[i].potencia}");
            Console.WriteLine($"Tempo de uso: {listaEletro[i].tempo}\n");
            
        }// Fim for

    }

     static void salvarDados(List<Eletro> listaEletros, string nomeArquivo)
    {
        StreamWriter writer = new StreamWriter(nomeArquivo);
        foreach (Eletro eletro in listaEletros)
        {
            writer.WriteLine($"{eletro.nome};{eletro.potencia};{eletro.tempo}");
        }
        Console.WriteLine("Dados salvos com sucesso!");
        writer.Dispose();
    }

    static void carregarDados(List<Eletro> listaEletros, string nomeArquivo) {
        if (File.Exists(nomeArquivo))
        {
            string[] linhasDoArquivo = File.ReadAllLines(nomeArquivo);
            foreach (string linha in linhasDoArquivo)
            {
                string[] campos = linha.Split(';');
                Eletro eletro = new Eletro();
                eletro.nome = campos[0];
                eletro.potencia = double.Parse(campos[1]);
                eletro.tempo = double.Parse(campos[2]);
                listaEletros.Add(eletro);
            }
            Console.WriteLine("Dados carregados com sucesso!");
        }
        else
        {
            Console.WriteLine("Arquivo não encontrado :(");
        }
    }
    static bool buscarEletro(List<Eletro> listaEletro, string nomebusca)
    {
        bool encontrado = false;
        for (int i = 0; i < listaEletro.Count; i++) // Percorra até a quantidade de elementos da lista
        {
            if (listaEletro[i].nome.ToUpper().Contains(nomebusca.ToUpper()))
            {
                Console.WriteLine("\n*** Eletro ***");
                Console.WriteLine($"Nome: {listaEletro[i].nome}");
                Console.WriteLine($"Potencia: {listaEletro[i].potencia}");
                Console.WriteLine($"Tempo de Uso: {listaEletro[i].tempo}");
                encontrado = true; // Marque que pelo menos uma banda foi localizada
            }
        }
        return encontrado;
    }

    static bool buscarPorConsumo(List<Eletro> listaEletro, double valorX)
    {
        bool encontrado = false;
        for (int i = 0; i < listaEletro.Count; i++) {
            double consumoDia = listaEletro[i].potencia * listaEletro[i].tempo;

            if (valorX > consumoDia) {
                Console.WriteLine($"Nome: {listaEletro[i].nome}");
                Console.WriteLine($"Consumo diário: {consumoDia}");

                encontrado = true;
            }

        }
        return encontrado;
      
    }

    static void calcularConsumo(List<Eletro> listaEletro) {
        Console.WriteLine("Valor do Kw em R$: ");
        double valorkw = double.Parse(Console.ReadLine());
        double totalDia = 0, totalMes = 0;

        foreach (Eletro e in listaEletro) {
            totalDia = e.potencia * e.tempo;
        }
        Console.WriteLine($"Total de Kw consumidos por dia: {totalDia}");
        Console.WriteLine($"Total em R$ gastos por dia: {totalDia * valorkw}");
        totalMes = totalDia * 30;
        Console.WriteLine($"Total de Kw consumidos no mes: {totalMes}");
        Console.WriteLine($"Total em R$ gastos no mes: {totalMes * valorkw}");

    }



    static int menu()
    {
        Console.WriteLine("\n=== CONTROLE DE CONSUMO DE ENERGIA RESIDENCIAL ===");
        Console.WriteLine("1 - Cadastrar Eletrodoméstico");
        Console.WriteLine("2 - Mostrar Eletrodomésticos");
        Console.WriteLine("3 - Buscar Eletrodoméstico pelo Nome");
        Console.WriteLine("4 - Buscar Eletrodomésticos que Gastam mais que X (kWh/dia)");
        Console.WriteLine("5 - Calcular Consumo Diário e Mensal da Casa (kW e R$)");
        Console.WriteLine("0 - Sair");
        Console.Write("Escolha uma opção: ");
        return int.Parse(Console.ReadLine());
    }


    static void Main() {

        List<Eletro> listaEletro = new List<Eletro>();
        carregarDados(listaEletro, "dadosEletro.txt");

        int opcao;
        do
        {
            opcao = menu();
            switch (opcao)
            {
                case 1:
                    addEletro(listaEletro);
                    break;
                case 2:
                    mostrarEletro(listaEletro);
                    break;
                case 3:
                    Console.Write("Digite o nome do Eletro que deseja buscar:");
                    string nomeDeBusca = Console.ReadLine();
                    bool encontrado = buscarEletro(listaEletro, nomeDeBusca);
                    if (!encontrado)
                        Console.WriteLine("Eletro não encontrada!");
                    break;
                case 4:
                    Console.Write("Consumo minimo para buscar: ");
                    double consumo = double.Parse(Console.ReadLine());
                    buscarPorConsumo(listaEletro, consumo);
                    break;
                case 5:
                   
                    calcularConsumo(listaEletro);

                    break;
                case 0:
                    Console.WriteLine("Saindo e salvando...");
                    salvarDados(listaEletro, "dadosEletro.txt");

                    break;
            }// fim switch
            Console.ReadKey();
            Console.Clear();
        } while (opcao != 0);



    }



}

































