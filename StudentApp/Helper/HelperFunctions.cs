//using Microsoft.IdentityModel.Tokens;
//using StudentApp.Data;
//using StudentApp.Dto;
//using StudentApp.Models;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;

//namespace StudentApp.Helper
//{
//	public class HelperFunctions
//	{
//		private ApplicationDbContext _context;
//		private IConfiguration _configuration;

//		public HelperFunctions(ApplicationDbContext context, IConfiguration configuration)
//		{
//			_context = context;
//			_configuration = configuration;
//		}

//		public User GetUser(string email, string password, int roleId)
//		{
//			return _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password && u. == roleId);
//		}

//		public string GenerateToken(UserDto user)
//		{
//			var claims = new[] {
//						new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
//						new Claim("Email", user.Email),
//						new Claim("Password",user.Password),
//						new Claim("RoleId",user.RoleId.ToString())
//					};

//			var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
//			var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
//			var token = new JwtSecurityToken(
//				_configuration["Jwt:Issuer"],
//				_configuration["Jwt:Audience"],
//				claims,
//				expires: DateTime.UtcNow.AddMinutes(10),
//				signingCredentials: signIn);

//			return new JwtSecurityTokenHandler().WriteToken(token);
//		}
//	}
//}
