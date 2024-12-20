using Auth_Rol.ASP.NET.Configuration;

var builder = WebApplication.CreateBuilder(args);
//DatabaseConfiguration
builder.Services.AddDatabaseConfiguration(builder.Configuration);
//Add httpContext
builder.Services.AddHttpContextAccessor();
//Cors Policy
builder.Services.AddCorsPolicy();
//JwtBuilderConfigure
builder.Configuration.AddJsonFile("appsettings.json");
builder.Services.AddJwtAuthentication(builder.Configuration);
//Register Filter
builder.Services.AddCustomController();
// Register Services
builder.Services.AddCustomServices();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerConfiguration();
//Mapper
builder.Services.AddMapperConfig();
builder.Services.AddMvc();
//Por Listen
builder.WebHost.UseUrls("http://*:5024");
// app config

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
