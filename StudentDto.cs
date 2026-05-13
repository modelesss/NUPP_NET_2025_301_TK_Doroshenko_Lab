namespace University.REST.Models;

public class StudentDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public int Course { get; set; }
    public double GPA { get; set; }
    public string StudentId { get; set; } = string.Empty;
}
