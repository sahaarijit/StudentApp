using Newtonsoft.Json;
using StudentApp.Api.Model;
using System.Net;

namespace StudentApp.Api.Exceptions
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
			var errorResponse = CustomResponse.ErrorResponse(response, exception);

			switch (exception) {
				case ApplicationException ex: {
					response.StatusCode = (int)HttpStatusCode.BadRequest;
					errorResponse = CustomResponse.ErrorResponse(response, ex);
					break;
				}
				case NotFoundException ex: {
					response.StatusCode = (int)HttpStatusCode.NotFound;
					errorResponse = CustomResponse.ErrorResponse(response, ex);
					break;
				}
				case UnAuthorizationAccessException ex: {
					response.StatusCode = (int)HttpStatusCode.Unauthorized;
					errorResponse = CustomResponse.ErrorResponse(response, ex);
					break;
				}
				case ForbiddenException ex: {
					response.StatusCode = (int)HttpStatusCode.Forbidden;
					errorResponse = CustomResponse.ErrorResponse(response, ex);
					break;
				}
				case BadRequestException ex: {
					response.StatusCode = (int)HttpStatusCode.BadRequest;
					errorResponse = CustomResponse.ErrorResponse(response, ex);
					break;
				}
				case DbUpdateException ex: {
					response.StatusCode = (int)HttpStatusCode.BadRequest;
					errorResponse = CustomResponse.ErrorResponse(response, ex);
					break;
				}
				default: {
					response.StatusCode = (int)HttpStatusCode.InternalServerError;
					errorResponse = CustomResponse.ErrorResponse(response, exception);
					break;
				}
			}
			//_logger.LogError(exception.Message);
			var result = JsonConvert.SerializeObject(errorResponse);
			await context.Response.WriteAsync(result);
		}
	}
}
