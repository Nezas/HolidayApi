using HolidayApi.Models;

namespace HolidayApi.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Country> Countries { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<FromDate> FromDates { get; set; }
        public DbSet<ToDate> ToDates { get; set; }
        public DbSet<HolidayType> HolidayTypes { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>()
                .HasOne<FromDate>(c => c.FromDate)
                .WithOne(d => d.Country)
                .HasForeignKey<FromDate>(d => d.CountryCode);

            modelBuilder.Entity<Country>()
                .HasOne<ToDate>(c => c.ToDate)
                .WithOne(d => d.Country)
                .HasForeignKey<ToDate>(d => d.CountryCode);

            modelBuilder.Entity<Region>()
                .HasOne<Country>(r => r.Country)
                .WithMany(c => c.Regions)
                .HasForeignKey(r => r.CountryCode);

            modelBuilder.Entity<HolidayType>()
                .HasOne<Country>(ht => ht.Country)
                .WithMany(c => c.HolidayTypes)
                .HasForeignKey(ht => ht.CountryCode);
        }
    }
}
