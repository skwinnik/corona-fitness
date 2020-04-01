using System;
using System.IO;
using System.Threading.Tasks;
using AspNetCore.Identity.Mongo;
using CoronaFitness.Integration.OpenVidu;
using CoronaFitnessBL.Account;
using CoronaFitnessBL.Meeting;
using CoronaFitnessBL.User;
using CoronaFitnessDb;
using CoronaFitnessDb.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Serilog;

namespace DbGenerator
{
    class Program
    {
        public static IConfigurationRoot Configuration;
        
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console(Serilog.Events.LogEventLevel.Debug)
                .MinimumLevel.Debug()
                .Enrich.FromLogContext()
                .CreateLogger();
            
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            var serviceProvider = serviceCollection.BuildServiceProvider();
            
            try
            {
                serviceProvider.GetService<Application>().Run(serviceProvider).Wait();
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
        }
        
        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", false)
                .Build();
            
            ConfigureMongoData(serviceCollection);
            ConfigureMongoIdentity(serviceCollection);

            serviceCollection.AddTransient<Application>();
            serviceCollection.AddScoped<IxUserBusinessOperations, FxUserBusinessOperations>();
            serviceCollection.AddScoped<IxAccountBusinessOperations, FxAccountBusinessOperations>();
            serviceCollection.AddScoped<IxMeetingBusinessOperations, FxMeetingBusinessOperations>();
            serviceCollection.AddScoped<IxOpenViduGateway, FxFakeOpenViduGateway>();
            
            serviceCollection.AddSingleton(LoggerFactory.Create(builder =>
            {
                builder
                    .AddSerilog(dispose: true);
            }));
            serviceCollection.AddLogging();
        }
        
        private static void ConfigureMongoData(IServiceCollection services)
        {
            services.Configure<FxMongoDataSettings>(
                Configuration.GetSection("MongoSettings"));

            services.AddSingleton<IxMongoDataSettings>(sp =>
                sp.GetRequiredService<IOptions<FxMongoDataSettings>>().Value);

            services.AddSingleton<IxMongoDataContext, FxMongoDataContext>();
        }

        private static void ConfigureMongoIdentity(IServiceCollection services)
        {
            var config = Configuration
                .GetSection("MongoIdentitySettings")
                .GetSection("FullConnectionString").Value;

            services.AddIdentityMongoDbProvider<FxIdentityUser, FxIdentityRole>(identityOptions =>
                {
                    identityOptions.Password.RequiredLength = 6;
                    identityOptions.Password.RequireLowercase = false;
                    identityOptions.Password.RequireUppercase = false;
                    identityOptions.Password.RequireNonAlphanumeric = false;
                    identityOptions.Password.RequireDigit = false;
                },
                mongoIdentityOptions => { mongoIdentityOptions.ConnectionString = config; });

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "fx_corona_auth";
                
                options.Events.OnRedirectToLogin = context =>
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return Task.CompletedTask;
                };
            });
        }
    }
}