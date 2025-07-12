using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace task11
{
    public interface ICalculator
    {
        int Add(int a, int b);
        int Minus(int a, int b);
        int Mul(int a, int b);
        int Div(int a, int b);
    }

    public static class CalculatorGenerator
    {
        private static int _counter = 0;

        public static dynamic CreateCalculator()
        {
            _counter++;
            string assemblyName = $"DynamicAssembly_{_counter}";

            string code = @"
                using task11;
                
                public class Calculator : ICalculator
                {
                    public int Add(int a, int b) => a + b;
                    public int Minus(int a, int b) => a - b;
                    public int Mul(int a, int b) => a * b;
                    public int Div(int a, int b) => a / b;
                }
            ";

            var compilation = CSharpCompilation.Create(assemblyName)
                .WithOptions(new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary))
                .AddReferences(
                    MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                    MetadataReference.CreateFromFile(typeof(ICalculator).Assembly.Location)
                )
                .AddSyntaxTrees(CSharpSyntaxTree.ParseText(code));

            using var ms = new System.IO.MemoryStream();
            var result = compilation.Emit(ms);

            ms.Seek(0, System.IO.SeekOrigin.Begin);
            var assembly = System.Runtime.Loader.AssemblyLoadContext.Default.LoadFromStream(ms);
            var type = assembly.GetType("Calculator");
            return Activator.CreateInstance(type);
        }
    }
}
