using StudentApp.Types;
using System.Net;

namespace StudentApp.Model
{
	public class Success : ISuccess
	{
		public int StatusCode { get; set; }
		public string? Message { get; set; }
		public dynamic? Result { get; set; }

		public async Task<ISuccess> SuccessResponseFormat(dynamic? result, string? message)
		{
			return new Success {
				StatusCode = (int)HttpStatusCode.OK,
				Message = message ?? null,
				Result = result
			};
		}
	}
}
