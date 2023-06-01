using FluentValidation;
using SampleProject.Models;


namespace SampleProject.Validator
{
	public class UserValidator : AbstractValidator<User>
	{
		public UserValidator()
		{
			RuleFor(u => u.FirstName).NotNull().NotEmpty();
			RuleFor(u => u.LastName).NotNull().NotEmpty();
			RuleFor(u => u.RoleId).GreaterThan(0).LessThan(3);
			RuleFor(u => u.email).EmailAddress().NotNull();
			RuleFor(u => u.password).NotNull().NotEmpty();
		}
	}

}
