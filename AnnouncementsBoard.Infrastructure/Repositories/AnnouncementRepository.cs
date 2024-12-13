using AnnouncementsBoard.Infrastructure.Data;
using AnnouncementsBoard.Domain.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace AnnouncementsBoard.Infrastructure.Repositories
{
    public class AnnouncementRepository : IAnnouncementRepository
    {
        private readonly AnnouncementsDbContext _context;

        public AnnouncementRepository(AnnouncementsDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Announcement>> GetAllAnnouncementsAsync()
        {
            return await _context.Announcements
                .FromSqlRaw("EXEC GetAllAnnouncements").ToListAsync();
        }

        public async Task<IEnumerable<Announcement>> GetAnnouncementsByCategoryAsync(string category)
        {
            var categoryParam = new SqlParameter("@Category", category);

            return await _context.Announcements
                                 .FromSqlRaw("EXEC GetAnnouncementsByCategory @Category", categoryParam)
                                 .ToListAsync();
        }

        public async Task<Announcement> GetAnnouncementByIdAsync(int id)
        {
            var result = await _context.Announcements
                            .FromSqlRaw("EXEC GetAnnouncementById @Id", new SqlParameter("@Id", id))
                            .ToListAsync();

            return result.SingleOrDefault();
        }

        public async Task AddAnnouncementAsync(Announcement announcement)
        {
            var titleParam = new SqlParameter("@Title", announcement.Title);
            var descriptionParam = new SqlParameter("@Description", announcement.Description);
            var createdDateParam = new SqlParameter("@CreatedDate", announcement.CreatedDate);
            var statusParam = new SqlParameter("@Status", announcement.Status);
            var categoryParam = new SqlParameter("@Category", announcement.Category);
            var subCategoryParam = new SqlParameter("@SubCategory", announcement.SubCategory);

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC InsertAnnouncement @Title, @Description, @CreatedDate, @Status, @Category, @SubCategory",
                titleParam, descriptionParam, createdDateParam, statusParam, categoryParam, subCategoryParam
            );
        }

        public async Task UpdateAnnouncementAsync(Announcement announcement)
        {
            var idParam = new SqlParameter("@Id", announcement.Id);
            var titleParam = new SqlParameter("@Title", announcement.Title);
            var descriptionParam = new SqlParameter("@Description", announcement.Description);
            var statusParam = new SqlParameter("@Status", announcement.Status);
            var categoryParam = new SqlParameter("@Category", announcement.Category);
            var subCategoryParam = new SqlParameter("@SubCategory", announcement.SubCategory);

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC UpdateAnnouncement @Id, @Title, @Description, @Status, @Category, @SubCategory",
                idParam, titleParam, descriptionParam, statusParam, categoryParam, subCategoryParam
            );
        }

        public async Task DeleteAnnouncementAsync(int id)
        {
            var idParam = new SqlParameter("@Id", id);
            await _context.Database.ExecuteSqlRawAsync("EXEC DeleteAnnouncement @Id", idParam);
        }
    }
}
