using AnnouncementsBoard.Domain.Entities;

namespace AnnouncementsBoard.Infrastructure.Repositories
{
    public interface IAnnouncementRepository
    {
        Task<List<Announcement>> GetAllAsync();
        Task<Announcement> GetByIdAsync(int id);
        Task CreateAsync(Announcement announcement);
        Task UpdateAsync(Announcement announcement);
        Task DeleteAsync(int id);
    }
}
