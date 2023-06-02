using Microsoft.IdentityModel.Tokens;
using StudentApp.Data;
using StudentApp.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace StudentApp.Helper
{
	public class HelperFunctions
	{
		private ApplicationDbContext _context;
		private IConfiguration _configuration;
		public HelperFunctions(ApplicationDbContext context, IConfiguration configuration)
		{
			_context = context;
			_configuration = configuration;

		}
		public User GetUser(string firstname, string lastname, int roleId, string email, string password)
		{
			return _context.Users.FirstOrDefault(u => u.FirstName == firstname && u.LastName == lastname && u.RoleId == roleId && u.email == email && u.password == password);
		}
		public string GenerateToken(User user)
		{
			var claims = new[] {
						new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
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

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
