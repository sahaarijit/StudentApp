using System.Net;

namespace StudentApp.Model
{
	public class ErrorResponse
	{
		public int statusCode;
		public bool success;
		public string message;

		public ErrorResponse(int? statusCode, string? message)
			=> new ErrorResponse(statusCode, message) {
				statusCode = (int)statusCode,
				success = false,
				message = message ?? ""
			};
	}

	public class SuccessResponse
	{
		private int? statusCode;
		private bool? success;
		private string? message;
		private dynamic? stackTrace;
		private dynamic? result;

		public SuccessResponse(dynamic? result, string? message)
			=> new SuccessResponse(result, message) {
				statusCode = (int)HttpStatusCode.OK,
				success = true,
				message = message ?? null,
				stackTrace = null,
				result = result
			};
	}
}
