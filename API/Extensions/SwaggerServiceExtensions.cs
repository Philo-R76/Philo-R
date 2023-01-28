using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Builder;
namespace API.Extensions
{
    public static class SwaggerServiceExtensions
    {
     public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)   
     {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo {Title = "PHILVIN API", Version = "v1"});
        });
        return services;
     }
     public static IApplicationBuilder UseSwaggerDocumentation (this IApplicationBuilder app)
     {
        app.UseSwagger();
        app.UseSwaggerUI(c => 
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "PHILVIN API V1");
        });
     
     return app;
    }
}
}