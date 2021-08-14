using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using Versionamento.Api.Infrastructure;
using Versionamento.Api.Swagger;

namespace Versionamento.Api
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();

			services.AddApiVersioning(options =>
			{
				options.AssumeDefaultVersionWhenUnspecified = true;
				options.DefaultApiVersion = ApiVersion.Default;
				options.ApiVersionReader = ApiVersionReader.Combine(
					new MediaTypeApiVersionReader("version"), //passa o version no header na Key accept. Ex: application/json;version=2.0
					new HeaderApiVersionReader("api-version")); //aceita uma nova Key api-version no header
				options.ReportApiVersions = true;
			});

			services.AddVersionedApiExplorer(options =>
			{
				// adiciona o API explorer versionado, que tamb�m adiciona o servi�o IApiVersionDescriptionProvider
				// observa��o: o c�digo de formato especificado formatar� a vers�o como "'v'major [.minor] [- status]"
				options.GroupNameFormat = "'v'VVV";

				// nota: esta op��o s� � necess�ria ao controlar a vers�o por segmento de url.
				// o SubstitutionFormat tamb�m pode ser usado para controlar o formato da vers�o da API em modelos de rota
				//options.SubstituteApiVersionInUrl = true;
			});

			services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
			services.AddSwaggerGen(options =>
			{
				// adiciona um filtro de opera��o personalizado que define os valores padr�o
				options.OperationFilter<SwaggerDefaultValues>();
				options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
			});

			//URL da API em letras min�sculas: Carros -> carros
			services.Configure<RouteOptions>(options => { options.LowercaseUrls = true; });
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger(options => { options.RouteTemplate = "api-docs/{documentName}/docs.json"; });
				app.UseSwaggerUI(options =>
				{
					options.RoutePrefix = "api-docs";
					foreach (var description in provider.ApiVersionDescriptions)
						options.SwaggerEndpoint($"/api-docs/{description.GroupName}/docs.json", description.GroupName.ToUpperInvariant());
				});
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}

}
