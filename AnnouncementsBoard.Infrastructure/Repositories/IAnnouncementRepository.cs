using AnnouncementsBoard.Domain.Models;

namespace AnnouncementsBoard.Infrastructure.Repositories
{
    public interface IAnnouncementRepository
    {
        Task<IEnumerable<Announcement>> GetAllAnnouncementsAsync();
        Task<IEnumerable<Announcement>> GetAnnouncementsByCategoryAsync(string category);
        Task<Announcement> GetAnnouncementByIdAsync(int id);
        Task AddAnnouncementAsync(Announcement announcement);
        Task UpdateAnnouncementAsync(Announcement announcement);
        Task DeleteAnnouncementAsync(int id);
    }
}
