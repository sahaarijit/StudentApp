using Newtonsoft.Json;
using StudentApp.Model;
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
			var errorResponse = new ErrorResponse(default, default);

			switch (exception) {
				case ApplicationException ex: {
					if (ex.Message.Contains("Invalid Token")) {
						response.StatusCode = (int)HttpStatusCode.Forbidden;
						errorResponse = new ErrorResponse(response.StatusCode, ex.Message);
						break;
					}
					response.StatusCode = (int)HttpStatusCode.BadRequest;
					errorResponse = new ErrorResponse(response.StatusCode, ex.Message);
					break;
				}
				case NotFoundException ex: {
					response.StatusCode = (int)HttpStatusCode.NotFound;
					errorResponse = new ErrorResponse(response.StatusCode, ex.Message);
					break;
				}
				case UnAuthorizationAccessException ex: {
					response.StatusCode = (int)HttpStatusCode.Unauthorized;
					errorResponse = new ErrorResponse(response.StatusCode, ex.Message);
					break;
				}
				case ForbiddenException ex: {
					response.StatusCode = (int)HttpStatusCode.Forbidden;
					errorResponse = new ErrorResponse(response.StatusCode, ex.Message);
					break;
				}
				case BadRequestException ex: {
					response.StatusCode = (int)HttpStatusCode.BadRequest;
					errorResponse = new ErrorResponse(response.StatusCode, ex.Message);
					break;
				}
				default: {
					response.StatusCode = (int)HttpStatusCode.InternalServerError;
					errorResponse = new ErrorResponse(response.StatusCode, exception.Message);
					break;
				}
			}
			_logger.LogError(exception.Message);
			var result = JsonConvert.SerializeObject(errorResponse);
			await context.Response.WriteAsync(result);
		}
	}
}
