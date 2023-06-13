using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentApp.Data;
using StudentApp.Dto;
using StudentApp.Entity;
using System.IdentityModel.Tokens.Jwt;

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
		public IActionResult StudentTeacher(StudentTeacherDto studentTeacher)
		{

			var studentTeacher1 = new StudentTeacher {
				StudentId = studentTeacher.StudentId,
				TeacherId = studentTeacher.TeacherId
			};
			_context.StudentTeacher.Add(studentTeacher1);
			_context.SaveChanges();
			return Ok("Student and teacher are assigned to each other");
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			var studetTeacher = _context.StudentTeacher.FirstOrDefault(s => s.Id == id);
			if (studetTeacher == null) {
				return BadRequest("teacher not found");
			}
			if (studetTeacher.IsDeleted == false) {
				studetTeacher.IsDeleted = true;
				studetTeacher.DeletedAt = DateTime.Now;
				_context.SaveChanges();
				return Ok("StudentTeacher is successfully deleted");
			}
			else {
				studetTeacher.IsDeleted = false;
				studetTeacher.DeletedAt = null;
				_context.SaveChanges();
				return Ok("StudentTeacher is successfully updated");
			}
		}

		[HttpPut("{id}")]
		public IActionResult actionResult(int id, StudentTeacherDto studentTeacherDto)
		{
			var studentTeacher1 = new StudentTeacher {
				Id = id,
				StudentId = studentTeacherDto.StudentId,
				TeacherId = studentTeacherDto.TeacherId
			};
			_context.StudentTeacher.Update(studentTeacher1);
			_context.SaveChanges();
			return Ok("StudentTeacher are updated to each other");
		}

		[HttpGet]
		public IEnumerable<StudentTeacher> GetStudents()
		{
			return _context.StudentTeacher.Where(s => s.IsDeleted == false);
		}


		//Teacher can see list students assigned
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
								 join teacher in _context.Teachers on user.Id equals teacher.TeacherId
								 join studentteacher in _context.StudentTeacher on teacher.Id equals studentteacher.TeacherId
								 join student in _context.Students on studentteacher.StudentId equals student.Id
								 select new { studentteacher.Id, studentteacher.StudentId, student.user.FirstName, teacher.TeacherId };

			var teacherData = from user in users
							  join teacher in _context.Teachers on user.Id equals teacher.TeacherId
							  //join studentteacher in _context.StudentTeacher on teacher.Id equals studentteacher.TeacherId
							  select new { user.FirstName, teacher.Id };

			var data = new { teacherDetails = teacherData, studentsData = studentTeacher };

			return Ok(data);
		}


		//student can see list teachers assigned
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
								 join student in _context.Students on user.Id equals student.StudentId
								 join studentteacher in _context.StudentTeacher on student.Id equals studentteacher.StudentId
								 join teacher in _context.Teachers on studentteacher.TeacherId equals teacher.Id
								 select new { studentteacher.Id, studentteacher.TeacherId, teacher.user.FirstName };

			var studentData = from user in users
							  join student in _context.Students on user.Id equals student.StudentId
							  //join studentteacher in _context.StudentTeacher on teacher.Id equals studentteacher.TeacherId
							  select new { user.FirstName, student.Id };

			var data = new { studentsData = studentData, teacherDetails = studentTeacher };

			return Ok(data);
		}

	}
}
