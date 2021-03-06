using Microsoft.AspNetCore.Mvc;
using VersionamentoURL.Api.DTO;

namespace VersionamentoURL.Api.Controllers.V2
{
	[ApiController]
	[Route("api/v{version:apiVersion}/[controller]")]
	[ApiVersion("2.0")]
	public class BicicletasController : ControllerBase
	{
		[HttpGet("{id}")]
		public IActionResult GetV1([FromRoute] int id)
		{
			var carroResponse = new MotoDTO(id, "MTB", "V2");

			return Ok(carroResponse);
		}
	}
}
