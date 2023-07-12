using System.Text;
using Libra.App;
using Libra.Database;
using Libra.Database.Interfaces;
using Libra.Database.Repositories;
using Libra.Handlers;
using Libra.Handlers.Interfaces;
using Libra.Hubs;
using Libra.Models;
using Libra.Services;
using Libra.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureAppConfiguration(config =>
    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true));

builder.Services
    .Configure<AppConfig>(builder.Configuration.GetSection("AppConfig"))
    .AddDbContext<LibraContext>(opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("LibraDb")))
    .AddCors()
    .AddSignalR();

// repositories
builder.Services
    .AddScoped<IUserRepository, UserRepository>()
    .AddScoped<IRentRepository, RentRepository>()
    .AddScoped<IBookRepository, BookRepository>()
    .AddScoped<IPicturesRepository, PicturesRepository>()
    .AddScoped<IRegistrationRepository, RegistrationRepository>();

// services
builder.Services
    .AddScoped<IUserService, UserService>()
    .AddScoped<IBookService, BookService>()
    .AddScoped<IPictureService, PictureService>()
    .AddScoped<IRentService, RentService>()
    .AddScoped<IRegistrationService, RegistrationService>()
    .AddScoped<IMessagesService, MessagesService>()
    .AddScoped<IMongoService, MongoService>()
    .AddScoped<ICommentsService, CommentsService>();

// handlers
builder.Services
    .AddScoped<IUserHandler, UserHandler>()
    .AddScoped<IBookHandler, BookHandler>()
    .AddScoped<IRentHandler, RentHandler>()
    .AddScoped<IChatHandler, ChatHandler>()
    .AddScoped<ICommentsHandler, CommentsHandler>();

    builder.Services
        .AddAuthorization()
        .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(x =>
        {
            var keyBytes = Encoding.UTF8.GetBytes(builder.Configuration["AppConfig:JwtConfig:Key"]);
            
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = builder.Configuration["AppConfig:JwtConfig:Issuer"],
                ValidAudience = builder.Configuration["AppConfig:JwtConfig:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true
            };
        });

var app = builder.Build()
    .SetUserRoutes()
    .SetRentRoutes()
    .SetBookRoutes()
    .SetCommentsRoutes()
    .SetChatRoutes();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseCors(
    options => options.SetIsOriginAllowed(x => _ = true)
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()
);

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<MessengerHub>("/messenger");
});

app.Run();