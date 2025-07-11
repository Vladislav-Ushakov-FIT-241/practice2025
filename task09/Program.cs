using System;
using System.Reflection;

namespace task09
{
    class Program
    {
        static void Main(string[] args)
        {
            Assembly assembly = Assembly.LoadFrom(args[0]);

            Array.ForEach(assembly.GetTypes(), type =>
            {
                Console.WriteLine($"Класс: {type.Name}");

                Array.ForEach((Attribute[])type.GetCustomAttributes(),
                    attr => Console.WriteLine($"Атрибут: {attr.GetType().Name}"));

                Array.ForEach(type.GetConstructors(), constructor =>
                {
                    Console.WriteLine($"Конструктор: {constructor.Name}");
                    Array.ForEach(constructor.GetParameters(),
                        param => Console.WriteLine($"Параметр: {param.ParameterType.Name} {param.Name}"));
                });

                Array.ForEach(type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly), method =>
                {
                    Console.WriteLine($"Метод: {method.Name}");
                    Array.ForEach(method.GetParameters(),
                        param => Console.WriteLine($"Параметр: {param.ParameterType.Name} {param.Name}"));
                });
            });
        }
    }
}
