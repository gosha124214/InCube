using Microsoft.EntityFrameworkCore;
using MyDatabaseAPI;

var builder = WebApplication.CreateBuilder(args);

// Добавление DbContext с подключением к MySQL
var serverVersion = ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"));
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), serverVersion));

// **Добавление контроллеров**
builder.Services.AddControllers();

var app = builder.Build();

// Остальная конфигурация приложения
app.MapControllers();
app.Run();
