using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using UpdatedToDoApp.Application.Queries.GettAllToDoItemQueryHandler;
using UpdatedToDoApp.Infrastructure.Repositories.ToDoItemRepository;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddCors(options => 
{
    options.AddPolicy("AllowSpecificOrigin",
                builder => builder.WithOrigins("http://localhost:4200")
                .AllowAnyHeader()
                .AllowAnyMethod());
});



// MediatR ve handler'ları kaydetme
builder.Services.AddMediatR(typeof(GettAllToDoItemQueryHandler).Assembly);

// Repository'yi kaydetme
builder.Services.AddScoped<IToDoRepository, ToDoItemRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors("AllowSpecificOrigin");

app.MapControllers();

app.Run();
