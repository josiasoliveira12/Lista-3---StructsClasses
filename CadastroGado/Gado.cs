using System;
namespace CadastroGado{
	internal class Gado{
		public int codigoGado;
		public double litrosLeite;
		public double alimento;
		public char abate;
		public Nascimento nasc = new Nascimento();
	}
	internal class Nascimento{
		public string mes;
		public int ano;
	}
	
}