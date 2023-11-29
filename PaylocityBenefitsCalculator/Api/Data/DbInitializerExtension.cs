using Api.Data;

internal static class DbInitializerExtension
{
    public static IApplicationBuilder SeedSqlServer(this IApplicationBuilder app)
    {
        ArgumentNullException.ThrowIfNull(app, nameof(app));

        using var scope = app.ApplicationServices.CreateScope();
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<EmployeeDbContext>();
            DbInitializer.Initialize(context);
        }
        catch (Exception)
        {

        }

        return app;
    }
}