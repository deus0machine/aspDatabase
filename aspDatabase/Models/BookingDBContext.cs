using Microsoft.EntityFrameworkCore;

namespace aspDatabase.Models
{
    public class BookingDBContext : DbContext
    {
        public BookingDBContext(DbContextOptions<BookingDBContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Room>()
                .HasOne(r => r.Hotel)
                .WithMany()  // Assuming Hotel doesn't have a collection of Rooms
                .HasForeignKey(r => r.Idhotel)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Hotel> Hotels{ get; set; }
        public DbSet<Agreement> Agreements { get; set; }
        public DbSet<BookingRequest> BookingRequests { get; set; }

    }
}
