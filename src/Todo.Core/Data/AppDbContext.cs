using Microsoft.EntityFrameworkCore;
using Todo.Core.Models;

namespace Todo.Core.Data;

public class AppDbContext : DbContext
{
    public DbSet<Models.Todo> Todos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("DataSource=app.db;Cache=shared");
    }
}
