using Microsoft.EntityFrameworkCore;
using MySqlWebApi.Models;

namespace MySqlWebApi.Data
{
    public class DataContext : DbContext
    {
        //Step 2
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        //Step 3 SuperHeroes is the name of tabe in database
        public DbSet<SuperHero> SuperHeroes { get; set; }
    }
}
