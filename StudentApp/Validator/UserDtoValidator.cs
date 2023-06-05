using FluentValidation;
using StudentApp.Dto;

namespace StudentApp.Validator
{
	public class UserDtoValidator : AbstractValidator<UserDto>
	{
		public UserDtoValidator()
		{
			RuleFor(u => u.email).EmailAddress().NotNull();
			RuleFor(u => u.password).NotNull().NotEmpty();
		}
	}
}
