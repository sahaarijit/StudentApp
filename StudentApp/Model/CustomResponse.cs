using StudentApp.Types;

namespace StudentApp.Model
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
	}
}