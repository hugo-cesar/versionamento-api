using Microsoft.AspNetCore.Mvc;

namespace Versionamento.Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	[ApiVersion("1.0", Deprecated = true)]
	[ApiVersion("2.0")]
	[ApiVersion("3.0")]
	public class CarrosController : ControllerBase
	{
		[HttpGet("{id}")]
		public IActionResult GetV1([FromRoute] int id)
		{
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
