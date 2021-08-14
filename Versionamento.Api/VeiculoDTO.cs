﻿using System;
using Versionamento.Api.Swagger;

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

		[SwaggerIgnoreProperty]
		public int Id { get; set; }
		public string Nome { get; set; }
		public string Versao { get; set; }
	}
}
