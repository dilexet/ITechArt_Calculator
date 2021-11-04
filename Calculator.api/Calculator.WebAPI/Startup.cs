using System;
using AutoMapper;
using Calculator.BLL.Abstract;
using Calculator.BLL.Services;
using Calculator.DAL.Abstract;
using Calculator.DAL.Factory;
using Calculator.DAL.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;

namespace Calculator.WebAPI
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
            var connectionString = Configuration.GetConnectionString("DefaultConnection");

            services.AddControllers();
            
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IContextFactory, AppDbContextFactory>();
           
            services.AddScoped<IRepository>(provider =>
                new GenericRepository(connectionString, provider.GetService<IContextFactory>()));
           
            services.AddScoped<ICalculatorService>(provider =>
                new CalculatorService(provider.GetService<IRepository>(), provider.GetService<IMapper>()));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Calculator.WebAPI", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Calculator.WebAPI v1"));
            }

            app.UseSerilogRequestLogging(options =>
            {
                options.MessageTemplate = "Handled {RequestPath}";
                options.GetLevel = (_, _, _) => LogEventLevel.Error;
                options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
                {
                    diagnosticContext.Set("RequestHost", httpContext.Request.Host.Value);
                    diagnosticContext.Set("RequestScheme", httpContext.Request.Scheme);
                };
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}