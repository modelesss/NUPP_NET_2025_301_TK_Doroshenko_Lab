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
    }
}
