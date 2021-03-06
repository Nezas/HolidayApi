namespace HolidayApi.DAL.Data
{
    public class DataContext : DbContext
    {
        public bool IsConnected { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<FromDate> FromDates { get; set; }
        public DbSet<ToDate> ToDates { get; set; }
        public DbSet<HolidayType> HolidayTypes { get; set; }
        public DbSet<DayStatus> DayStatuses { get; set; }
        public DbSet<MaximumFreeDays> MaximumFreeDays { get; set; }
        public DbSet<Holiday> Holidays { get; set; }
        public DbSet<HolidayName> HolidayNames { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            IsConnected = false;
            if(Database.CanConnect())
            {
                IsConnected = true;
            }
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
