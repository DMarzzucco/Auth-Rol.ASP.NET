namespace User.Configurations
{
    public static class CORSConfiguration
    {
        public static IServiceCollection AddCORSPolicy(this IServiceCollection service)
        {
            service.AddCors(o =>
            {
                o.AddPolicy("CorsPolicy", b =>
                {
                    b.WithOrigins("http://localhost:8888", "http://localhost:5090")
                     .AllowAnyHeader()
                     .AllowAnyMethod()
                     .AllowCredentials();
                });
            });
            return service;
        }
    }
}
