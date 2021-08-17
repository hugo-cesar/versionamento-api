using System;
using Microsoft.AspNetCore.Mvc;
using VersionamentoHeaderEspecifico.Api.DTO;

namespace VersionamentoHeaderEspecifico.Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
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
