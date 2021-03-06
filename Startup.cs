using JOINN.Data;
using JOINN.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace JOINN.API
{
   public class Startup
   {
      public Startup (IConfiguration configuration)
      {
         Configuration = configuration;
      }

      public IConfiguration Configuration { get; }

      // This method gets called by the runtime. Use this method to add services to the container.
      public void ConfigureServices (IServiceCollection services)
      {
         services.AddControllers ();

         services.AddDbContext<JOINNDbContext> (opts =>
         {
            opts.EnableDetailedErrors ();
            opts.UseSqlServer (Configuration.GetConnectionString ("joinn.dev"));
         });

         services.AddTransient<IRegionService, RegionService> ();
         services.AddTransient<ISolutionService, SolutionService> ();
      }

      // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
      public void Configure (IApplicationBuilder app, IWebHostEnvironment env)
      {
         if (env.IsDevelopment ())
         {
            app.UseDeveloperExceptionPage ();
         }

         //app.UseHttpsRedirection ();
         app.UseRouting ();

         // Place before UserRouting and before UseAuthorization
         app.UseCors (builder => builder
            .WithOrigins (
               "http://localhost:8080",
               "http://localhost:4200"
            )
            .AllowAnyMethod ()
            .AllowAnyHeader ()
         );

         //app.UseAuthorization();

         app.UseEndpoints (endpoints =>
         {
            endpoints.MapControllers ();
         });
      }
   }
}
