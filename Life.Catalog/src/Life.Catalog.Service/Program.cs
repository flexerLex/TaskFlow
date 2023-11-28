var builder = WebApplication.CreateBuilder(args);

// Добавляем Swagger для документирования API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Примерная интеграция с Google Calendar и Telegram
builder.Services.AddSingleton<IGoogleCalendarService, DummyGoogleCalendarService>();
builder.Services.AddSingleton<ITelegramService, DummyTelegramService>();

var app = builder.Build();

// Настройка Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Простая логика для управления задачами
var tasks = new List<Task>();

app.MapPost("/tasks", (Task task) =>
{
    tasks.Add(task);
    // TODO: Добавить задачу в Google Calendar
    // TODO: Отправить уведомление в Telegram
    return Results.Ok(task);
}).WithName("AddTask");



app.MapGet("/tasks", () =>
{
    return Results.Ok(tasks);
}).WithName("GetTasks");

app.Run();

// Примерные интерфейсы и службы
public interface IGoogleCalendarService { /* Методы для работы с Google Calendar */ }
public class DummyGoogleCalendarService : IGoogleCalendarService { /* Простая имплементация */ }

public interface ITelegramService { /* Методы для работы с Telegram */ }
public class DummyTelegramService : ITelegramService { /* Простая имплементация */ }

// Модель для представления задачи
public record Task(string Title, string Description, DateTime DueDate);
//

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
