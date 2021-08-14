using System;

namespace Versionamento.Api.Swagger
{
	[AttributeUsage(AttributeTargets.Property)]
	public class SwaggerIgnorePropertyAttribute : Attribute
	{
	}
}
