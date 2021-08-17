using System;

namespace VersionamentoHeaderAccept.Api.Swagger
{
	[AttributeUsage(AttributeTargets.Property)]
	public class SwaggerIgnorePropertyAttribute : Attribute
	{
	}
}
