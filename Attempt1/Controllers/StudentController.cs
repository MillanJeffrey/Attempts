using Attempt1.Data;
using Attempt1.Models;
using Attempt1.Common;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using Attempt1.DTOs;
using Dapper;

namespace Attempt1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {


        //private static List<Student> _students = new List<Student>
        //    {
        //        new Student
        //        {
        //            Id = 1,
        //            StudentId = "S001",
        //            FirstName = "John",
        //            LastName = "Doe",
        //            Course = "Computer Science",
        //            DateCreated = DateTime.Now
        //        },
        //        new Student
        //        {
        //            Id = 2,
        //            StudentId = "S002",
        //            FirstName = "Jane",
        //            LastName = "Smith",
        //            Course = "Mathematics",
        //            DateCreated = DateTime.Now
        //        },
        //        new Student
        //        {
        //            Id = 3,
        //            StudentId = "S003",
        //            FirstName = "Michael",
        //            LastName = "Johnson",
        //            Course = "Physics",
        //            DateCreated = DateTime.Now
        //        }
        //    };

        //[HttpGet("all")]
        //public IActionResult GetAll()
        //{
        //    return Ok(_students);//BadRequest(students);
        //    //return NotFound(students);
        //}

        //[HttpGet("{id}")]
        //public IActionResult GetById(int id)
        //{
        //    var student = _students.FirstOrDefault(s => s.Id == id);

        //    return Ok(_students);
        //}

        //[HttpPost]
        //public IActionResult Create([FromBody] Student student)
        //{
        //    if (student == null)
        //    {
        //        return BadRequest("Student data is needed.");
        //    }
        //    // Generate a new Id for the student
        //    student.Id = _students.Count + 1; 
        //    _students.Add(student);

        //    return CreatedAtAction(nameof(GetById), new { id = student.Id }, student);

        //}

        //[HttpPut("{id}")]  
        //public IActionResult Update(int id, [FromBody] Student updatedStudent)
        //{
        //    var student = _students.FirstOrDefault(s => s.Id == id);

        //    if (student == null)
        //    {
        //        return NotFound();
        //    }

        //    student.FirstName = updatedStudent.FirstName;
        //    student.LastName = updatedStudent.LastName;
        //    student.Course = updatedStudent.Course;

        //    return Ok(student);
        //}

        //[HttpDelete("{id}")]
        //public IActionResult Delete(int id)
        //{
        //    var student = _students.FirstOrDefault(s => s.Id == id);

        //    if (student == null)
        //    {
        //        return NotFound();
        //    }
        //    _students.Remove(student);
        //    return NoContent();
        //}
        //[HttpPatch]
        //public IActionResult Patch(int id, [FromBody] Student updatedStudent)
        //{
        //    var student = _students.FirstOrDefault(s => s.Id == id);
        //    if (student == null)
        //    {
        //        return NotFound();
        //    }
        //    if (!string.IsNullOrEmpty(updatedStudent.FirstName))
        //    {
        //        student.FirstName = updatedStudent.FirstName;
        //    }
        //    if (!string.IsNullOrEmpty(updatedStudent.LastName))
        //    {
        //        student.LastName = updatedStudent.LastName;
        //    }
        //    if (!string.IsNullOrEmpty(updatedStudent.Course))
        //    {
        //        student.Course = updatedStudent.Course;
        //    }
        //    return Ok(student);
        //}

        private readonly DapperContext _db;

        public StudentController(DapperContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var sql = "SELECT * FROM [Attempts].[dbo].[Student]";

            using var connection = _db.CreateConnection();
            var students = connection.Query<StudentResponsesDTO>(sql)
                                     .ToList();

            return Ok(new ApiResponse<IEnumerable<StudentResponsesDTO>>
            {
                Success = true,
                Message = "Students retrieved successfully.",
                Data = students
            });
        }

        [HttpGet("{id}")]

        public IActionResult GetById(int id)
        {
            var sql = "SELECT * FROM [Attempts].[dbo].[Student]" +
                " WHERE Id = @Id";

            using var connection = _db.CreateConnection();
            var student = connection.QueryFirstOrDefault<StudentResponsesDTO>(sql, new { Id = id });

            if (student == null)
            {
                return NotFound($"Student with ID {id} not found.");
            }
            return Ok(student);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateStudentDTO dto)
        {
            var sql = @"INSERT INTO Student (StudentId,FirstName,LastName,Course,Age)
                        OUTPUT INSERTED.Id
                        VALUES (@StudentId, @FirstName, @LastName, @Course, @Age)";

            using var connection = _db.CreateConnection();

            // ExecuteScalar returns the single value from OUTPUT INSERTED.Id — the new ID
            var newId = connection.ExecuteScalar<string>(sql, new
            {
                dto.StudentId,
                dto.FirstName,
                dto.LastName,
                dto.Course,
                dto.Age
            });

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Student created successfully.",
                Data = dto
            });
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateStudentDTO dto)
        {
            using var connection = _db.CreateConnection();

            // Check if the product exists first
            var exists = connection.QueryFirstOrDefault<int?>(
                "SELECT Id FROM Student WHERE Id = @Id", new { Id = id }
            );

            if (exists == null)
                return NotFound();

            var sql = @"UPDATE Student
                    SET FirstName = @FirstName,
                        LastName = @LastName,
                        Course = @Course
                    WHERE Id = @Id";

            connection.Execute(sql, new
            {
                Id = id,
                dto.FirstName,
                dto.LastName,
                dto.Course
            });

            var updated = connection.QueryFirstOrDefault<StudentResponsesDTO>(
                "SELECT * FROM Student WHERE Id = @Id",
                new { Id = id }
            );

            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            using var connection = _db.CreateConnection();

            var rowsAffected = connection.Execute(
                "DELETE FROM Student WHERE Id = @Id", new { Id = id }
            );

            // If zero rows were affected, the ID didn't exist
            if (rowsAffected == 0)
                return NotFound();

            return NoContent();   // 204 — success, nothing to return
        }
        //[HttpGet]
        //public IActionResult GetAll(int id)
        //{
        //    try
        //    {
        //        var sql = "SELECT * FROM [Attempts].[dbo].[Student] WHERE Id = @Id";
        //        using var connection = _db.CreateConnection();
        //        var student = connection.QueryFirstOrDefault<StudentResponsesDTO>(sql, new { Id = id });

        //        if (student == null)
        //        {
        //            return NotFound($"Student with ID {id} not found.");
        //        }

        //        return Ok(student);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception (you can use a logging framework here)
        //        Console.WriteLine($"Error occurred: {ex.Message}");
        //        return StatusCode(500, "An error occurred while processing your request.");
        //    }
        //}

    }
}
