using Microsoft.AspNetCore.Mvc;
using University.Common.Models;
using University.Common.Services;
using University.Infrastructure.Models;
using University.Infrastructure.Services;
using University.REST.Models;

namespace University.REST.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TeachersController : ControllerBase
{
    private readonly ICrudServiceAsync<TeacherEntity> _service;

    public TeachersController(ICrudServiceAsync<TeacherEntity> service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TeacherDto>>> GetAll()
    {
        var teachers = await _service.ReadAllAsync();
        var dtos = teachers.Select(t => new TeacherDto
        {
            Id = t.Id,
            FirstName = t.FirstName,
            LastName = t.LastName,
            Department = t.Department,
            Salary = t.Salary
        });

        return Ok(dtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TeacherDto>> GetById(Guid id)
    {
        var teacher = await _service.ReadAsync(id);
        if (teacher == null) return NotFound();

        var dto = new TeacherDto
        {
            Id = teacher.Id,
            FirstName = teacher.FirstName,
            LastName = teacher.LastName,
            Department = teacher.Department,
            Salary = teacher.Salary
        };

        return Ok(dto);
    }

    [HttpPost]
    public async Task<ActionResult<TeacherDto>> Create(Teacher teacher)
    {
        var entity = new TeacherEntity
        {
            Id = teacher.Id,
            FirstName = teacher.FirstName,
            LastName = teacher.LastName,
            Department = teacher.Department,
            Salary = teacher.Salary,
            DateOfBirth = teacher.DateOfBirth
        };

        await _service.CreateAsync(entity);

        var dto = new TeacherDto
        {
            Id = entity.Id,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            Department = entity.Department,
            Salary = entity.Salary
        };

        return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var teacher = await _service.ReadAsync(id);
        if (teacher == null) return NotFound();

        await _service.RemoveAsync(teacher);
        return NoContent();
    }
}
