
using Auth_Rol.ASP.NET.Context;
using Auth_Rol.ASP.NET.Filter;
using Auth_Rol.ASP.NET.Mapper;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Build StringConection
var connectionString = builder.Configuration.GetConnectionString("Connection");

//Register Server
builder.Services.AddDbContext<AppDbContext>(op => op.UseNpgsql(connectionString));

//Register Filter
builder.Services.AddControllers(op =>
{
    op.Filters.Add<GlobalFilterExceptions>();
});

// Register Services
//builder.Services.AddScoped<IUserServicesImpl, UserServices>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Cors Policy
builder.Services.AddCors(o =>
{
    o.AddPolicy("CorsPolicy", b =>
    {
        b.WithOrigins("*")
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();
    });
});

//Mapper
var mappConfig = new MapperConfiguration(m =>
{
    m.AddProfile<MappingProfile>();
});
IMapper mapper = mappConfig.CreateMapper();

builder.Services.AddSingleton(mapper);
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
app.UseAuthorization();

app.MapControllers();

app.Run();
