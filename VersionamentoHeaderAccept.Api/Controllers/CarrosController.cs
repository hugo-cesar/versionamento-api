using Microsoft.AspNetCore.Mvc;
using VersionamentoHeaderAccept.Api.DTO;

namespace VersionamentoHeaderAccept.Api.Controllers
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
			// throw new DomainException($"Carro id: {id} não foi encontrado.");

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

		[HttpPost]
		[MapToApiVersion("1.0")]
		public IActionResult PostV1([FromBody] CarroDTO carroDto)
		{
			return Ok();
		}

	}
}
