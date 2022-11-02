using Microsoft.EntityFrameworkCore;
using RaveFlixAPI.Models;

namespace RaveFlixAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Festival> Festivals { get; set; } = null!;
        public DbSet<Genre> Genres { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
    }
}
