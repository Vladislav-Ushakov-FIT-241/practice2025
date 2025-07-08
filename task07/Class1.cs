using System;
using System.Text;
using System.Reflection;

namespace task07
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property)]
    public class DisplayNameAttribute(string name) : Attribute
    {
        public string DisplayName => name;
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class VersionAttribute(int major, int minor) : Attribute
    {
        public int Major => major;
        public int Minor => minor;
    }

    [Version(1, 0)]
    [DisplayName("Пример класса")]
    public class SampleClass
    {
        [DisplayName("Тестовый метод")]
        public void TestMethod() { }

        [DisplayName("Числовое свойство")]
        public string? Number { get; set; }
    }

    public static class ReflectionHelper
    {
        public static string PrintTypeInfo(Type type)
        {
            var output = new StringBuilder();
            var verAttr = type.GetCustomAttribute<VersionAttribute>();
            var nameAttr = type.GetCustomAttribute<DisplayNameAttribute>();

            //ИНФОРМАЦИЯ О КЛАССЕ
            output.AppendLine($"Имя класса: {nameAttr?.DisplayName}");
            output.AppendLine($"Версия класса: {verAttr?.Major}.{verAttr?.Minor}\n");

            //МЕТОДЫ КЛАССА
            output.AppendLine("Методы класса:");
            type.GetMethods().Select(m => m.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName).
                Where(name => name != null).Aggregate(output, (sb, name) => sb.AppendLine(name));

            //СВОЙСТВА КЛАССА
            output.AppendLine("Свойства класса:");
            type.GetProperties().Select(p => p.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName).
                Where(name => name != null).Aggregate(output, (sb, name) => sb.AppendLine(name));

            Console.Write(output.ToString());
            return output.ToString();
        }
    }
}
