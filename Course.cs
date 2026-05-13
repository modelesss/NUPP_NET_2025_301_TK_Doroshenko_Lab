using System;

namespace University.Common.Models
{
    public class Course
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Credits { get; set; }
        public Teacher? Teacher { get; set; }

        public Course()
        {
            Id = Guid.NewGuid();
        }

        public void ShowInfo()
        {
            Console.WriteLine($"Курс: {Title} ({Credits} кредитів)");
            if (Teacher != null)
                Console.WriteLine($"Викладач: {Teacher.GetFullName()}");
            Console.WriteLine(new string('-', 50));
        }
    }
}
