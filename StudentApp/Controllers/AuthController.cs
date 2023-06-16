using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using StudentApp.Data;
using StudentApp.Dto;
using StudentApp.Entity;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;

namespace StudentApp.Controllers
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

		[HttpPost]
		[Route("signUp")]
		public IActionResult CreateUser([FromBody] UserDto userDto)
		{

			var user = new User {
				FirstName = userDto.FirstName,
				LastName = userDto.LastName,
				RoleId = userDto.RoleId,
				Email = userDto.Email,
				Password = userDto.Password
			};

			_context.Users.Add(user);
			_context.SaveChanges();
			return Ok(new { message = "User successfully created", Status = HttpStatusCode.OK, result = user });
		}


		[HttpPost]
		[Route("login")]
		public IActionResult Login(LoginDto login)
		{
			var getUser = _context.Users.FirstOrDefault(u => u.Email == login.Email && u.Password == login.Password);
			if (getUser != null) {
				int roleId = getUser.RoleId;
				var getRole = _context.Roles.FirstOrDefault(r => r.Id == roleId);
				string role = getRole.Name;
				if (role == null) {
					return BadRequest(new { error = "Role not found", status = HttpStatusCode.NotFound });
				}
				else {
					var claims = new[] {
						new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
						new Claim("Email", login.Email),
						new Claim(ClaimTypes.Role,role)
					};

					var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
					var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
					var token = new JwtSecurityToken(
						_configuration["Jwt:Issuer"],
						_configuration["Jwt:Audience"],
						claims,
						expires: DateTime.UtcNow.AddMinutes(10),
						signingCredentials: signIn);

					var jwt = new JwtSecurityTokenHandler().WriteToken(token);
					return Ok(new { jwtToken = jwt });
				}
			}
			else {
				return BadRequest(new { error = "Invalid Credinals", status = HttpStatusCode.BadRequest });
			}

		}

		[HttpPut("{id}")]
		[Authorize]
		public IActionResult edit(int id, UserDto userDto)
		{
			var user1 = new User {
				Id = id,
				FirstName = userDto.FirstName,
				LastName = userDto.LastName,
				RoleId = userDto.RoleId,
				Email = userDto.Email,
				Password = userDto.Password
			};
			_context.Users.Update(user1);
			_context.SaveChanges();
			return Ok(new { message = "User successfully updated", Status = HttpStatusCode.OK, result = user1 });
		}


		[HttpDelete("{id}")]
		[Authorize]
		public IActionResult delete(int id)
		{
			var user = _context.Users.FirstOrDefault(s => s.Id == id);
			if (user == null) {
				return BadRequest(new { error = "User is not found", status = HttpStatusCode.NotFound });
			}
			if (user.IsDeleted == false) {
				user.IsDeleted = true;
				user.DeletedAt = DateTime.Now;
				_context.SaveChanges();
				return Ok(new { message = "user is successfully deleted", status = HttpStatusCode.OK, result = _context.Users.Where(s => s.IsDeleted == false) });
			}
			else {
				user.IsDeleted = false;
				user.DeletedAt = null;
				_context.SaveChanges();
				return Ok(new { message = "user is successfully updated", status = HttpStatusCode.OK, result = _context.Users.Where(s => s.IsDeleted == false) });
			}
		}

		[HttpGet]
		[Authorize]
		[Route("getAllUsers")]
		public IActionResult users()
		{
			return Ok(new { message = "All users are fetched successfully", status = HttpStatusCode.OK, result = _context.Users.Where(s => s.IsDeleted == false) });
		}

		[HttpGet("{id}")]
		[Authorize]
		public IActionResult User(int id)
		{
			var user = _context.Users.FirstOrDefault(s => s.Id == id);
			if (user == null) {
				return BadRequest(new { error = "user not found with given id", status = HttpStatusCode.NotFound });
			}
			else {
				return Ok(new { meassage = "User fetched successfully", status = HttpStatusCode.OK, result = user });
			}
		}
	}
}





