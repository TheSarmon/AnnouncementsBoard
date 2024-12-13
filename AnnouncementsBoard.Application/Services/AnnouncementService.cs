using AnnouncementsBoard.Domain.Models;
using AnnouncementsBoard.Infrastructure.Repositories;

namespace AnnouncementsBoard.Application.Services
{
    public class AnnouncementService : IAnnouncementService
    {
        private readonly IAnnouncementRepository _repository;

        public AnnouncementService(IAnnouncementRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Announcement>> GetAllAnnouncementsAsync()
        {
            return await _repository.GetAllAnnouncementsAsync();
        }

        public async Task<IEnumerable<Announcement>> GetAnnouncementsByCategoryAsync(string category)
        {
            return await _repository.GetAnnouncementsByCategoryAsync(category);
        }

        public async Task<Announcement> GetAnnouncementByIdAsync(int id)
        {
            return await _repository.GetAnnouncementByIdAsync(id);
        }

        public async Task AddAnnouncementAsync(Announcement announcement)
        {
            announcement.CreatedDate = DateTime.UtcNow;
            await _repository.AddAnnouncementAsync(announcement);
        }

        public async Task UpdateAnnouncementAsync(Announcement announcement)
        {
            var existingAnnouncement = await _repository.GetAnnouncementByIdAsync(announcement.Id);
            if (existingAnnouncement == null)
            {
                throw new KeyNotFoundException($"Announcement with ID {announcement.Id} not found.");
            }

            await _repository.UpdateAnnouncementAsync(announcement);
        }

        public async Task DeleteAnnouncementAsync(int id)
        {
            var existingAnnouncement = await _repository.GetAnnouncementByIdAsync(id);
            if (existingAnnouncement == null)
            {
                throw new KeyNotFoundException($"Announcement with ID {id} not found.");
            }

            await _repository.DeleteAnnouncementAsync(id);
        }
    }
}
