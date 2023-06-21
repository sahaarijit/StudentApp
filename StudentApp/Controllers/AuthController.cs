﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using StudentApp.Data;
using StudentApp.Dto;
using StudentApp.Entity;
using StudentApp.Exceptions;
using StudentApp.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;

namespace StudentApp.Controllers
{
	[Route("api/auth")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly ApplicationDbContext _context;
		private readonly IConfiguration _configuration;
		private readonly ILogger<AuthController> _logger;

		public AuthController(
			ApplicationDbContext context,
			IConfiguration configuration,
			ILogger<AuthController> logger)
		{
			_context = context;
			_configuration = configuration;
			_logger = logger;
		}

		[HttpPost]
		[Route("signup")]
		public async Task<IActionResult> CreateUser([FromBody] UserDto userDto)
		{
			try {
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
					_context.SaveChanges();
					_logger.LogInformation("Process completed...");

					return Ok(new SuccessResponse { result = user, message = "Registration successful" });
				}
				else {
					throw new BadRequestException("Role not found");
				}
			}
			catch (Exception e) {
				//_logger.LogError(e.StackTrace);
				return BadRequest(e);
			}
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
		public IActionResult Edit(int id, UserDto userDto)
		{
			var role = _context.Roles.First(r => r.Name == userDto.role);
			var user = new User {
				FirstName = userDto.firstName,
				LastName = userDto.lastName,
				RoleId = role != null ? role.Id : default,
				Email = userDto.email,
				Password = userDto.password
			};
			_context.Users.Update(user);
			_context.SaveChanges();
			return Ok(new { message = "User successfully updated", Status = HttpStatusCode.OK, result = user });
		}


		[HttpDelete("{id}")]
		[Authorize]
		public IActionResult Delete(int id)
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
		//[Authorize]
		[Route("getAllUsers")]
		public async Task<IActionResult> Users()
		{
			var data = _context.Users.Where(user => user.IsDeleted == false);
			return Ok(new SuccessResponse { result = data, message = "All users are fetched successfully" });
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