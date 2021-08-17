using Microsoft.AspNetCore.Mvc;
using VersionamentoURL.Api.DTO;

namespace VersionamentoURL.Api.Controllers.V1
{
	[ApiController]
	[Route("api/v{version:apiVersion}/[controller]")]
	[ApiVersion("1.0")]
	public class BicicletasController : ControllerBase
	{
		[HttpGet("{id}")]
		public IActionResult Get([FromRoute] int id)
		{
			var carroResponse = new MotoDTO(id, "GTS", "V1");

			return Ok(carroResponse);
		}
	}
}
