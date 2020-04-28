using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ServiceBus.Web.Api;
using System;

namespace ServiceBus.Web.App
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
            services.AddCors();
            services.AddApiVersioning((options) =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
            });

            services.AddSwaggerGen((options) => {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Servicebus Publisher API",
                    Version = "v1",
                    Description = "Servicebus Publisher API",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Naveen Papisetty",
                        Email = string.Empty
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under MIT",
                        Url = new Uri("https://opensource.org/licenses/MIT"),
                    }
                });
            });
            var assembly = typeof(TopicsController).Assembly;
            services.AddMvc((options) => {
                options.EnableEndpointRouting = false;
            })
                .AddApplicationPart(assembly);

            services.AddBindings(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseApiVersioning();

            app.UseCors((config) =>
            {
                config.AllowAnyOrigin();
                config.AllowAnyHeader();
                config.AllowAnyMethod();
            });

            app.UseSwagger();
            app.UseSwaggerUI((options) => {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Servicebus Publisher API V1");
            });
            app.UseMvc();
        }
    }
}
