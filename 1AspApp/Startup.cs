using _1AspApp.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _1AspApp
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MovieContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("MsSQLConnection")));
            //options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
            services.AddControllersWithViews(); //*****
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                DataSeeding.Seed(app); //DataSeedþng classýndaki Seed metodunu çaðýrýp test aþamasý database'ini oluþturdum.
            }
            app.UseStaticFiles(); //wwwroot klasörünü kullanýma açar. bunu yazmazsak fotolar görünmez

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                ////localhost:5000
                //endpoints.MapGet("/", async context =>
                //{
                //    await context.Response.WriteAsync("Hello World!");
                //});
                ////localhost:5000/movies
                //endpoints.MapGet("/movies", async context =>
                //{
                //    await context.Response.WriteAsync("Movie List");
                //});

                /*//localhost:5000
                endpoints.MapControllerRoute(
                    name: "home",
                    pattern: "",
                    defaults: new { controller = "Home", action = "Index" }
                    );

                //localhost:/5000/about
                endpoints.MapControllerRoute(
                    name: "about",
                    pattern: "hakkimizda",
                    defaults: new { controller = "Home", action = "About" }
                    );


                endpoints.MapControllerRoute(
                    name: "movieList",
                    pattern: "movies/list",
                    defaults: new { controller = "Movies", action = "List" }
                    );


                endpoints.MapControllerRoute(
                    name: "movieDetails",
                    pattern: "movies/details",
                    defaults: new { controller = "Movies", action = "Details" }
                    );*/

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                    );

            });
        }
    }
}
