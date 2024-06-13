using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Digital_assistant_backend.Data;
using Digital_assistant_backend;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Digital_assistant_backend.Models;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUserService,userServiceHandler>();
builder.Services.AddScoped<IProjectService,projectService>();
builder.Services.AddScoped<ITaskService, taskService>();




builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    });

builder.Services.AddDbContext<ManagementDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("ManagementConnectionString"))
);
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ManagementDbContext>()
    .AddDefaultTokenProviders();
    
#pragma warning disable CS8604 // Possible null reference argument.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options=>options.TokenValidationParameters=new TokenValidationParameters{
    ValidateIssuer=true,
    ValidateAudience=true,
    ValidateLifetime=true,
    ValidateIssuerSigningKey=true,
    ValidIssuer=builder.Configuration["JWT:Issuer"],
    ValidAudience=builder.Configuration["JWT:Audience"],
    IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))

});
#pragma warning restore CS8604 // Possible null reference argument.

var app = builder.Build();

// Enable CORS
app.UseCors(options =>
{
    options.AllowAnyOrigin()
           .AllowAnyHeader()
           .AllowAnyMethod();
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        await RoleSeeder.SeedRoles(roleManager);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding roles.");
    }
}
app.Run();

