using AutoMapper;
using Cell.Domain.Dto.AnnouncementDto;
using Cell.Domain.Entities;

namespace Cell.Application.Mapping.AnnouncementMapping;

public class CreateAnnouncementDtoMapping : Profile
{
    public CreateAnnouncementDtoMapping()
    {
        CreateMap<Announcement, CreateAnnouncementDto>().ReverseMap();
    }
}