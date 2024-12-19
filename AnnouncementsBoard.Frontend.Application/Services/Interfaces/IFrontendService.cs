using AnnouncementsBoard.Frontend.Domain.Models;
using AnnouncementsBoard.Frontend.Domain.DTO;

namespace AnnouncementsBoard.Frontend.Application.Services.Interfaces
{
    public interface IFrontendService
    {
        Task<List<Announcement>> GetAllAsync();
        Task<Announcement> GetByIdAsync(int id);
        Task CreateAsync(CreateAnnouncementDTO dto);
        Task UpdateAsync(int id, UpdateAnnouncementDTO dto);
        Task DeleteAsync(int id);
    }
}
