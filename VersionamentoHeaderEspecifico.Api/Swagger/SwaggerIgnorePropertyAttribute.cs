using System;

namespace VersionamentoHeaderEspecifico.Api.Swagger
{
	[AttributeUsage(AttributeTargets.Property)]
	public class SwaggerIgnorePropertyAttribute : Attribute
	{
	}
}
