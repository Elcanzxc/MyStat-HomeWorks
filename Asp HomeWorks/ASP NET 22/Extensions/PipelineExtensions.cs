using InvoiceProject.Middlewares;

namespace InvoiceProject.Extensions;

public static class PipelineExtensions
{
    public static WebApplication UseTaskFlowPipeLine(this WebApplication app )
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(
                options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Invoice Manager API v1");
                    options.RoutePrefix = string.Empty;
                    options.DisplayRequestDuration();
                });
        }
        app.UseMiddleware<GlobalExceptionMiddleware>();

        app.UseCors("AllowAll");

        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllers();


        return app;
    }




}