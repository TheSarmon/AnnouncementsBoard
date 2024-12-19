using AnnouncementsBoard.Domain.Entities;
using AnnouncementsBoard.Domain.DTO;
using AutoMapper;

namespace AnnouncementsBoard.Application.Mappings
{
    public class AnnouncementMappingProfile : Profile
    {
        public AnnouncementMappingProfile()
        {
            CreateMap<CreateAnnouncementDTO, Announcement>()
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(_ => "Активне"));            

            CreateMap<UpdateAnnouncementDTO, Announcement>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src =>
                    string.IsNullOrEmpty(src.Status) ? "Активне" : src.Status));
        }
    }
}
