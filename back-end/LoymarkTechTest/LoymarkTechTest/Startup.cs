using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CEZ.LoymarkTechTest.WebAPI;
using MediatR;
using FluentValidation;
using FluentValidation.AspNetCore;
using CEZ.LoymarkTechTest.WebAPI.Infrastructure.Persistence.Entities;
using Microsoft.IdentityModel.Logging;
using CEZ.LoymarkTechTest.WebAPI.Infrastructure.Utils;
using System.Reflection;
using System.IO;

namespace CEZ.LoymarkTechTest
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private class CorsConfiguration
        {
            public string AllowedOrigins { get; set; }
            public string AllowedMethods { get; set; }
            public string AllowedHeaders { get; set; }
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        [Obsolete]
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CEZ.LoymarkTechTest", Version = "v1" });
                var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);

                c.IncludeXmlComments(xmlCommentsFullPath);
            });
            services.AddDbContext<Context>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("LoymarkTechTestDB"));
            });
            ConfigureValidator(services);
            ConfigureMediatR(services);
            services.AddControllers()
                    .AddFluentValidation()
                    .AddJsonOptions(options =>
                    {
                        options.JsonSerializerOptions.PropertyNamingPolicy = null;
                    });

            services.AddSwaggerGen(options =>
            {
                options.CustomSchemaIds(x => x.FullName.Replace("+", "."));
            });

            // Checking if a CORS configuration was provided
            var hasCors = Configuration.GetChildren().Any(item => item.Key == "CORS");
            if (hasCors)
                services.AddCors(options => {
                    // Getting all the CORS policies provided
                    var policies = Configuration.GetSection("CORS").GetChildren().ToList();
                    foreach (var policy in policies)
                    {
                        var policyInformation = Configuration.GetSection(policy.Path).GetChildren();
                        CorsConfiguration policyConfiguration = new CorsConfiguration();
                        Configuration.GetSection(policyInformation.First().Path).Bind(policyConfiguration);
                        // Creating each policy 
                        options.AddPolicy(policyInformation.First().Key,
                        builder =>
                        {
                            if (string.IsNullOrEmpty(policyConfiguration.AllowedOrigins))
                                throw new Exception($"CORS Origins AppSetting is null or empty: AllowedOrigins");
                            else if (policyConfiguration.AllowedOrigins == "*")
                                builder.AllowAnyOrigin();
                            else
                            {
                                foreach (var origin in policyConfiguration.AllowedOrigins.Split(','))
                                {
                                    builder.WithOrigins(origin.Trim());
                                }
                            }

                            if (string.IsNullOrEmpty(policyConfiguration.AllowedHeaders))
                                throw new Exception($"CORS Origins AppSetting is null or empty: AllowedHeaders");
                            else if (policyConfiguration.AllowedHeaders == "*")
                                builder.AllowAnyHeader();
                            else
                            {
                                foreach (var header in policyConfiguration.AllowedHeaders.Split(','))
                                {
                                    builder.WithHeaders(header.Trim());
                                }
                            }

                            if (string.IsNullOrEmpty(policyConfiguration.AllowedMethods))
                                throw new Exception($"CORS Origins AppSetting is null or empty: AllowedMethods");
                            else if (policyConfiguration.AllowedMethods == "*")
                                builder.AllowAnyMethod();
                            else
                            {
                                foreach (var method in policyConfiguration.AllowedMethods.Split(','))
                                {
                                    builder.WithMethods(method.Trim());
                                }
                            }
                        });

                    }
                });
            ConfigurationHelper.InitializeConfiguration(Configuration);
            services.AddHttpContextAccessor();
        }
        private void ConfigureMediatR(IServiceCollection services)
        {
            services.AddMediatR(typeof(GetUserByIdQuery));
            //services.AddMediatR(typeof(GetClientesPaginatedQuery));
        }

        private void ConfigureValidator(IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining(typeof(CreateUserCommand.Validator));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<Context>();
                context.Database.Migrate();
            }
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CEZ.LoymarkTechTest v1"));
            }
            IdentityModelEventSource.ShowPII = true;

            //app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("CorsApiTest");
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
