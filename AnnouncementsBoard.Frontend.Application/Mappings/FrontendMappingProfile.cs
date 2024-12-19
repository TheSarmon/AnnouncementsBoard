using AnnouncementsBoard.Frontend.Domain.DTO;
using AnnouncementsBoard.Frontend.Domain.Entities;
using AutoMapper;

namespace AnnouncementsBoard.Frontend.Application.Mappings
{
    public class FrontendMappingProfile : Profile
    {
        public FrontendMappingProfile()
        {
            CreateMap<Announcement, UpdateAnnouncementDTO>();
        }
    }
}
