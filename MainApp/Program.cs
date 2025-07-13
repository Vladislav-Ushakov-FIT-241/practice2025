using System.Text.Json.Serialization;
using System.Text.Json;
using task13;

namespace task13App
{
    public class Program
    {
        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            WriteIndented = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            Converters = { new CustomDateTimeConverter() }
        };

        public static void Main()
        {
            var student = new Student
            {
                FirstName = "Владислав",
                LastName = "Ушаков",
                BirthDate = new DateTime(2006, 10, 19),
                Grades = new List<Subject>
            {
                new Subject { Name = "Математика", Grade = 5 },
                new Subject { Name = "Программирование", Grade = 5 }
            }
            };

            string json = JsonSerializer.Serialize(student, _jsonOptions);
            Console.WriteLine("Сериализованный JSON:");
            Console.WriteLine(json);

            string filePath = "student.json";
            File.WriteAllText(filePath, json);
            Console.WriteLine($"\nФайл сохранён: {filePath}");

            string jsonFromFile = File.ReadAllText(filePath);
            Student deserializedStudent = JsonSerializer.Deserialize<Student>(jsonFromFile, _jsonOptions);

            Console.WriteLine("\nДесериализованные данные:");
            Console.WriteLine($"ФИО: {deserializedStudent.LastName} {deserializedStudent.FirstName}");
            Console.WriteLine($"Дата рождения: {deserializedStudent.BirthDate:dd.MM.yyyy}");
            Console.WriteLine($"Оценки: {deserializedStudent.Grades[0].Name} - {deserializedStudent.Grades[0].Grade}, " +
                            $"{deserializedStudent.Grades[1].Name} - {deserializedStudent.Grades[1].Grade}");
        }
    }
}
