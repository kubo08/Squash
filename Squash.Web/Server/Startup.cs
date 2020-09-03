using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using Squash.Data;
using Squash.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Squash.Data.Interfaces;
using Squash.Web.Shared.Services;
using Squash.Data.Interfaces.Repositories;
using Squash.Domain;
using Squash.Data.Interfaces.Services;

namespace Squash.Web.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<SquashContext>(options => options.UseSqlServer(Configuration["Database:ConnectionString"]));

            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IPlayerRepository, PlayerRepository>();
            services.AddTransient<IPointsRepository, PointsRepository>();

            services.AddTransient<IPlayerService, PlayerService>();
            services.AddTransient<IPointsService, PointsService>();

            services.AddControllersWithViews();
            services.AddRazorPages();
            
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                    policy.WithOrigins(new[] { "https://localhost:5003", "https://localhost:5001" })
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseCors();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}
