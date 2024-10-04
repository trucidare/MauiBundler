using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using MauiBundler.Abstractions.Interfaces;
using MauiBundler.Abstractions.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MauiBundler.Abstractions.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddMauiBundlerInternal(this IServiceCollection services, IConfiguration configuration, Assembly asm)
    {
        try
        {
            services.AddSingleton<IPluginService>(a => new PluginService
            {
                AnchorAssembly = asm
            });

            List<Type> types = new();
            var refs = asm.GetReferencedAssemblies()
                .Where(s => s.Name!.Contains("Plugin", StringComparison.InvariantCultureIgnoreCase))
                .ToList();

            var refFiles = Directory.GetFiles(Path.GetDirectoryName(asm.Location) ?? ".")
                .Where(s => s.Contains("Plugin", StringComparison.InvariantCultureIgnoreCase)
                            && s.Contains("dll", StringComparison.InvariantCultureIgnoreCase));

            foreach (var r in refFiles)
                types.AddRange(Assembly.LoadFrom(r).GetTypes());

            foreach (var r in refs)
                types.AddRange(Assembly.Load(r).GetTypes());

            var query = from type in types
                where type.IsSealed && !type.IsGenericType && !type.IsNested
                from method in type.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
                where method.IsDefined(typeof(ExtensionAttribute), false)
                where method.GetParameters()[0].ParameterType == typeof(IServiceCollection)
                select method;

            foreach (var type in query)
                if (type.GetParameters().Length == 1)
                    type?.Invoke(null, new object[] { services });
                else
                    type?.Invoke(null, new object[] { services, configuration });

        }
        catch (Exception e)
        {
            Debug.WriteLine(
                $"MauiBundler::AddExternalModules -> Error adding external modules {e.Message} {Environment.NewLine} {e.StackTrace}");
        }
    }
}