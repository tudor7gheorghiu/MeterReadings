using MeterReadings.DataAccessLayer;
using MeterReadings.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<ReadingsDbContext>(options => options.UseSqlServer (builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IMeterReadingUploader, MeterReadingUploader>();
builder.Services.AddScoped<IAccountsUploader, AccountsUploader>();
builder.Services.AddScoped<IMeterReadingValidator, MeterReadingValidator>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy => policy.WithOrigins("http://localhost:3000")
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
