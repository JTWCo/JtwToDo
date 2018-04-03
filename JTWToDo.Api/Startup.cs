using System;
using JTWToDo.Data;
using JTWToDo.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JTWToDo.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //TODO: use more secure configuration
            services.AddEntityFrameworkSqlServer().AddDbContext<ToDoContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))); 
            services.AddMvc();
            services.AddScoped(_repoFactory);
        }

        private readonly Func<IServiceProvider, ITodoDataContext> _repoFactory = x =>
        {
            var context = x.GetService<ITodoDataContext>();

            return context;

        };

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=ToDo}/{action=Get}/{id?}");
            });
        }
    }
}
