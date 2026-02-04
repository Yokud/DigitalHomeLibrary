using DigitalHomeLibrary.TrackingBooks.Domain.Entities;
using DigitalHomeLibrary.TrackingBooks.Domain.Models;
using DigitalHomeLibrary.TrackingBooks.Repositories;
using DigitalHomeLibrary.TrackingBooks.Repositories.Abstract;
using DigitalHomeLibrary.TrackingBooks.Services;
using DigitalHomeLibrary.TrackingBooks.Services.Abstract;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<TrackingBooksDbContext>(options =>
{
    options.UseNpgsql("User ID=books_tracker;Password=hnrsygtgr;Host=postgres_container;Port=5432;Database=BooksDB;");
}, ServiceLifetime.Singleton); 

builder.Services.AddSingleton<IAsyncRepository<Author>, AuthorsRepository>();
builder.Services.AddSingleton<IAsyncRepository<Book>, BooksRepositry>();
builder.Services.AddSingleton<IAsyncRepository<Review>, ReviewsRepository>();
builder.Services.AddSingleton<IAsyncRepository<Status>, StatusesRepository>();
builder.Services.AddSingleton<IAsyncRepository<Tag>, TagsRepository>();

builder.Services.AddSingleton<IBooksService, BooksService>();
builder.Services.AddSingleton<IBookTagsService, BooksTagsService>();
builder.Services.AddSingleton<IBookReviewsService, BookReviesService>();

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
