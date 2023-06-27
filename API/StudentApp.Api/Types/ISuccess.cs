namespace StudentApp.Types
{
	public interface ISuccess
	{
		public Task<ISuccess> SuccessResponseFormat(dynamic? result, string? message);
	}
}
