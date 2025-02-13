namespace User.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UserApplicationBuilderExtensions(this IApplicationBuilder app)
        {

            app.UseSwagger();
            app.UseSwaggerUI(op =>
            {
                op.SwaggerEndpoint("/swagger/v1/swagger.json", "API Gateway v1");
                op.SwaggerEndpoint("http://localhost:5024/swagger/v1/swagger.json", "Microservice API 1");
            });
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCors("CorsPolicy");

            return app;
        }
    }
}
