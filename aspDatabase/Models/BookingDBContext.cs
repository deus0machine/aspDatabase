using Microsoft.EntityFrameworkCore;

namespace aspDatabase.Models
{
    public class BookingDBContext : DbContext
    {
        public BookingDBContext(DbContextOptions<BookingDBContext> options) : base(options)
        {

        }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Hotel> Hotels{ get; set; }
        public DbSet<Agreement> Agreements { get; set; }
        public DbSet<BookingRequest> BookingRequests { get; set; }

    }
}
