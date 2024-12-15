using AnnouncementsBoard.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using AnnouncementsBoard.Domain.Entities;

namespace AnnouncementsBoard.Infrastructure.Repositories
{
    public class AnnouncementRepository : IAnnouncementRepository
    {
        private readonly AnnouncementsDbContext _context;

        public AnnouncementRepository(AnnouncementsDbContext context)
        {
            _context = context;
        }

        public async Task<List<Announcement>> GetAllAsync()
        {
            var query = "EXEC GetAllAnnouncements";
            return await _context.Announcements.FromSqlRaw(query).ToListAsync();
        }

        public async Task<Announcement> GetByIdAsync(int id)
        {
            var query = "EXEC GetAnnouncementById @Id = {0}";

            var result = await _context.Announcements.FromSqlRaw(query, id).ToListAsync();
            return result.FirstOrDefault();
        }

        public async Task CreateAsync(Announcement announcement)
        {
            var query = "EXEC InsertAnnouncement @Title = {0}, @Description = {1}, @CreatedDate = {2}, @Status = {3}, @Category = {4}, @SubCategory = {5}";
            await _context.Database.ExecuteSqlRawAsync(query, announcement.Title, announcement.Description, announcement.CreatedDate, announcement.Status, announcement.Category, announcement.SubCategory);
        }

        public async Task UpdateAsync(Announcement announcement)
        {
            var query = "EXEC UpdateAnnouncement @Id = {0}, @Title = {1}, @Description = {2}, @Status = {3}, @Category = {4}, @SubCategory = {5}";
            await _context.Database.ExecuteSqlRawAsync(query, announcement.Id, announcement.Title, announcement.Description, announcement.Status, announcement.Category, announcement.SubCategory);
        }

        public async Task DeleteAsync(int id)
        {
            var query = "EXEC DeleteAnnouncement @Id = {0}";
            await _context.Database.ExecuteSqlRawAsync(query, id);
        }
    }
}
