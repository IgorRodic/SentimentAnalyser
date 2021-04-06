using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SentimentAnalyser.Application;
using SentimentAnalyser.Infrastructure;
using SentimentAnalyser.Infrastructure.Database;

namespace SentimentAnalyser.WebApi
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
            services.AddApplication();
            services.AddInfrastructure(Configuration);

            services.AddHttpContextAccessor();

            //services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddHealthChecks().AddDbContextCheck<ApplicationDbContext>();

            //services.AddControllers(options =>
            //        options.Filters.Add<ApiExceptionFilterAttribute>())
            //    .AddFluentValidation();

            //services.AddOpenApiDocument(configure =>
            //{
            //    configure.Title = "CleanArchitecture API";
            //    configure.AddSecurity("JWT", Enumerable.Empty<string>(), new OpenApiSecurityScheme
            //    {
            //        Type = OpenApiSecuritySchemeType.ApiKey,
            //        Name = "Authorization",
            //        In = OpenApiSecurityApiKeyLocation.Header,
            //        Description = "Type into the textbox: Bearer {your JWT token}."
            //    });

            //    configure.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));
            //});

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SentimentAnalyser.WebApi", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SentimentAnalyser.WebApi v1"));
            }

            app.UseHealthChecks("/health");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}