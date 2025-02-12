using User.Configurations.DBConnect.Extensions;
using User.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddServicesBuilder(builder.Configuration);
builder.Configuration.AddJsonFile("appsettings.json");

//builder.WebHost.UseUrls("http://*:1080");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UserApplicationBuilderExtensions();
app.ApplyMigration();
app.MapControllers();
app.Run();