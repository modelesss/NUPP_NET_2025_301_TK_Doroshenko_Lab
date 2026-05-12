using System.ComponentModel.DataAnnotations.Schema;

namespace University.Infrastructure.Models
{
    public class StudentEntity : PersonEntity
    {
        public string StudentId { get; set; } = string.Empty;
        public int Course { get; set; }
        public double GPA { get; set; }

        // Для Table-per-Type
        [NotMapped]
        public List<CourseEntity>? EnrolledCourses { get; set; }
    }
}
