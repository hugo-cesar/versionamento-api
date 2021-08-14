namespace Versionamento.Api
{
	public class CarroDTO : VeiculoDTO
	{
		public CarroDTO(int id, string nome, string versao)
			: base(id, nome, versao)
		{
		}
	}
}
