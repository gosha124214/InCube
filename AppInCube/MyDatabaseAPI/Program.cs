using Microsoft.EntityFrameworkCore;
using MyDatabaseAPI;

var builder = WebApplication.CreateBuilder(args);

// ���������� DbContext � ������������ � MySQL
var serverVersion = ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"));
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), serverVersion));

// **���������� ������������**
builder.Services.AddControllers();

var app = builder.Build();

// ��������� ������������ ����������
app.MapControllers();
app.Run();
