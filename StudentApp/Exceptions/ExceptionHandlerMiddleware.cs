using Newtonsoft.Json;
using StudentApp.Model;
using System.Diagnostics;
using System.Net;

namespace StudentApp.Exceptions
{
	public class ExceptionHandlerMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<ExceptionHandlerMiddleware> _logger;

		public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
		{
			_next = next;
			_logger = logger;
		}

		public async Task InvokeAsync(HttpContext httpContext)
		{
			try {
				await _next(httpContext);
			}
			catch (Exception ex) {
				await HandleExceptionAsync(httpContext, ex);
			}
		}

		private async Task HandleExceptionAsync(HttpContext context, Exception exception)
		{
			context.Response.ContentType = "application/json";
			var response = context.Response;
			var errorResponse = new Error {
				StatusCode = 0,
				Message = "",
				StackTrace = default(StackTrace)
			};

			switch (exception) {
				case ApplicationException ex: {
					response.StatusCode = (int)HttpStatusCode.BadRequest;
					errorResponse.StatusCode = response.StatusCode;
					errorResponse.Message = ex.Message;
					errorResponse.StackTrace = ex.StackTrace;
					break;
				}
				case NotFoundException ex: {
					response.StatusCode = (int)HttpStatusCode.NotFound;
					errorResponse.StatusCode = response.StatusCode;
					errorResponse.Message = ex.Message;
					errorResponse.StackTrace = ex.StackTrace;
					break;
				}
				case UnAuthorizationAccessException ex: {
					response.StatusCode = (int)HttpStatusCode.Unauthorized;
					errorResponse.StatusCode = response.StatusCode;
					errorResponse.Message = ex.Message;
					errorResponse.StackTrace = ex.StackTrace;
					break;
				}
				case ForbiddenException ex: {
					response.StatusCode = (int)HttpStatusCode.Forbidden;
					errorResponse.StatusCode = response.StatusCode;
					errorResponse.Message = ex.Message;
					errorResponse.StackTrace = ex.StackTrace;
					break;
				}
				case BadRequestException ex: {
					response.StatusCode = (int)HttpStatusCode.BadRequest;
					errorResponse.StatusCode = response.StatusCode;
					errorResponse.Message = ex.Message;
					errorResponse.StackTrace = ex.StackTrace;
					break;
				}
				default: {
					response.StatusCode = (int)HttpStatusCode.InternalServerError;
					errorResponse.StatusCode = response.StatusCode;
					errorResponse.Message = exception.Message;
					errorResponse.StackTrace = exception.StackTrace;
					break;
				}
			}
			//_logger.LogError(exception.Message);
			var result = JsonConvert.SerializeObject(errorResponse);
			await context.Response.WriteAsync(result);
		}
	}
}
