using AutoMapper;
using Cell.Domain.Dto.AnnouncementDto;
using Cell.Domain.Entities;

namespace Cell.Application.Mapping.AnnouncementMapping;

public class AnnouncementAnswerMapping : Profile
{
    public AnnouncementAnswerMapping()
    {
        CreateMap<Announcement, AnnouncementAnswerDto>().ReverseMap();
    }
}