using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Examples;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.IO;
using System.Reflection;

namespace SpeedUpCoreAPIExample.Swagger
{
    public static class SwaggerServiceExtensions
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddVersionedApiExplorer(options =>
            {
                //The format of the version added to the route URL (VV = <major>.<minor>) 
                options.GroupNameFormat = "'v'VV";

                //Order API explorer to change /api/v{version}/ to /api/v1/  
                options.SubstituteApiVersionInUrl = true;
            });

            // Get IApiVersionDescriptionProvider service
            IApiVersionDescriptionProvider provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

            services.AddSwaggerGen(options =>
             {
                 //Create description for each discovered API version
                 foreach (ApiVersionDescription description in provider.ApiVersionDescriptions)
                 {
                     options.SwaggerDoc(description.GroupName, 
                         new  Info()
                         {
                             Title = $"Speed Up ASP.NET Core WEB API Application {description.ApiVersion}",
                             Version = description.ApiVersion.ToString(),
                             Description = "Using various approaches to increase .Net Core RESTful WEB API productivity.",
                             TermsOfService = "None",
                             Contact = new Contact
                             {
                                 Name = "Silantiev Eduard",
                                 Email = "",
                                 Url = "https://www.codeproject.com/Members/EduardSilantiev"
                             },
                             License = new License
                             {
                                 Name = "The Code Project Open License (CPOL)",
                                 Url = "https://www.codeproject.com/info/cpol10.aspx"
                             }
                         });
                 }

                 //Extend Swagger for using examples
                 options.OperationFilter<ExamplesOperationFilter>();

                 //Get XML comments file path and include it to Swagger for the JSON documentation and UI.
                 string xmlCommentsPath = Assembly.GetExecutingAssembly().Location.Replace("dll", "xml");
                 options.IncludeXmlComments(xmlCommentsPath);
             });

            return services;
        }

        public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app,
                                        IApiVersionDescriptionProvider provider)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                //Build a swagger endpoint for each discovered API version  
                foreach (ApiVersionDescription description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                    options.RoutePrefix = string.Empty;
                    options.DocumentTitle = "SCAR store API documentation";
                    options.DocExpansion(DocExpansion.None);
                }
            });

            return app;
        }
    }
}
