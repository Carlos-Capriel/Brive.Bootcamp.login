using Brive.Bootcamp.login.DBContext;
using Brive.Bootcamp.login.Services;
using Brive.Bootcamp.login.Services.Implementation;
using Brive.Bootcamp.login.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brive.Bootcamp.login
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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Brive.Bootcamp.login", Version = "v1" });
            });

            services.AddDbContext<ProjectContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("Connection"))
            );

            services.AddTransient<IUsers, ImpUsers>();
            services.AddTransient<IGlobalUtilities, ImpGlobalUtilities>();
            //services.AddCors(o => o.AddPolicy("Login", builder =>
            //{
            //    builder.AllowAnyOrigin()
            //     .AllowAnyMethod()
            //     .AllowAnyHeader();
            //}));
            var secretKey = this.Configuration.GetValue<string>("Secret");
            services.AddAuthentication(auth => {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(jwt => {
                jwt.RequireHttpsMetadata = false;
                jwt.SaveToken = true;
                jwt.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey)),
                    ValidateAudience = false
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Brive.Bootcamp.login v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("Login");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
