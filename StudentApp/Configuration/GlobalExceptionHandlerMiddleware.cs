using StudentApp.Exceptions;
using System.Net;
using System.Text.Json;

namespace StudentApp.Configuration
{
	public class GlobalExceptionHandlerMiddleware
	{
		private readonly RequestDelegate _next;
		public GlobalExceptionHandlerMiddleware(RequestDelegate next)
		{
			next = _next;
		}
		public async Task Invoke(HttpContext context)
		{
			try {

			}
			catch (Exception ex) {
				await HandleExceptionAsync(context, ex);
			}
		}

		private static Task HandleExceptionAsync(HttpContext context, Exception ex)
		{
			HttpStatusCode status;
			var stackTrace = string.Empty;
			string message = "";

			var exceptionType = ex.GetType();

			if (exceptionType == typeof(NotFoundException)) {
				message = ex.Message;
				stackTrace = ex.StackTrace;
				status = HttpStatusCode.NotFound;
			}


			else if (exceptionType == typeof(UnAuthorizationAccessException)) {
				message = ex.Message;
				stackTrace = ex.StackTrace;
				status = HttpStatusCode.Unauthorized;
			}


			else if (exceptionType == typeof(ForbiddenException)) {
				message = ex.Message;
				stackTrace = ex.StackTrace;
				status = HttpStatusCode.Forbidden;
			}
			else {
				message = ex.Message;
				stackTrace = ex.StackTrace;
				status = HttpStatusCode.InternalServerError;
			}

			var exceptionResult = JsonSerializer.Serialize(new { error = message, stackTrace });
			context.Response.ContentType = "application/json";
			context.Response.StatusCode = (int)status;

			return context.Response.WriteAsync(exceptionResult);
		}
	}
}

