using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentApp.Data;
using StudentApp.Dto;
using StudentApp.Entity;
using System.Net;

namespace StudentApp.Controllers
{
	[Route("api/student")]
	[ApiController]
	public class StudentController : ControllerBase
	{
		private ApplicationDbContext _context;
		public StudentController(ApplicationDbContext context)
		{
			_context = context;
		}

		[HttpPost]
		[Authorize(Roles = "Student")]
		[Route("asignStudent")]
		public IActionResult Student(StudentDto studentDto)
		{
			var user = _context.Users.FirstOrDefault(s => s.Id == studentDto.StudentId && s.RoleId == 1);
			if (user == null) {
				return BadRequest(new { error = "Student is not there with the given id", status = HttpStatusCode.NotFound });
			}
			else {
				var student = new Student {
					UserId = studentDto.StudentId
				};
				_context.Students.Add(student);
				_context.SaveChanges();
				return Ok(new { message = "student created", status = HttpStatusCode.OK, Student = student });
			}
		}

		[HttpDelete("{id}")]
		[Authorize(Roles = "Student")]
		public IActionResult Delete(int id)
		{
			var student = _context.Students.FirstOrDefault(s => s.Id == id);
			if (student == null) {
				return BadRequest(new { error = "student not found", status = HttpStatusCode.NotFound });
			}
			if (student.IsDeleted == false) {
				student.IsDeleted = true;
				student.DeletedAt = DateTime.Now;
				_context.SaveChanges();
				return Ok(new { message = "student is successfully deleted", status = HttpStatusCode.OK, result = _context.Students.Where(s => s.IsDeleted == false) });
			}
			else {
				student.IsDeleted = false;
				student.DeletedAt = null;
				_context.SaveChanges();
				return Ok(new { message = "student is successfully updated", status = HttpStatusCode.OK, result = _context.Students.Where(s => s.IsDeleted == false) });
			}
		}


		//[HttpPut("{id}")]
		//[Authorize(Roles = "Student")]

		//public IActionResult Put(int id, StudentDto studentDto)
		//{
		//	var stu = _context.Students.FirstOrDefault(s => s.Id == id);
		//	if (stu == null) {
		//		return BadRequest(new { error = "Student is not there with the given id", status = HttpStatusCode.NotFound });
		//	}
		//	else {
		//		var user = _context.Users.FirstOrDefault(s => s.Id == studentDto.StudentId && s.RoleId == 1);
		//		if (user == null) {
		//			return BadRequest(new { error = "student is invalid", status = HttpStatusCode.NotFound });
		//		}
		//		else {
		//			var student = new Student {
		//				Id = id,
		//				UserId = studentDto.StudentId
		//			};
		//			_context.Students.Update(student);
		//			_context.SaveChanges();
		//			return Ok(new { message = "student Updated", status = HttpStatusCode.OK, result = student });
		//		}
		//	}
		//}

		[HttpGet]
		[Authorize]
		[Route("getAllstudents")]
		public IActionResult GetStudents()
		{
			return Ok(new { message = "All studnets are fetched successfully", status = HttpStatusCode.OK, result = _context.Students.Where(s => s.IsDeleted == false) });
		}

		[HttpGet("{id}")]
		[Authorize]

		public IActionResult result(int id)
		{
			var student = _context.Students.FirstOrDefault(s => s.Id == id);
			if (student == null) {
				return BadRequest(new { error = "student not found with given id", status = HttpStatusCode.NotFound });
			}
			else {
				return Ok(new { message = "student fetched successfully", status = HttpStatusCode.OK, result = student });
			}
		}
	}

}
