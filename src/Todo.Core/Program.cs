using Todo.Core.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>();

var app = builder.Build();

app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();
