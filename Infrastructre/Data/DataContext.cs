using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Infrastructure.Data;
public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }
    public DbSet<Product> Products{ get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      
        
        base.OnModelCreating(modelBuilder);
    }
}

