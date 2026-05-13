using Microsoft.AspNetCore.Mvc;
using University.Common.Models;
using University.Common.Services;
using University.Infrastructure.Models;
using University.Infrastructure.Services;
using University.REST.Models;

namespace University.REST.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentsController : ControllerBase
{
    private readonly ICrudServiceAsync<StudentEntity> _service;

    public StudentsController(ICrudServiceAsync<StudentEntity> service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<StudentDto>>> GetAll()
    {
        var students = await _service.ReadAllAsync();
        var dtos = students.Select(s => new StudentDto
        {
            Id = s.Id,
            FirstName = s.FirstName,
            LastName = s.LastName,
            Course = s.Course,
            GPA = s.GPA,
            StudentId = s.StudentId
        });

        return Ok(dtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<StudentDto>> GetById(Guid id)
    {
        var student = await _service.ReadAsync(id);
        if (student == null) return NotFound();

        var dto = new StudentDto
        {
            Id = student.Id,
            FirstName = student.FirstName,
            LastName = student.LastName,
            Course = student.Course,
            GPA = student.GPA,
            StudentId = student.StudentId
        };

        return Ok(dto);
    }

    [HttpPost]
    public async Task<ActionResult<StudentDto>> Create(Student student)
    {
        var entity = new StudentEntity
        {
            Id = student.Id,
            FirstName = student.FirstName,
            LastName = student.LastName,
            DateOfBirth = student.DateOfBirth,
            Course = student.Course,
            GPA = student.GPA,
            StudentId = student.StudentId
        };

        await _service.CreateAsync(entity);

        var dto = new StudentDto
        {
            Id = entity.Id,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            Course = entity.Course,
            GPA = entity.GPA,
            StudentId = entity.StudentId
        };

        return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var student = await _service.ReadAsync(id);
        if (student == null) return NotFound();

        await _service.RemoveAsync(student);
        return NoContent();
    }
}
