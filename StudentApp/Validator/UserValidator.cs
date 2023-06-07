//using FluentValidation;
//using StudentApp.Models;


//namespace StudentApp.Validator
//{
//	public class UserValidator : AbstractValidator<User>
//	{
//		public UserValidator()
//		{
//			RuleFor(u => u.FirstName).NotNull().NotEmpty();
//			RuleFor(u => u.LastName).NotNull().NotEmpty();
//			RuleFor(u => u.role_id).GreaterThan(0).LessThan(3);
//			RuleFor(u => u.Email).EmailAddress().NotNull();
//			RuleFor(u => u.Password).NotNull().NotEmpty();
//		}
//	}

//}
