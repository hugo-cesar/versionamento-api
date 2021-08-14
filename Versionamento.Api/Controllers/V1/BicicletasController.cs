using Microsoft.AspNetCore.Mvc;

namespace Versionamento.Api.Controllers.V1
{
	[ApiController]
	[Route("api/v{version:apiVersion}/[controller]")] //Versionamento através da rota
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
