using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

using Case.Data;
using Case.Auth;

namespace Case {
  public class Startup {
    public IConfiguration Configuration { get; }
    
    public Startup(IConfiguration configuration) {
      Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services) {
      // Using InMemoryDatabase so you can run the project easily when testing the project :)
      services.AddDbContext<InMemoryContext>(options => options.UseInMemoryDatabase("CaseDb"));
      services.AddScoped<DbContext>(options => options.GetRequiredService<InMemoryContext>());
      services.AddScoped<ITransactionsRepository, TransactionsRepository>();

      // It's not safe to store keys in config files, but this is just a demonstration
      var jwtSecretKey = Configuration["JwtSecretKey"];
      var jwtIssuer = "transactions-auth.com";
      var jwtAudience = "transactions-clients.com";
      var jwtRole = "Transactions";

      services.AddScoped<IJwtProvider>(provider => new JwtProvider(jwtRole, jwtSecretKey, jwtIssuer, jwtAudience));

      services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
        options.TokenValidationParameters = new TokenValidationParameters {
          ValidateIssuer = true,
          ValidateAudience = true,
          ValidIssuer = jwtIssuer,
          ValidAudience = jwtAudience,
          ValidateLifetime = true,
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecretKey))
        };
      });

      services.AddAuthorization(options => {
        options.AddPolicy(jwtRole, p => p.RequireAuthenticatedUser().RequireRole(jwtRole));
      });

      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
    }

    public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
      if (env.IsDevelopment()) {
        app.UseDeveloperExceptionPage();
      } else {
        app.UseHsts();
      }

      // Seed database
      using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope()) {
        scope.ServiceProvider.GetRequiredService<InMemoryContext>().Database.EnsureCreated();
      }
      
      app.UseAuthentication();
      app.UseHttpsRedirection();
      app.UseMvc();
    }
  }
}
