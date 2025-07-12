using System;

namespace PluginBase
{
    [AttributeUsage(AttributeTargets.Class)]
    public class PluginLoadAttribute : Attribute
    {
        public string[] Dependencies { get; }
        public PluginLoadAttribute(params string[] dependencies) => Dependencies = dependencies;
    }

    public interface IPlugin
    {
        void Execute();
    }
}
