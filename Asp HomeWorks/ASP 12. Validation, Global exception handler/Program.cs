using FluentValidation;
using FluentValidation.AspNetCore;
using InvoiceProject.Abtractions.Interfaces;
using InvoiceProject.DataAccess;
using InvoiceProject.Mappings;
using InvoiceProject.Middlewares;
using InvoiceProject.Models;
using InvoiceProject.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);




builder.Services.AddDbContext<InvoiceDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IInvoiceService, InvoiceService>();
builder.Services.AddScoped<IInvoiceRowService, InvoiceRowService>();
builder.Services.AddScoped<IApplicationUsersService, ApplicationUsersService>();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(
    //options =>
    //{
    //    options.Password.RequireNonAlphanumeric = false;
    //}
    )
    .AddEntityFrameworkStores<InvoiceDbContext>();
//.AddDefaultTokenProviders();

builder.Services.AddValidatorsFromAssemblyContaining<Program>();

builder.Services.AddFluentValidationAutoValidation();


builder.Services.AddControllers();

builder.Services.AddOpenApi();



builder.Services.AddSwaggerGen(
       options =>
       {
           options.SwaggerDoc("v1", new OpenApiInfo
           {
               Version = "v1",
               Title = "TimeLineOfMe API",
               Description = "Timeline of Me is a digital journey through the moments that shaped who I am. From childhood memories to major life milestones, this interactive timeline captures experiences, lessons, achievements, and dreams for the future. Every date tells a story. Every moment matters. ",
               Contact = new OpenApiContact
               {
                   Name = "Elcan Team",
                   Email = "Elcan4361@gmail.com"
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
               options.IncludeXmlComments(xmlPath);
           }

       });

var jwtSettings = builder.Configuration.GetSection("JWTSettings");
var secretKey = jwtSettings["SecretKey"];
var issuer = jwtSettings["Issuer"];
var audience = jwtSettings["Audience"];

//builder.Services
//    .AddAuthentication(
//    options =>
//    {
//        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//    })
//    .AddJwtBearer(
//    options =>
//    {
//        options.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateIssuer = true,
//            ValidateAudience = true,
//            ValidateLifetime = true,
//            ValidateIssuerSigningKey = true,
//            ValidIssuer = issuer,
//            ValidAudience = audience,
//            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!)),
//            ClockSkew = TimeSpan.Zero
//        };
//    }
//    );

builder.Services.AddAuthorization(
    options =>
    {
        options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
        options.AddPolicy("AdminOrManager", policy => policy.RequireRole("Admin", "Manager"));
        options.AddPolicy("UserOrAbove", policy => policy.RequireRole("Admin", "Manager", "User"));
    }
    );

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(
        options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Invoice Manager API v1");
        options.RoutePrefix = string.Empty;
        options.DisplayRequestDuration();
        options.EnableTryItOutByDefault();
        options.EnableFilter();
    });
}
app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseCors("AllowAll");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
