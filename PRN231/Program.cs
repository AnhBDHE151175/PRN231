
using PRN231.Entities;
using PRN231.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<ApplicationContext>();
builder.Services.AddScoped<RegionService>();
builder.Services.AddScoped<JobService>();
builder.Services.AddScoped<SkillService>();
builder.Services.AddScoped<StageService>();


builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

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
//testtttt
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
