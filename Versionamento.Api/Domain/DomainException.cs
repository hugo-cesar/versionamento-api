using System;

namespace Versionamento.Api.Domain
{
	public class DomainException : Exception
	{
		public DomainException(string message)
			: base(message)
		{
		}
	}
}
