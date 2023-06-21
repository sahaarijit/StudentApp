namespace StudentApp.Types
{
	public interface ISuccess
	{
		public Task SuccessResponseFormat(dynamic? result, string? message);
	}
}
