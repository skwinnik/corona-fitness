using AspNetCore.Identity.Mongo;
using CoronaFitnessBL.User;
using CoronaFitnessDb;
using CoronaFitnessDb.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace CoronaFitnessApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private void ConfigureMongoData(IServiceCollection services)
        {
            services.Configure<FxMongoDataSettings>(
                Configuration.GetSection("MongoSettings"));

            services.AddSingleton<IxMongoDataSettings>(sp =>
                sp.GetRequiredService<IOptions<FxMongoDataSettings>>().Value);

            services.AddSingleton<IxMongoDataContext, FxMongoDataContext>();
        }

        private void ConfigureMongoIdentity(IServiceCollection services)
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
        }

        public void ConfigureServices(IServiceCollection services)
        {
            this.ConfigureMongoData(services);
            this.ConfigureMongoIdentity(services);

            services.AddControllers();
            services.AddSingleton<IxUserBusinessOperations, FxUserBusinessOperations>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}