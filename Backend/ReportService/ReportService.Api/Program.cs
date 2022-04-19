using MassTransit;
using Microsoft.EntityFrameworkCore;
using ReportService.Application;
using ReportService.Data;
using ReportService.Data.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        policy =>
        {
            policy.AllowAnyHeader().AllowAnyOrigin();
        });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IReportRepository, ReportRepository>();
builder.Services.AddTransient<ReportApp>();
builder.Services.AddDbContext<ReportContext>(options =>
{
    var connString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseNpgsql(connString);
});

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((cfx, cnf) =>
    {
        cnf.Host(Environment.GetEnvironmentVariable("RabbitMQConnectionString"));

        cnf.ConfigureEndpoints(cfx);
    });
});

builder.Services.Configure<MassTransitHostOptions>(options =>
{
    options.WaitUntilStarted = true;
    options.StartTimeout = TimeSpan.FromSeconds(30);
    options.StopTimeout = TimeSpan.FromMinutes(1);
});

var app = builder.Build();
app.UseCors("CorsPolicy");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
using (var Scope = app.Services.CreateScope())
{
    var context = Scope.ServiceProvider.GetService<ReportContext>();
    context?.Database.Migrate();
}
app.Run();