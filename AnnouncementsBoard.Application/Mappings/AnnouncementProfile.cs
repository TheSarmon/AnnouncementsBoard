using AnnouncementsBoard.Domain.Entities;
using AnnouncementsBoard.Domain.Models;
using AutoMapper;


namespace AnnouncementsBoard.Application.Mappings
{
    public class AnnouncementProfile : Profile
    {
        public AnnouncementProfile()
        {
            CreateMap<CreateAnnouncementDTO, Announcement>()
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(_ => "Active"));            

            CreateMap<UpdateAnnouncementDTO, Announcement>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src =>
                    string.IsNullOrEmpty(src.Status) ? "Active" : src.Status));
        }
    }
}
