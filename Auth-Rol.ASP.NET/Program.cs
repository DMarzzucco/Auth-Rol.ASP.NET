
using Auth_Rol.ASP.NET.Filter;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
