using Microsoft.AspNetCore.Mvc;
using StudentApp.Data;
using StudentApp.Dto;
using StudentApp.Entity;

namespace StudentApp.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TeacherController : ControllerBase
	{
		private ApplicationDbContext _context;
		public TeacherController(ApplicationDbContext context)
		{
			_context = context;
		}

		[HttpPost]
		public IActionResult Teacher(TeacherDto teacherDto)
		{
			var user = _context.Users.FirstOrDefault(s => s.Id == teacherDto.TeacherId && s.RoleId == 2);
			if (user == null) {
				return BadRequest("Teacher is not there with the given id");
			}
			else {
				var Teacher = new Teacher {
					TeacherId = teacherDto.TeacherId
				};
				_context.Teachers.Add(Teacher);
				_context.SaveChanges();
				return Ok("Teacher created");
			}
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			var teacher = _context.Teachers.FirstOrDefault(s => s.TeacherId == id);
			if (teacher == null) {
				return BadRequest("teacher not found");
			}
			if (teacher.IsDeleted == false) {
				teacher.IsDeleted = true;
				teacher.DeletedAt = DateTime.Now;
				_context.SaveChanges();
				return Ok("teacher is successfully deleted");
			}
			else {
				teacher.IsDeleted = false;
				teacher.DeletedAt = null;
				_context.SaveChanges();
				return Ok("teacher is successfully updated");
			}
		}


		[HttpPut("{id}")]
		public IActionResult Put(int id, TeacherDto teacherDto)
		{
			var user = _context.Users.FirstOrDefault(s => s.Id == teacherDto.TeacherId && s.RoleId == 1);
			if (user == null) {
				return BadRequest("teacher is not there with the given id");
			}
			else {
				var teacher = new Teacher {
					Id = id,
					TeacherId = teacherDto.TeacherId
				};
				_context.Teachers.Update(teacher);
				_context.SaveChanges();
				return Ok("teacher Updated");
			}
		}

		[HttpGet]
		public IEnumerable<Teacher> GetStudents()
		{
			return _context.Teachers.Where(s => s.IsDeleted == false);
		}
	}
}
