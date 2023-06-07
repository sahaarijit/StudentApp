//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using StudentApp.Data;
//using StudentApp.Models;
//using StudentApp.Validator;

//namespace StudentApp.Controllers
//{
//	[Route("api/[controller]")]
//	[ApiController]
//	public class StudentController : ControllerBase
//	{

//		private ApplicationDbContext _context;

//		public StudentController(ApplicationDbContext context)
//		{
//			_context = context;
//		}

//		[HttpGet]
//		[Route("GetStudents")]
//		[Authorize]
//		public IEnumerable<User> GetStudents()
//		{
//			var data = _context.Users.Where(u => u.RoleId == 1);
//			return data;
//		}

//		[HttpPut]
//		[Route("Edit")]
//		[Authorize]
//		public IActionResult Edit([FromBody] User user)
//		{
//			if (user.RoleId == 2) {
//				var validator = new UserValidator();
//				var result = validator.Validate(user);
//				if (!result.IsValid) {
//					return BadRequest();
//				}
//				else {
//					_context.Users.Update(user);
//					_context.SaveChanges();
//					return Ok(user);
//				}
//			}
//			else {
//				return BadRequest();
//			}

//		}
//		[HttpDelete("{id}")]
//		[Authorize(Policy = "Teacher")]
//		public IActionResult Delete(int id)
//		{
//			var student = _context.Users.Where(u => u.RoleId == 1 && u.id == id).FirstOrDefault();
//			if (student != null) {
//				_context.Users.Remove(student);
//				_context.SaveChanges();
//				return Ok(student);
//			}
//			else {
//				return BadRequest();
//			}
//		}
//	}
//}
