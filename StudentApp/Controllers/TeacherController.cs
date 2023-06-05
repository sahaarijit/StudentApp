using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentApp.Data;
using StudentApp.Models;
using StudentApp.Validator;

namespace StudentApp.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize(Policy = "Teacher")]
	public class TeacherController : ControllerBase
	{
		private ApplicationDbContext _context;


		public TeacherController(ApplicationDbContext context)
		{
			_context = context;
		}

		[HttpGet]
		[Route("GetTeachers")]
		public IEnumerable<User> GetTeachers()
		{
			var data = _context.Users.Where(u => u.RoleId == 2);
			return data;
		}

		[HttpPut]
		[Route("TeacherEdit")]

		public IActionResult Edit([FromBody] User user)
		{
			if (user.RoleId == 2) {
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
			else {
				return BadRequest();
			}

		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			var teacher = _context.Users.Where(u => u.RoleId == 2 && u.id == id).FirstOrDefault();
			if (teacher != null) {
				_context.Users.Remove(teacher);
				_context.SaveChanges();
				return Ok(teacher);
			}
			else {
				return BadRequest();
			}
		}
	}
}
