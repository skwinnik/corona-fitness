using System;
using System.Collections.Generic;
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

            var generatorClasses = runtimeAssemblyNames
                .Select(Assembly.Load)
                .SelectMany(a => a.ExportedTypes)
                .Where(t => typeof(IxGenerator).IsAssignableFrom(t))
                .Where(t => t.IsClass);

            List<IxGenerator> generators = new List<IxGenerator>();
            foreach (var generatorClass in generatorClasses)
                generators.Add((IxGenerator)ActivatorUtilities.CreateInstance(provider, generatorClass));

            generators = generators.OrderBy(x => x.Priority).ToList();

            foreach (var generator in generators)
                await generator.Generate();
        }
    }
}