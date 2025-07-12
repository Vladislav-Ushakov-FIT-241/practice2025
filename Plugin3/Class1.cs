using PluginBase;

[PluginLoad("Plugin2")]
public class Plugin3 : IPlugin
{
    public void Execute() => Console.WriteLine("Plugin3 executed");
}
