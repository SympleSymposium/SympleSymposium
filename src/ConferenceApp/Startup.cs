using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ConferenceApp.Models;
using ConferenceApp.Services;
using Newtonsoft.Json.Serialization;
using ConferenceApp.Infrastructure;
using ConferenceApp.Controllers;

namespace ConferenceApp
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddEntityFramework()
                .AddSqlServer()
                .AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(Configuration["Data:DefaultConnection:ConnectionString"]));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc();

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();

            //add Repositories
            services.AddScoped<ConferenceRepository>();
            services.AddScoped<PresentationRepository>();
            services.AddScoped<RoomRepository>();
            services.AddScoped<SlotRepository>();
            services.AddScoped<SpeakerRepository>();
            services.AddScoped<AddressRepository>();
            services.AddScoped<UserRepository>();

            //add Services
            services.AddScoped<ConferenceService>();
            services.AddScoped<PresentationService>();
            services.AddScoped<RoomService>();
            services.AddScoped<SlotService>();
            services.AddScoped<SpeakerService>();

            //add Controllers
            services.AddScoped<ConferencesController>();
            services.AddScoped<PresentationsController>();
            services.AddScoped<RoomsController>();
            services.AddScoped<SlotsController>();
            services.AddScoped<SpeakersController>();

            // convert Pascal to Camel
            services.AddMvc().AddJsonOptions(options => {
                options.SerializerSettings.ContractResolver =
                    new CamelCasePropertyNamesContractResolver();
            });


            // add security policies
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy => policy.RequireClaim("IsAdmin"));
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");

                // For more details on creating database during deployment see http://go.microsoft.com/fwlink/?LinkID=615859
                try
                {
                    using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
                        .CreateScope())
                    {
                        serviceScope.ServiceProvider.GetService<ApplicationDbContext>()
                             .Database.Migrate();
                    }
                }
                catch { }
            }

            app.UseIISPlatformHandler(options => options.AuthenticationDescriptions.Clear());

            app.UseStaticFiles();

            app.UseIdentity();

            // To configure external authentication please see http://go.microsoft.com/fwlink/?LinkID=532715

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                     name: "default",
                     template: "{*path}",
                     defaults: new { controller = "Home", action = "Index" }
                );
            });


            // initialize sample data
            SampleData.Initialize(app.ApplicationServices).Wait();

        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
