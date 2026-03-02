using FluentValidation.AspNetCore;
using InvoiceProject.Extensions;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwagger()
         .AddTaskFlowDbContext(builder.Configuration)
         .AddIdentityAndDb(builder.Configuration)
         .AddAuthenticationAndAuthorization(builder.Configuration)
         .AddCorsPolicy()
         .AddFluentValidation()
         .AddAutoMapperAndOtherServices();


var app = builder.Build();

app.UseTaskFlowPipeLine();


app.Run();