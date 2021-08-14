using System;

namespace Versionamento.Api
{
	public abstract class VeiculoDTO
	{
		protected VeiculoDTO(int id, string nome, string versao)
		{
			Id = id;
			Nome = nome;
			Versao = versao;
		}

		public int Id { get; set; }
		public string Nome { get; set; }
		public string Versao { get; set; }
	}
}
