using System;
using System.Collections.Generic;

namespace University.Common.Models
{
    public class Student : Person
    {
        public string StudentId { get; set; } = string.Empty;
        public int Course { get; set; }
        public double GPA { get; set; }
        public List<Course> EnrolledCourses { get; set; } = new();

        public Student() : base() { }

        public override string GetFullName()
        {
            return $"Студент: {base.GetFullName()} (Курс {Course})";
        }

        public void AddCourse(Course course)
        {
            EnrolledCourses.Add(course);
            Console.WriteLine($"Студент {GetFullName()} записався на {course.Title}");
        }

        // === Статичний метод CreateNew() для лабораторної №2 ===
        public static Student CreateNew()
        {
            var random = new Random();
            string[] firstNames = { "Петро", "Анна", "Іван", "Марія", "Олег", "Софія", "Дмитро", "Катерина" };
            string[] lastNames = { "Мельник", "Бондар", "Шевченко", "Коваленко", "Ткачук", "Кравченко", "Сидоренко" };

            return new Student
            {
                FirstName = firstNames[random.Next(firstNames.Length)],
                LastName = lastNames[random.Next(lastNames.Length)],
                Course = random.Next(1, 7),
                GPA = Math.Round(3.0 + random.NextDouble() * 2.0, 2), // від 3.0 до 5.0
                StudentId = $"STU-{DateTime.Now.Year}-{random.Next(10000, 99999)}"
            };
        }
    }
}
