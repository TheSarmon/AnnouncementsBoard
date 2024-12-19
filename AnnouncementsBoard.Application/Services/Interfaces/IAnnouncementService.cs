using AnnouncementsBoard.Domain.Entities;
using AnnouncementsBoard.Domain.DTO;

namespace AnnouncementsBoard.Application.Services.Interfaces
{
    public interface IAnnouncementService
    {
        Task<List<Announcement>> GetAllAnnouncementsAsync();
        Task<Announcement> GetAnnouncementByIdAsync(int id);
        Task<Announcement> CreateAnnouncementAsync(CreateAnnouncementDTO announcementDTO);
        Task<Announcement> UpdateAnnouncementAsync(int id, UpdateAnnouncementDTO announcementDTO);
        Task DeleteAnnouncementAsync(int id);
    }
}
