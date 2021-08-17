namespace VersionamentoHeaderEspecifico.Api.DTO
{
	public class BicicletaDTO : VeiculoDTO
	{
		public BicicletaDTO(int id, string nome, string versao)
			: base(id, nome, versao)
		{
		}
	}
}