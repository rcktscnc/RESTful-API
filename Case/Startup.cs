using System;
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

using Case.Data;

namespace Case {
  public class Startup {
    public IConfiguration Configuration { get; }
    
    public Startup(IConfiguration configuration) {
      Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services) {
      services.AddDbContext<InMemoryContext>(options => options.UseInMemoryDatabase("CaseDb"));

      services.AddScoped<DbContext>(options => options.GetRequiredService<InMemoryContext>());
      services.AddScoped<TransactionsRepository>();
      services.AddScoped<IRepository<Transaction>, Repository<Transaction>>();

      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
    }

    public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
      if (env.IsDevelopment()) {
        app.UseDeveloperExceptionPage();
      } else {
        app.UseHsts();
      }

      // Seed data to database
      using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope()) {
        scope.ServiceProvider.GetRequiredService<InMemoryContext>().Database.EnsureCreated();
      }

      app.UseHttpsRedirection();
      app.UseMvc();
    }
  }
}
