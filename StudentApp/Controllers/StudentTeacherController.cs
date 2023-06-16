using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentApp.Data;
using StudentApp.Dto;
using StudentApp.Entity;
using System.IdentityModel.Tokens.Jwt;
using System.Net;

namespace StudentApp.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class StudentTeacherController : ControllerBase
	{
		private ApplicationDbContext _context;

		public StudentTeacherController(ApplicationDbContext context)
		{
			_context = context;
		}

		[HttpPost]
		[Authorize(Roles = "Teacher")]
		[Route("assignStudentTeacher")]
		public IActionResult StudentTeacher(StudentTeacherDto studentTeacher)
		{
			try {
				var studentTeacher1 = new StudentTeacher {
					StudentId = studentTeacher.StudentId,
					TeacherId = studentTeacher.TeacherId
				};
				_context.StudentTeacher.Add(studentTeacher1);
				_context.SaveChanges();
				return Ok(new { message = "Student and teacher are assigned to each other", status = HttpStatusCode.OK, result = studentTeacher1 });
			}
			catch (Exception ex) {
				return BadRequest(ex.Message);
			}
		}


		[HttpDelete("{id}")]
		[Authorize(Roles = "Teacher")]
		public IActionResult Delete(int id)
		{
			var studetTeacher = _context.StudentTeacher.FirstOrDefault(s => s.Id == id);
			if (studetTeacher == null) {
				return BadRequest(new { error = "StudentTeacher not found with given id", status = HttpStatusCode.NotFound });
			}
			if (studetTeacher.IsDeleted == false) {
				studetTeacher.IsDeleted = true;
				studetTeacher.DeletedAt = DateTime.Now;
				_context.SaveChanges();
				return Ok(new { message = "StudentTeacher is successfully deleted", status = HttpStatusCode.OK, result = _context.StudentTeacher.Where(s => s.IsDeleted == false) });
			}
			else {
				studetTeacher.IsDeleted = false;
				studetTeacher.DeletedAt = null;
				_context.SaveChanges();
				return Ok(new { message = "StudentTeacher is successfully updated", status = HttpStatusCode.OK, result = _context.StudentTeacher.Where(s => s.IsDeleted == false) });
			}
		}


		//[HttpPut("{id}")]
		//[Authorize(Roles = "Teacher")]
		//public IActionResult actionResult(int id, StudentTeacherDto studentTeacherDto)
		//{
		//	var studetTeacher = _context.StudentTeacher.FirstOrDefault(s => s.Id == id);
		//	if (studetTeacher == null) {
		//		return BadRequest(new { error = "StudentTeacher not found", status = HttpStatusCode.NotFound });
		//	}

		//	else {
		//		var studentTeacher1 = new StudentTeacher {
		//			Id = id,
		//			StudentId = studentTeacherDto.StudentId,
		//			TeacherId = studentTeacherDto.TeacherId
		//		};
		//		_context.StudentTeacher.Update(studentTeacher1);
		//		_context.SaveChanges();
		//		return Ok(new { Message = "StudentTeacher are updated to each other", status = HttpStatusCode.OK, result = studentTeacher1 });

		//	}
		//}

		//[HttpGet]
		//[Authorize]
		//[Route("getStudentTeacher")]
		//public IActionResult GetStudents()
		//{
		//	return Ok(new { message = " All StudentTeacher are fetched successfully ", status = HttpStatusCode.OK, result = _context.StudentTeacher.Where(s => s.IsDeleted == false) });
		//}


		//Teacher can see list of students assigned
		[HttpGet]
		[Route("assignedStudents")]
		[Authorize(Roles = "Teacher")]
		public IActionResult GetResult(string jwtToken)
		{
			var handler = new JwtSecurityTokenHandler();
			var token = handler.ReadJwtToken(jwtToken);

			string email = token.Payload["Email"].ToString();
			var users = _context.Users.Where(u => u.Email == email);
			var studentTeacher = from user in users
								 join teacher in _context.Teachers on user.Id equals teacher.UserId
								 join studentteacher in _context.StudentTeacher on teacher.Id equals studentteacher.TeacherId
								 join student in _context.Students on studentteacher.StudentId equals student.Id
								 select new { student.user.FirstName };

			var teacherData = from user in users
							  join teacher in _context.Teachers on user.Id equals teacher.UserId
							  select new { user.FirstName };

			var data = new { teacherDetails = teacherData, studentsData = studentTeacher };

			return Ok(data);
		}


		//student can see list of teachers assigned
		[HttpGet]
		[Route("assignedTeachers")]
		[Authorize(Roles = "Student")]
		public IActionResult GetTeachers(string jwtToken)
		{
			var handler = new JwtSecurityTokenHandler();
			var token = handler.ReadJwtToken(jwtToken);

			string email = token.Payload["Email"].ToString();
			var users = _context.Users.Where(u => u.Email == email);
			var studentTeacher = from user in users
								 join student in _context.Students on user.Id equals student.UserId
								 join studentteacher in _context.StudentTeacher on student.Id equals studentteacher.StudentId
								 join teacher in _context.Teachers on studentteacher.TeacherId equals teacher.Id
								 select new { teacher.user.FirstName };

			var studentData = from user in users
							  join student in _context.Students on user.Id equals student.UserId
							  select new { user.FirstName };

			var data = new { studentsData = studentData, teacherDetails = studentTeacher };

			return Ok(data);
		}

	}
}
