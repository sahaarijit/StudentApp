using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentApp.Data;
using StudentApp.Dto;
using StudentApp.Entity;
using System.Net;

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
		[Authorize(Roles = "Teacher")]
		[Route("assignTeacher")]
		public IActionResult Teacher(TeacherDto teacherDto)
		{
			var user = _context.Users.FirstOrDefault(s => s.Id == teacherDto.TeacherId && s.RoleId == 2);
			if (user == null) {
				return BadRequest(new { error = "Teacher is not there with the given id", status = HttpStatusCode.NotFound });
			}
			else {
				var Teacher = new Teacher {
					UserId = teacherDto.TeacherId
				};
				_context.Teachers.Add(Teacher);
				_context.SaveChanges();
				return Ok(new { message = "Teacher Created", status = HttpStatusCode.OK, result = Teacher });
			}
		}

		[HttpDelete("{id}")]
		[Authorize(Roles = "Teacher")]
		public IActionResult Delete(int id)
		{
			var teacher = _context.Teachers.FirstOrDefault(s => s.Id == id);
			if (teacher == null) {
				return BadRequest(new { error = "teacher not found", status = HttpStatusCode.NotFound });
			}
			if (teacher.IsDeleted == false) {
				teacher.IsDeleted = true;
				teacher.DeletedAt = DateTime.Now;
				_context.SaveChanges();
				return Ok(new { message = "teacher is successfully deleted", status = HttpStatusCode.OK, result = _context.Teachers.Where(s => s.IsDeleted == false) });
			}
			else {
				teacher.IsDeleted = false;
				teacher.DeletedAt = null;
				_context.SaveChanges();
				return Ok(new { message = "teacher is successfully updated", status = HttpStatusCode.OK, result = _context.Teachers.Where(s => s.IsDeleted == false) });
			}
		}


		//[HttpPut("{id}")]
		//[Authorize(Roles = "Teacher")]

		//public IActionResult Put(int id, TeacherDto teacherDto)
		//{
		//	var tea = _context.Teachers.FirstOrDefault(s => s.Id == id);
		//	if (tea == null) {
		//		return BadRequest(new { error = "teacher is not there with the given id", status = HttpStatusCode.NotFound });
		//	}
		//	else {
		//		var user = _context.Users.FirstOrDefault(s => s.Id == teacherDto.TeacherId && s.RoleId == 1);
		//		if (user == null) {
		//			return BadRequest(new { error = "teacher is invalid", status = HttpStatusCode.NotFound });
		//		}
		//		else {
		//			var teacher = new Teacher {
		//				Id = id,
		//				UserId = teacherDto.TeacherId
		//			};
		//			_context.Teachers.Update(teacher);
		//			_context.SaveChanges();
		//			return Ok(new { message = "teacher Updated successfully", status = HttpStatusCode.OK, result = teacher });
		//		}
		//	}
		//}

		[HttpGet]
		[Authorize]
		[Route("getAllTeachers")]
		public IActionResult GetTeachers()
		{
			return Ok(new { message = "All teachers are fetched successfully", status = HttpStatusCode.OK, result = _context.Teachers.Where(s => s.IsDeleted == false) });
		}

		[HttpGet("{id}")]
		[Authorize]

		public IActionResult result(int id)
		{
			var teacher = _context.Teachers.FirstOrDefault(s => s.Id == id);
			if (teacher == null) {
				return BadRequest(new { error = "student not found with given id", status = HttpStatusCode.NotFound });
			}
			else {
				return Ok(new { message = "teacher fetched successfully", status = HttpStatusCode.OK, result = teacher });
			}
		}
	}
}

