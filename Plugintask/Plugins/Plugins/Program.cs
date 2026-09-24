using System.Reflection;
using PluginDLLS;
namespace Plugins;

public class Program
{
    public static void Main(string[] args)
    {
        string projectRoot = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", ".."));
        string pluginFolder = Path.Combine(projectRoot, "PluginFiles");
        if (!Directory.Exists(pluginFolder))
        {
            Console.WriteLine("Plugin folder not found.");
            return;
        }

        string[] dllFiles = Directory.GetFiles(pluginFolder, "*.dll");

        foreach (string dllPath in dllFiles)
        {
            try
            {
                Console.WriteLine($"\nLoading: {Path.GetFileName(dllPath)}");
                Assembly assembly = Assembly.LoadFrom(dllPath);
                Type[] types = assembly.GetTypes();

                foreach (Type type in types)
                {
                    if (typeof(IPlugin).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
                    {
                        IPlugin plugin = (IPlugin)Activator.CreateInstance(type)!;
                        Console.WriteLine($"Plugin: {plugin.Name}");
                        plugin.Execute();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading " + $"{Path.GetFileName(dllPath)}: {ex.Message}");
            }
        }

        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
    }
}