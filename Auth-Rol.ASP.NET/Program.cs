
using Auth_Rol.ASP.NET.Context;
using Auth_Rol.ASP.NET.Filter;
using Auth_Rol.ASP.NET.Mapper;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Reflection;
using Auth_Rol.ASP.NET.Users.Services.Interface;
using Auth_Rol.ASP.NET.Users.Services;
using Auth_Rol.ASP.NET.Users.Repository.Interface;
using Auth_Rol.ASP.NET.Users.Repository;
using Microsoft.OpenApi.Models;
using Auth_Rol.ASP.NET.Auth.Services.Interfaces;
using Auth_Rol.ASP.NET.Auth.Services;
using Auth_Rol.ASP.NET.Auth.Filter;

var builder = WebApplication.CreateBuilder(args);

//Build StringConection
var connectionString = builder.Configuration.GetConnectionString("Connection");

//Register Server
builder.Services.AddDbContext<AppDbContext>(op => op.UseNpgsql(connectionString));

//Add httpContext
builder.Services.AddHttpContextAccessor();

//Cors Policy
builder.Services.AddCors(o =>
{
    o.AddPolicy("CorsPolicy", b =>
    {
        b.WithOrigins("http://localhost:300.com")
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();
    });
});

//JwtBuilderConfigure
builder.Configuration.AddJsonFile("appsettings.json");

var secretKey = builder.Configuration.GetSection("JwtSettings").GetSection("secretKey").ToString();

builder.Services.AddAuthentication(conf =>
{
    conf.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    conf.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(conf =>
{
    var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
    var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256Signature);

    conf.RequireHttpsMetadata = false;
    conf.SaveToken = true;
    conf.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = signingKey,
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

//Register Filter
builder.Services.AddControllers(op =>
{
    op.Filters.Add<GlobalFilterExceptions>();
});
// Register Services

//GlobalFilterException
builder.Services.AddScoped<GlobalFilterExceptions>();
//RolesAuth
builder.Services.AddScoped<RolesAuthentication>();
//JwtAuth
builder.Services.AddScoped<JwtAuthFilter>();
//AuthSerives
builder.Services.AddScoped<LocalAuthFilter>();
builder.Services.AddScoped<IAuthServices, AuthServices>();
//UserServices
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserServices>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(op =>
{
    op.EnableAnnotations();
    op.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Auth Rols",
        Description = "An ASP.NET Core Web Apit Auth Controller"
    });
    op.SchemaFilter<SwaggerSchemaExampleFilter>();

    var xmLFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    op.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmLFileName));
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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
