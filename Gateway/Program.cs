using Gateway.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("ocelot.json", optional:false, reloadOnChange:true);
builder.WebHost.UseUrls("http://*:8888");
builder.Services.AddServicesBuilder(builder.Configuration);

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UserApplicationBuilderExtensions();

app.Run();
