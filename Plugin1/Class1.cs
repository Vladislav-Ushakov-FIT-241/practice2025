using PluginBase;

[PluginLoad("Plugin2", "Plugin3")]
public class Plugin1 : IPlugin
{
    public void Execute() => Console.WriteLine("Plugin1 executed");
}
