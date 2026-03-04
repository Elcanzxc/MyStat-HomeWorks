using FluentValidation.AspNetCore;
using InvoiceProject.Application.Extensions;
using InvoiceProject.Extensions;
using InvoiceProject.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);



builder.Services.AddSwagger()
         .AddTaskFlowDbContext(builder.Configuration)
         .AddIdentityAndDb(builder.Configuration)
         .AddAuthenticationAndAuthorization(builder.Configuration)
         .AddCorsPolicy()
         .AddFluentValidation();
         

var app = builder.Build();

app.UseTaskFlowPipeLine();


app.Run();