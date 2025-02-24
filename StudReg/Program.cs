using Microsoft.EntityFrameworkCore;
using StudReg.Context;
using StudReg.Repositories.Implementaions;
using StudReg.Repositories.Interfaces;
using StudReg.Services.Interfaces;
using StudReg.Services.Implementations;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
 builder.Services.AddSwaggerGen(c =>
 {
    //  c.SwaggerDoc("v1", new OpenApiInfo
    //  {
    //      Version = "v1",
    //      Title = "MediCall API",
    //      Description = "A simple API documentation for the MediCall Portal"
    //  });

     c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
     {
         Name = "Authorization",
         Type = SecuritySchemeType.ApiKey,
         Scheme = "Bearer",
         BearerFormat = "JWT",
         In = ParameterLocation.Header,
         Description = "JWT Authorization header using the Bearer scheme."
     });

     c.AddSecurityRequirement(new OpenApiSecurityRequirement
     {
         {
             new OpenApiSecurityScheme
             {
                 Reference = new OpenApiReference
                 {
                     Type = ReferenceType.SecurityScheme,
                     Id = "Bearer"
                 }
             },
             new string[] {}
         }
     });
     
 });
builder.Services.AddDbContext<StudRegContext>(config => config.UseMySQL(builder.Configuration.GetConnectionString("StudRegString")));

builder.Services.AddScoped<IGuardianRepository, GuardianRepository>();
builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IGuardianService, GuardianService>();
builder.Services.AddScoped<IProfileService, ProfileService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IGuardianService, GuardianService>();
builder.Services.AddScoped<HttpContextAccessor>();
builder.Services.AddScoped<ICurrentUser, CurrentUser>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(config =>
// {
//     config.LoginPath = "/api/auth/login";
//     config.LogoutPath = "/api/auth/logout";
//     config.ExpireTimeSpan = TimeSpan.FromMinutes(30);
// });
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = "https://your-auth-server.com"; // OAuth provider URL
        options.Audience = "your-audience"; // API identifier
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = "your-app",
            ValidateIssuer = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ggdghsdghghsdgvyvdsyvvvvvvvvvvvvvyygvdsytvdsydvsgsdvdysvdsyvchvchdvcdycudbdvdusbdcus")),
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true
        };
    });

builder.Services.AddAuthorization();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
// app.UseMiddleware<ApiKeyMiddleware>();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();
app.Run();
