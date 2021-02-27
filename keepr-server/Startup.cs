using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using keepr_server.Repositories;
using keepr_server.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MySqlConnector;

namespace keepr
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

      // Authenticate with Bearer Token
      services.AddAuthentication(options =>
      {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      }).AddJwtBearer(options =>
      {
        options.Authority = $"https://{Configuration["Auth0:Domain"]}/";
        options.Audience = Configuration["Auth0:Audience"];
      });

      // CORS for client connection
      services.AddCors(options =>
      {
        options.AddPolicy("CorsDevPolicy", builder =>
            {
              builder
                        .WithOrigins(new string[]{
                            "http://localhost:8080",
                            "http://localhost:8081"
                        })
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
            });
      });

      // Scoped connection to database
      services.AddScoped<IDbConnection>(x => CreateDbConnection());

      // Repositories
      services.AddTransient<ProfilesRepository>();
      services.AddTransient<KeepsRepository>();
      services.AddTransient<VaultsRepository>();
      services.AddTransient<VaultKeepsRepository>();

      // Services
      services.AddTransient<ProfilesService>();
      services.AddTransient<KeepsService>();
      services.AddTransient<VaultsService>();
      services.AddTransient<VaultKeepsService>();

      // Controllers
      services.AddControllers();

      // Swagger
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "keepr", Version = "v1" });
      });
    }

    private IDbConnection CreateDbConnection()
    {
      return new MySqlConnection(Configuration["Database:Gearhost"]);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "keepr v1"));
      }

      app.UseHttpsRedirection();

      app.UseRouting();

      // Use Authentication
      app.UseAuthentication();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
