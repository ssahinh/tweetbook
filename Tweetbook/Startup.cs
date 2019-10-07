using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Tweetbook.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Tweetbook.Installers;
using Tweetbook.Services;
using SwaggerOptions = Tweetbook.Options.SwaggerOptions;

namespace Tweetbook
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
           services.InstallServicesInAssembly(Configuration);
           /*services.AddDbContext<DataContext>(options => {
               options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
           });
           services.AddDefaultIdentity<IdentityUser>()
               .AddDefaultUI(UIFramework.Bootstrap4)
               .AddEntityFrameworkStores<DataContext>();

           services.AddScoped<IPostService, PostService>();*/
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            var swaggerOptions = new SwaggerOptions();
            Configuration.GetSection(nameof(swaggerOptions)).Bind(swaggerOptions);
    
            app.UseSwagger(option =>
            {
                option.RouteTemplate = swaggerOptions.JsonRoute;
            });
            app.UseSwaggerUI(option =>
            {
                option.SwaggerEndpoint(swaggerOptions.UIEndpoint, swaggerOptions.Description);
            }); 
            
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthentication();
            
            // Swagger Run
            var optionSwagger = new RewriteOptions();
            optionSwagger.AddRedirect("^$", "swagger");
            app.UseRewriter(optionSwagger);

            app.UseMvc();
        }
    }
}
