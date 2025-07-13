using System;
using System.Collections.Generic;
using System.Text.Json;
using Xunit;
using task13;

public class StudentTests
{
    [Fact]
    public void Serialize_Deserialize_Works()
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

        string json = JsonSerializer.Serialize(student);
        Student result = JsonSerializer.Deserialize<Student>(json);

        Assert.Equal("19.10.2006", result.BirthDate.ToString("dd.MM.yyyy"));
    }

    [Fact]
    public void File_Operations_Work()
    {
        string testFile = "test.json";
        File.WriteAllText(testFile, "test");
        string content = File.ReadAllText(testFile);
        File.Delete(testFile);

        Assert.Equal("test", content);
    }
}
