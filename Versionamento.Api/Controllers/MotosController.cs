using System;
using Microsoft.AspNetCore.Mvc;
using Versionamento.Api.DTO;

namespace Versionamento.Api.Controllers
{
	[ApiController]
	[Route("api/v{version:apiVersion}/[controller]")] //Versionamento através da rota
	[ApiVersion("1.0")]
	[ApiVersion("2.0")]
	public class MotosController : ControllerBase
	{
		[HttpGet("{id}")]
		[MapToApiVersion("1.0")]
		[Obsolete]
		public IActionResult GetV1([FromRoute] int id)
		{
			var carroResponse = new MotoDTO(id, "Twister", "V1");

			return Ok(carroResponse);
		}

		[HttpGet("{id}")]
		[MapToApiVersion("2.0")]
		public IActionResult GetV2([FromRoute] int id)
		{
			var carroResponse = new MotoDTO(id, "CB 500", "V2");

			return Ok(carroResponse);
		}
	}
}
