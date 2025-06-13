using CoFloPeopleManagement.Application.Interfaces;
using CoFloPeopleManagement.Application.Services;
using CoFloPeopleManagement.Infrastructure;
using CoFloPeopleManagement.Infrastructure.Data;
using CoFloPeopleManagement.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<IPeopleRepository, PersonRepository>();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("PeopleDb"));

// API Configuration
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS (Catering for dev CERT issues on Mac)
const string corsPolicyName = "AllowReactApp";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: corsPolicyName,
        policy =>
        {
            policy.WithOrigins("http://localhost:3000")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(corsPolicyName);
app.UseAuthorization();
app.MapControllers();
app.Run();