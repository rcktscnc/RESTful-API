using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

using Case.Business;
using Case.Authentication;

namespace Case
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Use InMemoryDatabase so you can run the project right out of the box :)
            services.AddDbContext<InMemoryContext>(options => options.UseInMemoryDatabase("CaseDb"));
            services.AddScoped<DbContext>(options => options.GetRequiredService<InMemoryContext>());
            services.AddScoped<ITransactionsRepository, TransactionsRepository>();

            // It's not safe to store keys in config files, but this is just a demonstration project.
            // Important keys usually are stored in Environment Variables.
            var jwtSecretKey = Configuration["JwtSecretKey"];
            var jwtIssuer = "transactions-auth.com";
            var jwtAudience = "transactions-clients.com";
            var jwtRole = "Transactions";

            services.AddScoped<IJwtProvider>(provider => new JwtProvider(jwtRole, jwtSecretKey, jwtIssuer, jwtAudience));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = jwtIssuer,
                    ValidAudience = jwtAudience,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecretKey))
                };
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(jwtRole, p => p.RequireAuthenticatedUser().RequireRole(jwtRole));
            });

            services.AddCors();

            // Formatting JSON responses to be indented because this is a requirement in the project description.
            // Usually, the response would be minified to make the payload footprint smaller.
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(options => options.SerializerSettings.Formatting = Formatting.Indented );
        }

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

            // Populate the database for the demonstration
            using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                scope.ServiceProvider.GetRequiredService<InMemoryContext>().Database.EnsureCreated();
            }

            // Cross-Origin Requests are very important for Restful APIs.
            // Usually, the API will be called from several different domains and CORS must be enabled
            // and configured accordingly. In this demonstration, I'm allowing any domain to access
            // any HTTP method.
            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
