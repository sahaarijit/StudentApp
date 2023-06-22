using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentApp.Data;
using StudentApp.Dto;
using StudentApp.Entity;
using StudentApp.Exceptions;
using StudentApp.Types;

namespace StudentApp.Controllers
{
	[Route("api/student")]
	[ApiController]
	public class StudentController : ControllerBase
	{
		private ApplicationDbContext _context;
		ICustomResponse _response;
		public StudentController(ApplicationDbContext context, ICustomResponse response)
		{
			_context = context;
			_response = response;
		}

		[HttpPost]
		[Authorize(Roles = "Student")]
		[Route("asignStudent")]
		public async Task<IActionResult> Student(StudentDto studentDto)
		{
			var user = _context.Users.FirstOrDefault(s => s.Id == studentDto.StudentId && s.RoleId == 1);
			if (user == null) {
				throw new BadHttpRequestException("Student is not available with given id");
			}
			else {
				var student = new Student {
					UserId = studentDto.StudentId
				};
				_context.Students.Add(student);
				try {
					_context.SaveChanges();
				}
				catch {
					throw new DbUpdateException("Same entity details");
				}

				var data = await _response.SuccessResponse(student, "student created successfully");
				return Ok(data);
			}
		}

		[HttpDelete("{id}")]
		[Authorize(Roles = "Student")]
		public async Task<IActionResult> Delete(int id)
		{
			var student = _context.Students.FirstOrDefault(s => s.Id == id);
			if (student == null) {
				throw new NotFoundException("Student not found");
			}
			else if (student.IsDeleted == false) {
				student.IsDeleted = true;
				student.DeletedAt = DateTime.Now;
				_context.SaveChanges();
				var data = await _response.SuccessResponse(_context.Students.Where(s => s.IsDeleted == false), "student is successfully deleted");
				return Ok(data);
			}
			else {
				student.IsDeleted = false;
				student.DeletedAt = null;
				_context.SaveChanges();
				var data = await _response.SuccessResponse(_context.Students.Where(s => s.IsDeleted == false), "student is successfully updated");
				return Ok(data);
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
		public async Task<IActionResult> GetStudents()
		{
			var data = await _response.SuccessResponse(_context.Students.Where(s => s.IsDeleted == false), "All studnets are fetched successfully");
			return Ok(data);
		}


		[HttpGet("{id}")]
		[Authorize]
		public async Task<IActionResult> result(int id)
		{
			var student = _context.Students.FirstOrDefault(s => s.Id == id);
			if (student == null) {
				throw new NotFoundException("student not found with given id");
			}
			else {
				var data = await _response.SuccessResponse(student, "student fetched successfully");
				return Ok(data);
			}
		}
	}

}
