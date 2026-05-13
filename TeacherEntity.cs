namespace University.Infrastructure.Models
{
    public class TeacherEntity : PersonEntity
    {
        public string Department { get; set; } = string.Empty;
        public decimal Salary { get; set; }
        public List<string> Subjects { get; set; } = new();
    }
}
