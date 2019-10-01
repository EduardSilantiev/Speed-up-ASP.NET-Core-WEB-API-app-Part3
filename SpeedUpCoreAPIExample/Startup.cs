using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SpeedUpCoreAPIExample.Contexts;
using SpeedUpCoreAPIExample.Exceptions;
using SpeedUpCoreAPIExample.Filters;
using SpeedUpCoreAPIExample.Helpers;
using SpeedUpCoreAPIExample.Interfaces;
using SpeedUpCoreAPIExample.Repositories;
using SpeedUpCoreAPIExample.Services;
using SpeedUpCoreAPIExample.Settings;
using SpeedUpCoreAPIExample.Swagger;

namespace SpeedUpCoreAPIExample
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
            services.AddCors(options =>
            {
                options.AddPolicy("Default", builder =>
                {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyMethod();
                    builder.AllowAnyHeader();
                });
            });

            services.AddMvc()
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddApiVersioning(options =>
            {
                options.ErrorResponses = new VersioningErrorResponseProvider();
            });

  
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });


            services.AddSwaggerDocumentation();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddHttpClient<ISelfHttpClient, SelfHttpClient>();

            services.AddDbContext<DefaultContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultDatabase")));

            //Cache
            services.AddDistributedRedisCache(options =>
            {
                options.InstanceName = Configuration.GetValue<string>("Redis:Name");
                options.Configuration = Configuration.GetValue<string>("Redis:Host");
            });

            //Settings
            services.Configure<ProductsSettings>(Configuration.GetSection("Products"));
            services.Configure<PricesSettings>(Configuration.GetSection("Prices"));
            services.Configure<ApiSettings>(Configuration.GetSection("Api"));

            services.AddSingleton<ValidateIdAsyncActionFilter>();
            services.AddSingleton<ValidatePagingAsyncActionFilter>();

            //Repositories
            services.AddScoped<IProductsRepository, ProductsRepository>();
            services.AddScoped<IPricesRepository, PricesRepository>();

            services.AddScoped<IPricesCacheRepository, PricesCacheRepository>();
            services.AddScoped<IProductCacheRepository, ProductCacheRepository>();

            //Services
            services.AddTransient<IProductsService, ProductsService>();
            services.AddTransient<IPricesService, PricesService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
                          ILoggerFactory loggerFactory, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            loggerFactory.AddFile("Logs/log.txt");

            app.UseMiddleware<ExceptionsHandlingMiddleware>();

            app.UseSwaggerDocumentation(provider);

            app.UseCors("Default");
            app.UseMvc();
        }
    }
}
