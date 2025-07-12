using PluginBase;

[PluginLoad]
public class Plugin2 : IPlugin
{
    public void Execute() => Console.WriteLine("Plugin2 executed");
}
