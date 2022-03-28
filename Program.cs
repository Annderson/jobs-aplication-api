using jobs_application_api.Persistence;
using jobs_application_api.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("JobsCs");
builder.Services.AddDbContext<JobsContext>(
    options =>
    {
        options.UseMySql(
                connectionString,
                new MySqlServerVersion(new Version(8, 0, 11)
            )
        );
    }
);

builder.Services.AddScoped<IJobVacancyRepository, JobVacancyRepository>();

// Add services to the container.

builder.Services.AddControllers();
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
