using InvoiceProject.Abtractions.Interfaces;
using InvoiceProject.Config;
using InvoiceProject.DataAccess;
using InvoiceProject.Models;
using InvoiceProject.Repositories;
using InvoiceProject.Storage;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceProject.Infrastructure.Extensions;

public static class InfrastructureServiceCollectionExtensions
{

    public static IServiceCollection AddIdentityAndDb(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JWTConfig>(configuration.GetSection(JWTConfig.SectionName));

        services.AddIdentity<User, IdentityRole>()
            .AddEntityFrameworkStores<InvoiceDbContext>()
            .AddDefaultTokenProviders();


        return services;
    }
    public static IServiceCollection AddTaskFlowDbContext(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddDbContext<InvoiceDbContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("DefaultConnectionString")));

        return services;
    }

    public static IServiceCollection AddAuthenticationAndAuthorization(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtConfig = new JWTConfig();
        configuration.GetSection(JWTConfig.SectionName).Bind(jwtConfig);

        services.AddAuthentication(options => {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options => {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtConfig.Issuer,
                ValidAudience = jwtConfig.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.SecretKey)),
                ClockSkew = TimeSpan.Zero
            };
        });

        return services;
    }
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddScoped<IFileStorage, LocalDiskStorage>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IInvoiceRepository, InvoiceRepository>();
        services.AddScoped<IInvoiceRowRepository, InvoiceRowRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IInvoiceAttachmentRepository, InvoiceAttachmentRepository>();
        return services;
    }
}