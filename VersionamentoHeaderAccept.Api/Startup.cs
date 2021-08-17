using System;
using System.IO;
using System.Linq;
using System.Reflection;
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
using VersionamentoHeaderAccept.Api.Swagger;

namespace VersionamentoHeaderAccept.Api
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
					new MediaTypeApiVersionReader("api-version")); //passa o version no header na Key accept. Ex: application/json;api-version=2.1
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
				options.SchemaFilter<SwaggerExcludePropertySchemaFilter>();
				options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

				var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
				var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
				options.IncludeXmlComments(xmlPath);
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
