using DigitalHomeLibrary.BookService.Application.Services;
using DigitalHomeLibrary.BookService.Domain.Repositories;
using DigitalHomeLibrary.BookService.Domain.Services;
using DigitalHomeLibrary.BookService.Infractructure.DataAccess.Models;
using DigitalHomeLibrary.BookService.Infractructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();

builder.Configuration.AddJsonFile("appsettings.json");

builder.Services.AddDbContext<BookServiceDbContext>(options =>
{
    options.UseNpgsql("User ID=book_user;Password=book_password;Host=postgres;Port=5432;Database=book_db;");
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.Audience = "book-service-api";
    options.Authority = "http://keycloak:8080/realms/digital-home-library";
    options.MetadataAddress = "http://keycloak:8080/realms/digital-home-library/.well-known/openid-configuration";
    options.MapInboundClaims = false;

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidIssuer = "http://localhost:8080/realms/digital-home-library",
        ValidAudience = "book-service-api",
        RoleClaimType = "realm_role"
    };

    options.RequireHttpsMetadata = false;
});
builder.Services.AddAuthorizationBuilder().AddPolicy("Moderators", policy =>
{
    policy.RequireRole("moderator");
});
builder.Services.AddAuthorization();

builder.Services.AddScoped<IBookRepository, EFCoreBookRepository>();
builder.Services.AddScoped<IAuthorRepository, EFCoreAuthorRepository>();
builder.Services.AddScoped<IReviewRepository, EFCoreReviewRepository>();
builder.Services.AddScoped<IBookTagRepository, EFCoreBookTagRepository>();

builder.Services.AddScoped<BookReviewsService>();
builder.Services.AddScoped<BooksService>();
builder.Services.AddScoped<AuthorService>();
builder.Services.AddScoped<ReviewService>();
builder.Services.AddScoped<BookTagsService>();

var app = builder.Build();

using var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<BookServiceDbContext>();
context.Database.EnsureCreated();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.WithTitle("Book Service")
               .WithTheme(ScalarTheme.BluePlanet)
               .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.Http);
    });
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
