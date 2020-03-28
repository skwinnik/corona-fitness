using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using DbGenerator.Generators;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;

namespace DbGenerator
{
    public class Application
    {
        public Application()
        {
        }

        public async Task Run(ServiceProvider provider)
        {
            var platform = Environment.OSVersion.Platform.ToString();
            var runtimeAssemblyNames = DependencyContext.Default.GetRuntimeAssemblyNames(platform);

            var generators = runtimeAssemblyNames
                .Select(Assembly.Load)
                .SelectMany(a => a.ExportedTypes)
                .Where(t => typeof(IxGenerator).IsAssignableFrom(t))
                .Where(t => t.IsClass);

            foreach (var generatorType in generators)
            {
                var generator = (IxGenerator) ActivatorUtilities.CreateInstance(provider, generatorType);
                await generator.Generate();
            }
        }
    }
}