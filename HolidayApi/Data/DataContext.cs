using HolidayApi.Models;

namespace HolidayApi.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Country> Countries { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
    }
}
