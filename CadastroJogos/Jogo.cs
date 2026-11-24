using System;

namespace CadastroJogos{
	internal class Jogo{
		public string nome;
		public string console;
		public int ano;
		public int ranking;
		public Emprestimo DadosEmprestimo = new Emprestimo();
	}
	internal class Emprestimo{
		public string data;
		public string nomePessoa;
		public char emprestado = 'N';
	}
}