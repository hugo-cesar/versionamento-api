using Microsoft.AspNetCore.Builder;

namespace Versionamento.Api.Infrastructure.Middlewares
{
	public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseApiExceptionHandling(this IApplicationBuilder app)
            => app.UseMiddleware<ApiExceptionHandlingMiddleware>();
    }
}
