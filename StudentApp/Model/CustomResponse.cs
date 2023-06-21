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
		//public int? statusCode;
		//	public bool? success;
		public string? message { get; set; }
		//public dynamic? stackTrace;
		public dynamic? result { get; set; }

		//public SuccessResponse(dynamic? result, string? message)
		////=> new SuccessResponse(result, message)
		//{
		//	result = result ?? null;
		//	statusCode = (int)HttpStatusCode.OK;
		//	success = true;
		//	message = message ?? null;
		//	stackTrace = null;
		//}
	}
}