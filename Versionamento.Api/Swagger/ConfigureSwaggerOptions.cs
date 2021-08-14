using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Versionamento.Api.Swagger
{
    /// <summary>
    /// Configura as opções de geração do Swagger.
    /// </summary>
    /// <remarks>Isso permite que o controle de versão da API defina um documento Swagger por versão da API após
    /// <see cref="IApiVersionDescriptionProvider"/> o serviço ser resolvido a partir do container de serviço.</remarks>
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;

        /// <summary>
        /// Inicializa uma nova instância do <see cref="ConfigureSwaggerOptions"/> class.
        /// </summary>
        /// <param name="provider">The <see cref="IApiVersionDescriptionProvider">provider</see> usado para gerar documentos Swagger.</param>
        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) => _provider = provider;

        /// <inheritdoc />
        public void Configure(SwaggerGenOptions options)
        {
            // adicione um documento swagger para cada versão de API descoberta
            foreach (var description in _provider.ApiVersionDescriptions)
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
        }

        private static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
        {
            var info = new OpenApiInfo()
            {
                Title = "Exemplo de Versionamento de Api",
                Version = description.ApiVersion.ToString()
            };

            //if (description.IsDeprecated)
            //    info.Description += " Essa versão da Api está obsoleta.";

            return info;
        }
    }
}
