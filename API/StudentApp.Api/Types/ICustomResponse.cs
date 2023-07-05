namespace StudentApp.Api.Types
{
	public interface ICustomResponse
	{
		public Task<ISuccess> SuccessResponse(dynamic? result, string? message);
	}
}
