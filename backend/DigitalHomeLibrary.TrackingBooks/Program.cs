using DigitalHomeLibrary.TrackingBooks.DataAccess.Entities;
using DigitalHomeLibrary.TrackingBooks.DataAccess.Models;
using DigitalHomeLibrary.TrackingBooks.DataAccess.Repositories;
using DigitalHomeLibrary.TrackingBooks.DataAccess.Repositories.Abstract;
using DigitalHomeLibrary.TrackingBooks.DataAccess.Services;
using DigitalHomeLibrary.TrackingBooks.DataAccess.Services.Abstract;
using DigitalHomeLibrary.TrackingBooks.Domain.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<TrackingBooksDbContext>(options =>
{
    options.UseNpgsql("User ID=books_tracker;Password=hnrsygtgr;Host=postgres_container;Port=5432;Database=BooksDB;");
}); 

builder.Services.AddScoped<IAsyncRepository<Author>, AuthorsRepository>();
builder.Services.AddScoped<IAsyncRepository<BookEntity>, BooksRepositry>();
builder.Services.AddScoped<IAsyncRepository<ReviewEntity>, ReviewsRepository>();
builder.Services.AddScoped<IAsyncRepository<StatusEntity>, StatusesRepository>();
builder.Services.AddScoped<IAsyncRepository<TagEntity>, TagsRepository>();

builder.Services.AddScoped<IBooksService, BooksService>();
builder.Services.AddScoped<IBookTagsService, BooksTagsService>();
builder.Services.AddScoped<IBookReviewsService, BookReviesService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
