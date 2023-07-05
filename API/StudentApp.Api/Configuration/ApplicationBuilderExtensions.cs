using StudentApp.Api.Exceptions;

namespace StudentApp.Api.Configuration
{
	public static class ApplicationBuilderExtensions
	{
		public static IApplicationBuilder AddGlobalErrorHandler(this IApplicationBuilder applicationBuilder)
		=> applicationBuilder.UseMiddleware<ExceptionHandlerMiddleware>();
	}
}
