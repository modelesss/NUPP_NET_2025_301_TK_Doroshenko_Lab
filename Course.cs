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

        // === Статичний метод CreateNew() для лабораторної №2 ===
        public static Course CreateNew(Teacher? teacher = null)
        {
            var random = new Random();
            string[] titles =
            {
                "C# Програмування",
                "Алгоритми та структури даних",
                "Бази даних",
                "Машинне навчання",
                "Веб-розробка",
                "Операційні системи",
                "Математичний аналіз",
                "Англійська мова"
            };

            string[] descriptions =
            {
                "Вивчення сучасних технологій розробки",
                "Фундаментальний курс з алгоритмів",
                "Проектування та адміністрування баз даних",
                "Основи штучного інтелекту та нейронних мереж"
            };

            return new Course
            {
                Title = titles[random.Next(titles.Length)],
                Description = descriptions[random.Next(descriptions.Length)],
                Credits = random.Next(3, 8),
                Teacher = teacher ?? Teacher.CreateNew() // якщо викладач не переданий — створюємо нового
            };
        }
    }
}
