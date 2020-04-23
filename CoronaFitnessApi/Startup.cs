using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AspNetCore.Identity.Mongo;
using CoronaFitness.Integration.OpenVidu;
using CoronaFitness.Integration.OpenVidu.Rest;
using CoronaFitness.Integration.OpenVidu.Settings;
using CoronaFitnessBL.Account;
using CoronaFitnessBL.Meeting;
using CoronaFitnessBL.User;
using CoronaFitnessBL.User.UserContext;
using CoronaFitnessDb;
using CoronaFitnessDb.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
            services.Configure<CxMongoDataSettings>(
                Configuration.GetSection("MongoSettings"));

            services.AddSingleton<IxMongoDataSettings>(sp =>
                sp.GetRequiredService<IOptions<CxMongoDataSettings>>().Value);

            services.AddSingleton<IxMongoDataContext, CxMongoDataContext>();
        }

        private void ConfigureMongoIdentity(IServiceCollection services)
        {
            var config = Configuration
                .GetSection("MongoIdentitySettings")
                .GetSection("FullConnectionString").Value;

            services.AddIdentityMongoDbProvider<CxIdentityUser, CxIdentityRole>(identityOptions =>
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

                options.Events.OnRedirectToAccessDenied = context =>
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return Task.CompletedTask;
                };
            });
        }

        //todo research CORS
        readonly string developmentOriginPolicy = "_developmentOriginPolicy";

        public void ConfigureServices(IServiceCollection services)
        {
            this.ConfigureMongoData(services);
            this.ConfigureMongoIdentity(services);

            services.AddControllers();
            
            services.AddScoped<IxUserBusinessOperations, CxUserBusinessOperations>();
            services.AddScoped<IxAccountBusinessOperations, CxAccountBusinessOperations>();
            services.AddScoped<IxMeetingBusinessOperations, CxMeetingBusinessOperations>();

            services.AddScoped<IxUserContext, CxUserContext>();
            
            services.Configure<CxOpenViduSettings>(
                Configuration.GetSection("OpenViduSettings"));

            services.AddSingleton<IxOpenViduSettings>(sp =>
                sp.GetRequiredService<IOptions<CxOpenViduSettings>>().Value);

            services.AddScoped<IxOpenViduGateway, CxOpenViduGateway>();

            services.AddSingleton<OpenViduRestClientBuilder, OpenViduRestClientBuilder>();
            
            services.AddHttpContextAccessor();

            services.AddCors(options =>
            {
                options.AddPolicy(developmentOriginPolicy,
                    builder =>
                    {
                        builder
                            .WithOrigins("https://localhost:44343")
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials();
                    });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(developmentOriginPolicy);
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}