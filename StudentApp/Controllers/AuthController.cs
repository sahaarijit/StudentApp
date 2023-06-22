using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using StudentApp.Data;
using StudentApp.Dto;
using StudentApp.Entity;
using StudentApp.Exceptions;
using StudentApp.Types;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;


namespace StudentApp.Controllers
{
	[Route("api/auth")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly ApplicationDbContext _context;
		private readonly ICustomResponse _response;
		private readonly IConfiguration _configuration;
		private readonly ILogger<AuthController> _logger;

		public AuthController(
			ApplicationDbContext context,
			ICustomResponse response,
			IConfiguration configuration,
			ILogger<AuthController> logger)
		{
			_context = context;
			_response = response;
			_configuration = configuration;
			_logger = logger;
		}

		[HttpPost]
		[Route("signup")]
		public async Task<IActionResult> CreateUser([FromBody] UserDto userDto)
		{
			_logger.LogInformation("Signup process initiated...");
			var role = _context.Roles.First(r => r.Name == userDto.role);
			if (role != null) {
				var user = new User {
					FirstName = userDto.firstName,
					LastName = userDto.lastName,
					RoleId = role != null ? role.Id : default,
					Email = userDto.email,
					Password = userDto.password
				};
				_context.Users.Add(user);
				try {
					_context.SaveChanges();
				}
				catch {
					throw new DbUpdateException("Same entity details");
				}
				_logger.LogInformation("Process completed...");
				var data = await _response.SuccessResponse(user, "Registration successful");
				return Ok(data);
			}
			else {
				throw new NotFoundException("Role not found");
			}
		}

		[HttpPost]
		[Route("login")]
		public async Task<IActionResult> Login(LoginDto login)
		{
			var getUser = _context.Users.FirstOrDefault(u => u.Email == login.Email && u.Password == login.Password);
			if (getUser != null) {
				int roleId = getUser.RoleId;
				var getRole = _context.Roles.FirstOrDefault(r => r.Id == roleId);
				string role = getRole.Name;
				if (role == null) {
					throw new NotFoundException("Role not found");
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
					var jwtToken = await _response.SuccessResponse(jwt, "Jwt token generated successful");
					return Ok(jwtToken);
				}
			}
			else {
				throw new BadRequestException("Invalid credinals");
			}

		}

		[HttpPut("{id}")]
		[Authorize]
		public async Task<IActionResult> Edit(int id, UserDto userDto)
		{

			var userDetails = _context.Users.FirstOrDefault(x => x.Id == id);
			if (userDetails == null) {
				throw new NotFoundException("User not found");
			}
			else {
				var role = _context.Roles.First(r => r.Name == userDto.role);
				if (role == null) {
					throw new NotFoundException("Role not found");
				}
				else {


					userDetails.FirstName = userDto.firstName;
					userDetails.LastName = userDto.lastName;
					userDetails.RoleId = role != null ? role.Id : default;
					userDetails.Email = userDto.email;
					userDetails.Password = userDto.password;

					_context.Users.Update(userDetails);
					_context.SaveChanges();
					var data = await _response.SuccessResponse(userDetails, "User successfully updated");
					return Ok(data);
				}
			}
		}


		[HttpDelete("{id}")]
		[Authorize]
		public async Task<IActionResult> Delete(int id)
		{
			var user = _context.Users.FirstOrDefault(s => s.Id == id);
			if (user == null) {
				throw new NotFoundException("User not found");
			}
			else if (user.IsDeleted == false) {
				user.IsDeleted = true;
				user.DeletedAt = DateTime.Now;
				_context.SaveChanges();
				var data = await _response.SuccessResponse(_context.Users.Where(s => s.IsDeleted == false), "user is successfully deleted");
				return Ok(data);
			}
			else {
				user.IsDeleted = false;
				user.DeletedAt = null;
				_context.SaveChanges();
				var data = await _response.SuccessResponse(_context.Users.Where(s => s.IsDeleted == false), "user is successfully updated");
				return Ok(data);
			}
		}

		[HttpGet]
		[Authorize]
		[Route("getAllUsers")]
		public async Task<IActionResult> Users()
		{
			var data = await _response.SuccessResponse(_context.Users.Where(s => s.IsDeleted == false), "All users are fetched successfully");
			return Ok(data);
		}

		[HttpGet("{id}")]
		[Authorize]
		public async Task<IActionResult> User(int id)
		{
			var user = _context.Users.FirstOrDefault(s => s.Id == id);
			if (user == null) {
				throw new NotFoundException("User not found");
			}
			else {
				var data = await _response.SuccessResponse(user, "User fetched successfully");
				return Ok(data);
			}
		}
	}
}
