using FluentValidation;
using FluentValidation.AspNetCore;
using InvoiceProject.Abtractions.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

namespace InvoiceProject.Extensions;

public static class ApiServiceCollectionExtensions
{
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {

        services.AddControllers();

        services.AddOpenApi();


        services.AddSwaggerGen(opt => {


            opt.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Invoices API",
                Description = "API for Invoices.",
                Contact = new OpenApiContact
                {
                    Name = "Invoices Team",
                    Email = "support@Invoices.com"
                },
                License = new OpenApiLicense
                {
                    Name = "MIT Licence",
                    Url = new Uri("https://opensource.org/license/mit")
                }
            });

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            if (File.Exists(xmlPath))
            {
                opt.IncludeXmlComments(xmlPath);
            }


            opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Пожалуйста, введите токен в формате: Bearer {твой_токен}",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "bearer"
            });

            opt.AddSecurityRequirement(new OpenApiSecurityRequirement {
           {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[]{}
        }
        });
        });


        return services;

    }

    public static IServiceCollection AddCorsPolicy(this IServiceCollection services )
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", policy =>
            {
                policy.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader();
            });
        });


        return services;
    }

    public static IServiceCollection AddFluentValidation(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<Program>();

        services.AddFluentValidationAutoValidation();

        return services;
    }

}
