using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentApp.Data;
using StudentApp.Dto;
using StudentApp.Helper;
using StudentApp.Models;
using StudentApp.Validator;

namespace StudentApp.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private ApplicationDbContext _context;
		private HelperFunctions _helperFunction;

		public AuthController(ApplicationDbContext context, HelperFunctions helperFunction)
		{
			_context = context;
			_helperFunction = helperFunction;
		}

		[HttpPost("signUp")]
		public IActionResult SignUp([FromBody] User user)
		{
			var validator = new UserValidator();
			var result = validator.Validate(user);
			if (!result.IsValid) {
				return BadRequest();
			}
			else {
				_context.Users.Add(user);
				_context.SaveChanges();
				return Ok(user);
			}
		}


		[HttpPost("logIn")]
		public IActionResult LogIn([FromBody] UserDto user)
		{
			var validator = new UserDtoValidator();
			var result = validator.Validate(user);
			if (!result.IsValid) {
				return BadRequest();
			}
			else {
				var getUser = _helperFunction.GetUser(user.email, user.password, user.RoleId);
				if (getUser != null) {

					var token = _helperFunction.GenerateToken(user);
					return Ok(token);
				}
				else {
					return BadRequest("Incorrect Credinals");
				}
			}
		}



		[HttpPut]
		[Route("edit")]
		[Authorize]
		public IActionResult Update([FromBody] User user)
		{
			var validator = new UserValidator();
			var result = validator.Validate(user);
			if (!result.IsValid) {
				return BadRequest();
			}
			else {
				_context.Users.Update(user);
				_context.SaveChanges();
				return Ok(user);
			}
		}
	}
}



