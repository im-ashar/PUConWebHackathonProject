using Microsoft.EntityFrameworkCore;

namespace PUConWebHackathonProject.Models
{
    public class EventsContext:DbContext
    {
        public EventsContext(DbContextOptions<EventsContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<EventModel> Events { get; set; }

    }
}
