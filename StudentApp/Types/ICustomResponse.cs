using StudentApp.Model;

namespace StudentApp.Types
{
	public interface ICustomResponse
	{
		public Task<ISuccess> SuccessResponse(dynamic? result, string? message);
	}
}
