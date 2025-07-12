using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using PluginBase;

class Program
{
    static void Main()
    {
        var pluginsDir = Path.Combine(Directory.GetCurrentDirectory(), "Plugins");

        if (!Directory.Exists(pluginsDir))
        {
            Directory.CreateDirectory(pluginsDir);
            Console.WriteLine($"Создана папка плагинов: {pluginsDir}");
        }

        var pluginTypes = Directory.GetFiles(pluginsDir, "*.dll")
            .Select(Assembly.LoadFrom)
            .SelectMany(a => a.GetTypes())
            .Where(t => t.GetCustomAttribute<PluginLoadAttribute>() != null
                      && typeof(IPlugin).IsAssignableFrom(t))
            .ToList();

        var sortedPlugins = TopologicalSort(pluginTypes);

        sortedPlugins.ForEach(t =>
        {
            var plugin = (IPlugin)Activator.CreateInstance(t)!;
            Console.WriteLine($"{t.Name} is running...");
            plugin.Execute();
        });
    }

    private static List<Type> TopologicalSort(List<Type> types)
    {
        var typeDict = types.ToDictionary(t => t.Name);

        var graph = types.ToDictionary(
            t => t,
            t => t.GetCustomAttribute<PluginLoadAttribute>()?
                .Dependencies?
                .Where(name => typeDict.ContainsKey(name))
                .Select(name => typeDict[name])
                .ToList() ?? new List<Type>()
        );

        var visited = new HashSet<Type>();
        var result = new List<Type>();

        graph.Keys.ToList().ForEach(node =>
        {
            if (!visited.Contains(node))
                Visit(node, graph, visited, result);
        });

        return result;
    }

    private static void Visit(Type node, Dictionary<Type, List<Type>> graph,
        HashSet<Type> visited, List<Type> result)
    {
        visited.Add(node);

        graph[node]?.ForEach(dep =>
        {
            if (!visited.Contains(dep))
                Visit(dep, graph, visited, result);
        });

        result.Add(node);
    }
}
