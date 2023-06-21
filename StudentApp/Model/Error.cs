using StudentApp.Types;

namespace StudentApp.Model
{
	public class Error : IError
	{
		public int StatusCode { get; set; }
		public string Message { get; set; }
		public dynamic StackTrace { get; set; }
	}
}
