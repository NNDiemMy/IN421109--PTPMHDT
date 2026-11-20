using EduHub.Application.Repositories;
using EduHub.Application.Services;
using EduHub.Infrastructure.Repositories;
using EduHub.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DI services
builder.Services.AddSingleton<IEventRepository, InMemoryEventRepository>();
builder.Services.AddSingleton<INotificationService, EmailNotificationService>();
builder.Services.AddSingleton<INotificationService, SmsNotificationService>();
builder.Services.AddSingleton<NotificationServiceFactory>();
builder.Services.AddScoped<EventService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
