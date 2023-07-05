using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentApp.Api.Data;
using StudentApp.Api.Dto;
using StudentApp.Api.Exceptions;
using StudentApp.Api.Types;
using StudentApp.Core.Entities;
using System.IdentityModel.Tokens.Jwt;

namespace StudentApp.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class StudentTeacherController : ControllerBase
	{
		private ApplicationDbContext _context;
		ICustomResponse _response;

		public StudentTeacherController(ApplicationDbContext context, ICustomResponse response)
		{
			_context = context;
			_response = response;
		}

		[HttpPost]
		[Authorize(Roles = "Teacher")]
		[Route("assignStudentTeacher")]
		public async Task<IActionResult> StudentTeacher(StudentTeacherDto studentTeacher)
		{
			try {
				var studentTeacher1 = new StudentTeacher {
					StudentId = studentTeacher.StudentId,
					TeacherId = studentTeacher.TeacherId
				};
				_context.StudentTeacher.Add(studentTeacher1);
				try {
					_context.SaveChanges();
				}
				catch {
					throw new DbUpdateException("Same entity details");
				}

				var data = await _response.SuccessResponse(studentTeacher1, "Student and teacher are assigned to each other");
				return Ok(data);
			}
			catch (Exception ex) {
				throw new BadRequestException(ex.Message);
			}
		}


		[HttpDelete("{id}")]
		[Authorize(Roles = "Teacher")]
		public async Task<IActionResult> Delete(int id)
		{
			var studetTeacher = _context.StudentTeacher.FirstOrDefault(s => s.Id == id);
			if (studetTeacher == null) {
				throw new NotFoundException("StudentTeacher not found with given id");
			}
			if (studetTeacher.IsDeleted == false) {
				studetTeacher.IsDeleted = true;
				studetTeacher.DeletedAt = DateTime.Now;
				_context.SaveChanges();
				var data = await _response.SuccessResponse(_context.StudentTeacher.Where(s => s.IsDeleted == false), "StudentTeacher is successfully deleted");
				return Ok(data);
			}
			else {
				studetTeacher.IsDeleted = false;
				studetTeacher.DeletedAt = null;
				_context.SaveChanges();
				var data = await _response.SuccessResponse(_context.StudentTeacher.Where(s => s.IsDeleted == false), "StudentTeacher is successfully updated");
				return Ok(data);
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
		public async Task<IActionResult> GetResult(string jwtToken)
		{
			var handler = new JwtSecurityTokenHandler();
			var token = handler.ReadJwtToken(jwtToken);

			string email = token.Payload["Email"].ToString();
			var users = _context.Users.Where(u => u.Email == email && u.RoleId == 2);
			var studentTeacher = from user in users
								 join teacher in _context.Teachers on user.Id equals teacher.UserId
								 join studentteacher in _context.StudentTeacher on teacher.Id equals studentteacher.TeacherId
								 join student in _context.Students on studentteacher.StudentId equals student.Id
								 select new { student.user.FirstName };

			var teacherData = from user in users
							  join teacher in _context.Teachers on user.Id equals teacher.UserId
							  select new { user.FirstName };

			var data = await _response.SuccessResponse(new { teacherDetails = teacherData, studentsData = studentTeacher }, "Here are the students assigned to the teacher");

			return Ok(data);
		}


		//student can see list of teachers assigned
		[HttpGet]
		[Route("assignedTeachers")]
		[Authorize(Roles = "Student")]
		public async Task<IActionResult> GetTeachers(string jwtToken)
		{
			var handler = new JwtSecurityTokenHandler();
			var token = handler.ReadJwtToken(jwtToken);

			string email = token.Payload["Email"].ToString();
			var users = _context.Users.Where(u => u.Email == email && u.RoleId == 1);
			var studentTeacher = from user in users
								 join student in _context.Students on user.Id equals student.UserId
								 join studentteacher in _context.StudentTeacher on student.Id equals studentteacher.StudentId
								 join teacher in _context.Teachers on studentteacher.TeacherId equals teacher.Id
								 select new { teacher.user.FirstName };

			var studentData = from user in users
							  join student in _context.Students on user.Id equals student.UserId
							  select new { user.FirstName };

			var data = await _response.SuccessResponse(new { studentsData = studentData, teacherDetails = studentTeacher }, "Here are the teachers assigned to the student");

			return Ok(data);
		}

	}
}
