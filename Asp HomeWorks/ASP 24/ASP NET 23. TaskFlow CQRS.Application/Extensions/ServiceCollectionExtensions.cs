using ASP_NET_23._TaskFlow_CQRS.Application.Common.Behaivors;
using ASP_NET_23._TaskFlow_CQRS.Application.Features.Auth.Command;
using ASP_NET_23._TaskFlow_CQRS.Application.Mapping;
using ASP_NET_23._TaskFlow_CQRS.Application.Services;
using ASP_NET_23._TaskFlow_CQRS.Application.Validators;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ASP_NET_23._TaskFlow_CQRS.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<RegisterValidator>();
        services.AddAutoMapper(typeof(MappingProfile));
        services.AddScoped<IProjectService, ProjectService>();
        services.AddScoped<ITaskItemService, TaskItemService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IAttachmentService, AttachmentService>();
        services.AddScoped<ITokenGenerator, TokenGenerator>();
        services.AddMediatR(
            config=> 
            config.RegisterServicesFromAssembly(typeof(ServiceCollectionExtensions).Assembly));

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaivor<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaivor<,>));


        return services;
    }
}