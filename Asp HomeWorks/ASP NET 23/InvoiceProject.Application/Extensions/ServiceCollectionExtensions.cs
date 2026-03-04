using FluentValidation;
using InvoiceProject.Abtractions.Interfaces;
using InvoiceProject.Application.Common.Behaivors;
using InvoiceProject.Config;
using InvoiceProject.Mappings;
using InvoiceProject.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceProject.Application.Extensions;

public static class ServiceCollectionExtensions
{

    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MappingProfile));

        services.AddScoped<IInvoiceExportService, InvoiceExportService>();
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<IInvoiceService, InvoiceService>();
        services.AddScoped<IInvoiceRowService, InvoiceRowService>();
        services.AddScoped<IUsersService, UserService>();
        services.AddScoped<IInvoiceAttachmentService, InvoiceAttachmentService>();

        services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(ServiceCollectionExtensions).Assembly));


        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaivor<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaivor<,>));

        services.AddHttpContextAccessor();

        return services;
    }
}
