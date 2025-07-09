using CommandLib;

namespace FileSystemCommands
{
    public class DirectorySizeCommand(string path) : ICommand
    {
        public void Execute()
        {
            if (!Directory.Exists(path))
                throw new DirectoryNotFoundException("Directory not found");

            long size = Directory
                .EnumerateFiles(path, "*", SearchOption.AllDirectories).Sum(f => new FileInfo(f).Length);

            Console.WriteLine($"Size of directory: {size} bytes");
        }
    }

    public class FindFilesCommand(string path, string mask) : ICommand
    {
        public void Execute()
        {
            if (!Directory.Exists(path))
                throw new DirectoryNotFoundException("Directory not found");

            string[] files = Directory.GetFiles(path, mask);
            
            string result = files.Any()
                ? $"Matched files ({mask}):\n{string.Join("\n", files.Select(Path.GetFileName))}"
                : $"No files found matching: {mask}";

            Console.WriteLine(result);
        }
    }
}
