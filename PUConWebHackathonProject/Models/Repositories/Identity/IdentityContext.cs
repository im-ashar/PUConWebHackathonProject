using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PUConWebHackathonProject.Models.Repositories.Identity
{
    public class IdentityContext : IdentityDbContext<IdentityModel>
    {
        public DbSet<EventModel> Events { get; set; }

        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
        {

        }

    }
}
