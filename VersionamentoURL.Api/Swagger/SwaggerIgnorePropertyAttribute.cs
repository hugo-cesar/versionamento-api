using System;

namespace VersionamentoURL.Api.Swagger
{
	[AttributeUsage(AttributeTargets.Property)]
	public class SwaggerIgnorePropertyAttribute : Attribute
	{
	}
}
