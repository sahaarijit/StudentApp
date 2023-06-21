using StudentApp.Types;
using System.Net;

namespace StudentApp.Model
{
	public class Success : ISuccess
	{
		private int StatusCode;
		private string? Message;
		private dynamic? Result;

		public async Task SuccessResponseFormat(dynamic? result, string? message)
		{
			StatusCode = (int)HttpStatusCode.OK;
			Message = message ?? null;
			Result = result;
		}
	}
}
