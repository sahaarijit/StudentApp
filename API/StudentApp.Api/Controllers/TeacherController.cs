using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentApp.Data;
using StudentApp.Dto;
using StudentApp.Entity;
using StudentApp.Exceptions;
using StudentApp.Types;

namespace StudentApp.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TeacherController : ControllerBase
	{
		private ApplicationDbContext _context;
		ICustomResponse _response;
		public TeacherController(ApplicationDbContext context, ICustomResponse response)
		{
			_context = context;
			_response = response;
		}

		[HttpPost]
		[Authorize(Roles = "Teacher")]
		[Route("assignTeacher")]
		public async Task<IActionResult> Teacher(TeacherDto teacherDto)
		{
			var user = _context.Users.FirstOrDefault(s => s.Id == teacherDto.TeacherId && s.RoleId == 2);
			if (user == null) {
				throw new NotFoundException("Teacher is not there with the given id");
			}
			else {
				var Teacher = new Teacher {
					UserId = teacherDto.TeacherId
				};
				_context.Teachers.Add(Teacher);
				try {
					_context.SaveChanges();
				}
				catch {
					throw new DbUpdateException("Same entity details");
				}

				var data = await _response.SuccessResponse(Teacher, "Teacher Created successfully");
				return Ok(data);
			}
		}

		[HttpDelete("{id}")]
		[Authorize(Roles = "Teacher")]
		public async Task<IActionResult> Delete(int id)
		{
			var teacher = _context.Teachers.FirstOrDefault(s => s.Id == id);
			if (teacher == null) {
				throw new NotFoundException("Teacher is not there with the given id");
			}
			else if (teacher.IsDeleted == false) {
				teacher.IsDeleted = true;
				teacher.DeletedAt = DateTime.Now;
				_context.SaveChanges();
				var data = await _response.SuccessResponse(_context.Teachers.Where(s => s.IsDeleted == false), "teacher is successfully deleted");
				return Ok(data);
			}
			else {
				teacher.IsDeleted = false;
				teacher.DeletedAt = null;
				_context.SaveChanges();
				var data = await _response.SuccessResponse(_context.Teachers.Where(s => s.IsDeleted == false), "teacher is successfully updated");
				return Ok(data);
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
		public async Task<IActionResult> GetTeachers()
		{
			var data = await _response.SuccessResponse(_context.Teachers.Where(s => s.IsDeleted == false), "All teachers are fetched successfully");
			return Ok(data);
		}

		[HttpGet("{id}")]
		[Authorize]

		public async Task<IActionResult> result(int id)
		{
			var teacher = _context.Teachers.FirstOrDefault(s => s.Id == id);
			if (teacher == null) {
				throw new NotFoundException("teacher not found with given id");
			}
			else {
				var data = await _response.SuccessResponse(teacher, "teacher fetched successfully");
				return Ok(data);
			}
		}
	}
}

