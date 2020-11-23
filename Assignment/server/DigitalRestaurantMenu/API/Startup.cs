using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.context.mongo;
using Domain.repository.repositories.implementations;
using Domain.repository.repositories.interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MediatR;
using AutoMapper;
using FluentValidation.AspNetCore;
using Shared;

namespace API
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
            services.AddControllers()
                .AddFluentValidation(cfg => {
                    cfg.RegisterValidatorsFromAssemblyContaining<Application.Dishes.Create>();
                });
            services.AddMediatR(typeof(Application.Dishes.List.Handler).Assembly);
            // services.AddSwaggerGen(c =>
            // {
            //     c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
            // });
            services.AddScoped<IMongoDRMDbContext, MongoDRMDbContext>(serviceProvider =>
                new MongoDRMDbContext(Constants.MongoUrl, Constants.DatabaseName)
            );
            services.AddAutoMapper(typeof(Application.Dishes.List.Handler));
            services.AddScoped<IDishRepository, DishRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                // app.UseSwagger();
                // app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
