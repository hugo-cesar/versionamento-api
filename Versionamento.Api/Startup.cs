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
					new HeaderApiVersionReader("api-version")); //aceita uma nova Key X-version no header
				options.ReportApiVersions = true;
			});

			services.AddVersionedApiExplorer(options =>
			{
				// add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
				// note: the specified format code will format the version as "'v'major[.minor][-status]"
				options.GroupNameFormat = "'v'VVV";

				// note: this option is only necessary when versioning by url segment. the SubstitutionFormat
				// can also be used to control the format of the API version in route templates
				//options.SubstituteApiVersionInUrl = true;
			});

			services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
			services.AddSwaggerGen(options =>
			{
				// add a custom operation filter which sets default values
				options.OperationFilter<SwaggerDefaultValues>();
				options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
			});

			//URL da API em letras minúsculas: Carros -> carros
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
