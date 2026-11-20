using EduHub.Application.Repositories;
using EduHub.Application.Services;
using EduHub.Infrastructure.Repositories;
using EduHub.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// 1. Thêm Controllers
builder.Services.AddControllers();

// 2. Cấu hình Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 3. Đăng ký Dependency Injection theo Clean Architecture

// Đăng ký InMemoryEventRepository dạng Singleton (Tránh mất dữ liệu giữa các HTTP Request như đã giải thích ở Câu 5d)
builder.Services.AddSingleton<IEventRepository, InMemoryEventRepository>();

// Đăng ký các dịch vụ thông báo
builder.Services.AddSingleton<INotificationService, EmailNotificationService>();
builder.Services.AddSingleton<INotificationService, SmsNotificationService>();

// Đăng ký Factory chọn kênh thông báo
builder.Services.AddSingleton<NotificationServiceFactory>();

// Đăng ký EventService tầng Application
builder.Services.AddScoped<EventService>();

var app = builder.Build();

// 4. Cấu hình HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "EduHub API v1");
        c.RoutePrefix = string.Empty; // Mở Swagger ngay tại root URL (http://localhost:<port>/)
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
