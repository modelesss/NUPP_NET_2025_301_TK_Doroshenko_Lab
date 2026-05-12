using System;
using University.Common.Models;

namespace University.Common.Extensions
{
    // Метод розширення
    public static class Extensions
    {
        // Метод розширення для класу Person
        public static int GetAge(this Person person)
        {
            var today = DateTime.Today;
            int age = today.Year - person.DateOfBirth.Year;

            if (person.DateOfBirth.Date > today.AddYears(-age))
                age--;

            return age;
        }

        // Метод розширення для Course
        public static bool IsAdvancedCourse(this Course course)
        {
            return course.Credits >= 5;
        }

        // Метод розширення для списку студентів
        public static void PrintAll(this List<Student> students)
        {
            Console.WriteLine($"=== Список студентів ({students.Count} шт.) ===");
            foreach (var student in students)
            {
                Console.WriteLine($"- {student.GetFullName()}, Вік: {student.GetAge()}, GPA: {student.GPA}");
            }
            Console.WriteLine();
        }
    }
}
