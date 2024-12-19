using AnnouncementsBoard.Frontend.Domain.Entities;
using AnnouncementsBoard.Frontend.Domain.DTO;

namespace AnnouncementsBoard.Frontend.Application.Services.Interfaces
{
    public interface IFrontendService
    {
        Task<List<Announcement>> GetAllAsync();
        Task<Announcement> GetByIdAsync(int id);
        Task<List<Announcement>> GetFilteredAsync(string category, string subcategory, string searchQuery);
        Task CreateAsync(CreateAnnouncementDTO dto);
        Task UpdateAsync(int id, UpdateAnnouncementDTO dto);
        Task DeleteAsync(int id);
    }
}
