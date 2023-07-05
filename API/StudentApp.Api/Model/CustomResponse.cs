using StudentApp.Api.Types;

namespace StudentApp.Api.Model
{
	public class CustomResponse : ICustomResponse
	{
		private readonly ISuccess _success;

		public CustomResponse(ISuccess success)
		{
			_success = success;
		}

		public async Task<ISuccess> SuccessResponse(dynamic? result, string? message)
			=> await _success.SuccessResponseFormat(result, message);

		public static IError ErrorResponse(HttpResponse response, Exception ex)
			=> Error.ErrorResponseFormat(response, ex);
	}
}