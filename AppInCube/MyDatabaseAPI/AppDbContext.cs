using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

//using System.Data.Entity;
using MySql.Data;
namespace MyDatabaseAPI
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }

    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}
