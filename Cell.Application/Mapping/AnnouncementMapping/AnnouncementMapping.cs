using AutoMapper;
using Cell.Domain.Dto.AnnouncementDto;
using Cell.Domain.Entities;

namespace Cell.Application.Mapping.AnnouncementMapping;

public class AnnouncementMapping : Profile
{
    public AnnouncementMapping()
    {
        CreateMap<Announcement, AnnouncementDto>().ReverseMap();
    }
}