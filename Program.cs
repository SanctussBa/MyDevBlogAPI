using Microsoft.EntityFrameworkCore;
using TheDevBlog.API.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Cors Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("CORSPolicy", builder =>
    {
        builder
            .AllowAnyMethod()
            .AllowAnyHeader()
            .WithOrigins("https://localhost:7253", "http://localhost:3000");
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuration for SQLServer connection
builder.Services.AddDbContext<TheDevBlogDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("TheDevBlogConnectionString")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("CORSPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
