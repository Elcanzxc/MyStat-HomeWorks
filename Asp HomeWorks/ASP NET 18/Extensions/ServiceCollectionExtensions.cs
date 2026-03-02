using FluentValidation;
using FluentValidation.AspNetCore;
using InvoiceProject.Abtractions.Interfaces;
using InvoiceProject.Config;
using InvoiceProject.DataAccess;
using InvoiceProject.Mappings;
using InvoiceProject.Models;
using InvoiceProject.Services;
using InvoiceProject.Storage;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

namespace InvoiceProject.Extensions;

public static class ServiceCollectionExtensions
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

    public static IServiceCollection AddTaskFlowDbContext(
    this IServiceCollection services,
    IConfiguration configuration
    )
    { 
        services.AddDbContext<InvoiceDbContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("DefaultConnectionString")));

        return services;
    }

    public static IServiceCollection AddIdentityAndDb(
    this IServiceCollection services,
    IConfiguration configuration
    )
    {
        services.Configure<JWTConfig>(configuration.GetSection(JWTConfig.SectionName));

        services.AddIdentity<User, IdentityRole>()
            .AddEntityFrameworkStores<InvoiceDbContext>()
            .AddDefaultTokenProviders();


        return services;
    }


    public static IServiceCollection AddAuthenticationAndAuthorization(this IServiceCollection services,IConfiguration configuration){
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


    public static IServiceCollection AddAutoMapperAndOtherServices( this IServiceCollection services )
    {
        services.AddAutoMapper(typeof(MappingProfile));

        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<IInvoiceService, InvoiceService>();
        services.AddScoped<IInvoiceRowService, InvoiceRowService>();
        services.AddScoped<IUsersService, UserService>();
        services.AddScoped<IInvoiceAttachmentService, InvoiceAttachmentService>();
        services.AddScoped<IFileStorage, LocalDiskStorage>();

        services.AddHttpContextAccessor();

        return services;
    }
}
