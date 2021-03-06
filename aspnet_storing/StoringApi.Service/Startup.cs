using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Okta.AspNetCore;
using StoringApi.Service.Repository;

namespace StoringApi.Service
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "StoringApi.Service", Version = "v1" });
            });

            services.AddDbContext<VWFContext>(options =>
            {
                // options.UseSqlServer("Server=revature-video-with-friends.database.windows.net,1433;Initial Catalog=RevatureVideoWithFriends;User ID=sqladmin;Password=Password123;", opts =>
                // {
                //     opts.EnableRetryOnFailure(2);
                // });
                options.UseSqlServer("Server=revature-video-with-friends.database.windows.net,1433;Initial Catalog=RevatureVideoWithFriends;User ID=sqladmin;Password=Password123;");
            });
/*             services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = OktaDefaults.ApiAuthenticationScheme;
                options.DefaultChallengeScheme = OktaDefaults.ApiAuthenticationScheme;
                options.DefaultSignInScheme = OktaDefaults.ApiAuthenticationScheme;
            }) */
            /* .AddOktaWebApi(new OktaWebApiOptions()
            {
                OktaDomain = Configuration["OktaDomain"],
            }); */

            services.AddAuthorization();

            services.AddScoped<UnitOfWork>();
            // services.AddCors(options =>
            // {
            //     // The CORS policy is open for testing purposes. In a production application, you should restrict it to known origins.
            //     options.AddPolicy(
            //         "AllowAll",
            //         builder => builder.AllowAnyOrigin()
            //                         .AllowAnyMethod()
            //                         .AllowAnyHeader());
            // });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "StoringApi.Service v1"));
            //}

            // app.UseHttpsRedirection();

            app.UseRouting();

            //app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
