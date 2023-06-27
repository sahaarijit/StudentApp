namespace StudentApp.Exceptions
{
	public class UnAuthorizationAccessException : Exception
	{
		public UnAuthorizationAccessException(string msg) : base(msg)
		{

		}
	}
}
