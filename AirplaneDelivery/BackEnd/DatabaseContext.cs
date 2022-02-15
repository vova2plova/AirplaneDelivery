using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace BackEnd
{
    public class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
    }
}
