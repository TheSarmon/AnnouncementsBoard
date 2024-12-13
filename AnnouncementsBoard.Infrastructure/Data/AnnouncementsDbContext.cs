using AnnouncementsBoard.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace AnnouncementsBoard.Infrastructure.Data
{
    public class AnnouncementsDbContext : DbContext
    {
        public AnnouncementsDbContext(DbContextOptions<AnnouncementsDbContext> options) : base(options) 
        { 
        }

        public DbSet<Announcement> Announcements { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Announcement>().ToTable("Announcements");
        }
    }
}
