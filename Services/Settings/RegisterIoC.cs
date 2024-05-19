using Microsoft.Extensions.DependencyInjection;
using Services.Services;
using System.Collections.Immutable;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Services.Settings
{
    public static class RegisterIoC
    {
        public static readonly ImmutableHashSet<string> ASSEMBLIES = ImmutableHashSet.Create("Services");

        public static string FilePathSlash => RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "\\" : "/";

        public static string RootFolder => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + FilePathSlash;


        public static void RegisterMediator(this IServiceCollection services)
        {
            Console.WriteLine("Starting Register IoC.");

            var scanningAssemblies = new HashSet<Assembly>();

            foreach (string dllFile in Directory.GetFiles(RootFolder, "*.dll"))
            {
                var assembly = System.Runtime.Loader.AssemblyLoadContext.Default.LoadFromAssemblyPath(dllFile);

                if (!ASSEMBLIES.Contains(assembly.GetName().Name.Split(".")[0]))
                {
                    continue;
                }

                scanningAssemblies.Add(assembly);
            }

            foreach (var assembly in scanningAssemblies)
            {
                foreach (var type in assembly.ExportedTypes.Select(t => t.GetTypeInfo()).Where(t => t.IsClass && !t.IsAbstract))
                {
                    var interfaces = type.ImplementedInterfaces.Select(i => i.GetTypeInfo()).ToArray();

                    if (interfaces.Length == 0)
                    {
                        continue;
                    }

                    if (interfaces.Contains(typeof(IService)))
                    {
                        services.AddTransient(interfaces.First(), type.AsType());
                    }
                    else if (interfaces.Contains(typeof(ISingletonService)))
                    {
                        services.AddSingleton(interfaces.First(), type.AsType());
                    }
                }
            }

            Console.WriteLine("Register IoC done.");
        }
    }
}