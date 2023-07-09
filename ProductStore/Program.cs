global using Microsoft.AspNetCore.Http;
global using ProductStore.Models;
global using Microsoft.AspNetCore.Mvc;
global using ProductStore.NonDbModels;
global using System.ComponentModel.DataAnnotations.Schema;
global using System.Security.Cryptography;
global using System.Security.Claims;
global using Microsoft.IdentityModel.Tokens;
global using System.IdentityModel.Tokens.Jwt;
global using System.Text;
global using Microsoft.AspNetCore.Authorization;
global using ProductStore.Service.UserServices;
global using System.ComponentModel.DataAnnotations;
global using Microsoft.EntityFrameworkCore;
global using ProductStore.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddHttpContextAccessor();
var connectionString = builder.Configuration.GetConnectionString("myconn");
builder.Services.AddDbContext<ProductDbContext>(x => x.UseSqlServer(connectionString));
builder.Services.AddSwaggerGen(options => {
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme {
        Description = "Standard Authorization header using the Bearer scheam(\"bearer {token)}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings:TokenKey").Value!)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});
builder.Services.AddCors(options => options.AddPolicy(name: "ProductFront",
    policy => {
        policy.WithOrigins("https://localhost:7194").AllowAnyMethod().AllowAnyHeader();
    }));

var app = builder.Build();

// Configure the HTTP request pipeline.
if(app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("ProductFront");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
