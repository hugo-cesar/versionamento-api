using Microsoft.AspNetCore.Mvc;
using Versionamento.Api.Domain;

namespace Versionamento.Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	[ApiVersion("1.0", Deprecated = true)]
	[ApiVersion("2.0")]
	[ApiVersion("3.0")]
	public class CarrosController : ControllerBase
	{
		/// <summary>
		/// Esta API recupera um carro pelo id
		/// </summary>
		/// <response code="200">Retorna um carro</response>
		/// <response code="400">Apenas para demonstração</response>
		[HttpGet("{id}")]
		public IActionResult GetV1([FromRoute] int id)
		{
			throw new DomainException($"Carro id: {id} não foi encontrado.");

			var carroResponse = new CarroDTO(id, "Cruze", "V1");

			return Ok(carroResponse);
		}

		[HttpGet("{id}")]
		[MapToApiVersion("2.0")]
		public IActionResult GetV2([FromRoute] int id)
		{
			var carroResponse = new CarroDTO(id, "Cruze", "V2");

			return Ok(carroResponse);
		}

		[HttpGet("{id}")]
		[MapToApiVersion("3.0")]
		public IActionResult GetV3([FromRoute] int id)
		{
			var carroResponse = new CarroDTO(id, "Cruze", "V3");

			return Ok(carroResponse);
		}

	}
}
