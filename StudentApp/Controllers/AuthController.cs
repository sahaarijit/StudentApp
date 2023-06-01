using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SampleProject.Data;
using SampleProject.Models;
using SampleProject.Validator;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SampleProject.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private ApplicationDbContext _context;
		private IConfiguration _configuration;
		public AuthController(ApplicationDbContext context, IConfiguration configuration)
		{
			_context = context;
			_configuration = configuration;
		}

		[HttpPost("signUp")]
		public IActionResult SignUp(User user)
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
		public IActionResult LogIn(User user)
		{
			var validator = new UserValidator();
			var result = validator.Validate(user);
			if (!result.IsValid) {
				return BadRequest();
			}
			else {
				var getUser = GetUser(user.FirstName, user.LastName, user.RoleId, user.email, user.password);
				if (getUser != null) {

					var claims = new[] {
						new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
						new Claim("FirstName",user.FirstName),
						new Claim("LastName",user.LastName),
						new Claim("RoleId",user.RoleId.ToString()),
						new Claim("email", user.email),
						new Claim("password",user.password)
					};

					var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
					var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
					var token = new JwtSecurityToken(
						_configuration["Jwt:Issuer"],
						_configuration["Jwt:Audience"],
						claims,
						expires: DateTime.UtcNow.AddMinutes(10),
						signingCredentials: signIn);

					return Ok(new JwtSecurityTokenHandler().WriteToken(token));
				}
				else {
					return BadRequest("Incorrect Credinals");
				}
			}
		}

		public User GetUser(string firstname, string lastname, int roleId, string email, string password)
		{
			return _context.Users.FirstOrDefault(u => u.FirstName == firstname && u.LastName == lastname && u.RoleId == roleId && u.email == email && u.password == password);
		}

		//[HttpPut]
		//[Authorize]
		//[Route("edit")]
		//public IActionResult Update(User user) {
		//  var validator = new UserValidator();
		//  var result = validator.Validate(user);
		//  if (!result.IsValid) {
		//    return BadRequest();
		//  } else {
		//    _context.Users.Update(user);
		//    _context.SaveChanges();
		//    return Ok(user);
		//  }
		//}

		[HttpPost]
		[Authorize]
		[Route("logout")]
		public IActionResult logout(string token)
		{

		}


	}
}



