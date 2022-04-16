using jobs_application_api.Persistence;
using jobs_application_api.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Pomelo.EntityFrameworkCore.MySql.Internal;
using Serilog;
using Serilog.Configuration;

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
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo{
        Title = "Job Application Api",
        Version = "v1",
        Contact = new OpenApiContact
        {
            Name = "Anderson Lima",
            Email = "anderson.rl.tr@gamil.com",
            Url = new Uri("https://www.linkedin.com/in/anderson-r-lima/")
        }
    });
    var xmlFile = "jobs-application-api.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

builder.Host.ConfigureAppConfiguration((hostingContext, config) => {
    Serilog.Log.Logger = new LoggerConfiguration()
        .Enrich.FromLogContext()
        .WriteTo.MySQL(connectionString )
        .CreateLogger();
}).UseSerilog();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
