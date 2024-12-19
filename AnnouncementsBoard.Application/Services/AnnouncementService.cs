using AnnouncementsBoard.Application.Services.Interfaces;
using AnnouncementsBoard.Domain.Entities;
using AnnouncementsBoard.Domain.DTO;
using AutoMapper;
using AnnouncementsBoard.Infrastructure.Repositories.Interfaces;

namespace AnnouncementsBoard.Application.Services
{
    public class AnnouncementService : IAnnouncementService
    {
        private readonly IAnnouncementRepository _repository;
        private readonly IMapper _mapper;

        public AnnouncementService(IAnnouncementRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<Announcement>> GetAllAnnouncementsAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Announcement> GetAnnouncementByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Announcement> CreateAnnouncementAsync(CreateAnnouncementDTO CreateAnnouncementDTO)
        {
            var announcement = _mapper.Map<Announcement>(CreateAnnouncementDTO);

            await _repository.CreateAsync(announcement);

            return announcement;
        }

        public async Task<Announcement> UpdateAnnouncementAsync(int id, UpdateAnnouncementDTO announcementDTO)
        {
            var announcement = await _repository.GetByIdAsync(id);
            if (announcement == null) throw new KeyNotFoundException("Announcement not found.");

            _mapper.Map(announcementDTO, announcement);

            await _repository.UpdateAsync(announcement);

            return announcement;
        }

        public async Task DeleteAnnouncementAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
