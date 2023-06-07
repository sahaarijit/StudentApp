//using Microsoft.AspNetCore.Mvc;
//using StudentApp.Data;
//using StudentApp.Dto;
//using StudentApp.Models;

//namespace StudentApp.Controllers
//{
//	[Route("api/[controller]")]
//	[ApiController]
//	public class StudentTeacherController : ControllerBase
//	{
//		private ApplicationDbContext _context;

//		public StudentTeacherController(ApplicationDbContext context)
//		{
//			_context = context;
//		}

//		[HttpPost]
//		public IActionResult result(StudentTeacherDto studentTeacher)
//		{
//			var stuId = from user in _context.Users
//						where user.RoleId == 1
//						select user.id;
//			var teacId = from user in _context.Users
//						 where user.RoleId == 2
//						 select user.id;

//			var student = _context.Users.Find(studentTeacher.StudentId);
//			var teacher = _context.Users.Find(studentTeacher.TeacherId);

//			var result1 = stuId.Contains(studentTeacher.StudentId);
//			var result2 = teacId.Contains(studentTeacher.TeacherId);
//			if (result1 && result2) {
//				StudentTeacher studentTeacher1 = new StudentTeacher();
//				studentTeacher1.StudentId = studentTeacher.StudentId;
//				studentTeacher1.TeacherId = studentTeacher.TeacherId;
//				studentTeacher1.student = student;
//				studentTeacher1.teacher = teacher;
//				_context.StudentTeacher.Add(studentTeacher1);
//				_context.SaveChanges();
//				return Ok(studentTeacher1);
//			}
//			else
//				return BadRequest();
//		}
//	}
//}
