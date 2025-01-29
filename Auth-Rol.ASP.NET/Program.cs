using Auth_Rol.ASP.NET.Configuration.DbConfiguration.Extensions;
using Auth_Rol.ASP.NET.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddServicesBuilder(builder.Configuration);
builder.Configuration.AddJsonFile("appsettings.json");
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
app.UserApplicationBuilderExtension();
app.ApplyMigration();
app.MapControllers();
app.Run();
