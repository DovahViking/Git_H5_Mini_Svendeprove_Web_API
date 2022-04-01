using Microsoft.EntityFrameworkCore;

namespace H5_Svendeprove_Web_API.Models
{
    public class Customer_Context : DbContext
    {
        public Customer_Context(DbContextOptions<Customer_Context> options) : base(options) 
        {
#if DEBUG
#else
this.Database.Migrate();
#endif
        }

        public DbSet<User> user { get; set; }//=> Set<User>();
        public DbSet<Score> score { get; set; }//=> Set<Score>();
        public DbSet<Device> device { get; set; }//=> Set<Device>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string connection_string = configuration.GetConnectionString("Customer_Context");
            optionsBuilder.UseNpgsql(connection_string);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) //increments id
        {
            modelBuilder.Entity<User>()
                .Property(u => u.id)
                .ValueGeneratedOnAdd();
        }
    }
}
