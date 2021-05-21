using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Options.NetCore.Options;
using Options.NetCore.Services;
using Options.NetCore.Services.Interfaces;
using Options.NetCore.Services.Implementations;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using System.Data;
using Microsoft.AspNetCore.Http;

namespace Options.NetCore
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

            services.AddControllers();

            services.AddSingleton<IEmailService, EmailService>();
            services.AddScoped<IReportService, ReportService>();

            services.Configure<EmailOptions>(EmailOptions.Email, Configuration.GetSection(
                                        EmailOptions.Email));
            services.Configure<EmailOptions>(EmailOptions.AdminEmail, Configuration.GetSection(
                                        EmailOptions.AdminEmail));

            

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Options.NetCore", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, 
                              IWebHostEnvironment env, 
                              IEmailService emailService)
        {
            app.UseExceptionHandler(builder =>
            {

                builder.Use(async (ctx, next) =>
                {
                    var feature = ctx.Features.Get<IExceptionHandlerPathFeature>();
                    ctx.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    string exceptionMessage = feature.Error.Message;
                    emailService.SendAdmin("Found an exception");
                    await ctx.Response.WriteAsync(exceptionMessage);

                });
            });
            
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Options.NetCore v1"));
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
