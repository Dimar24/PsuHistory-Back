using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PsuHistory.API.Host.Helpers;
using PsuHistory.Business.DTO.Models;
using PsuHistory.Business.Service;
using PsuHistory.Data.EF.SQL;
using PsuHistory.Data.Service;
using System.Text;

namespace PsuHistory.API.Host
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
            services.AddAutoMapper(typeof(KeyGuidEntityBase).Assembly);

            services.AddControllers();
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            var server = Configuration["DatabaseServer"] ?? "database";// "database|mssql"
            var port = Configuration["DatabasePort"] ?? "1433"; // Default SQL Server port
            var name = Configuration["DatabaseName"] ?? "psuhistorydb";
            var user = Configuration["DatabaseUser"] ?? "SA"; // Warning do not use the SA account
            var password = Configuration["DatabasePassword"] ?? "Pa55w0rd2021";

            services.AddDbContext<PsuHistoryDbContext>(options => {
                //options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                //options.UseSqlServer($"Server={server}, {port}; Initial Catalog={name}; User ID={user}; Password={password}");
                /**/
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                options.UseSqlServer(Configuration.GetConnectionString("MSSQLDbContext"));
                /**/
                //options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                //options.UseNpgsql(Configuration.GetConnectionString("PostgreSQLDbContext"));
            });
            services.AddPsuHistoryDataService();
            services.AddPsuHistoryService();
            services.AddSwaggerGen(s =>
            {
                s.EnableAnnotations();
                s.SwaggerDoc("v1", new OpenApiInfo { Title = "PSU History", Version = "v1" });
                s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."
                });
                s.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}

                    }
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            DatabaseManagement.MigrationInitialisation(app);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("/swagger/v1/swagger.json", "PSU History V1");
            });
        }
    }
}
