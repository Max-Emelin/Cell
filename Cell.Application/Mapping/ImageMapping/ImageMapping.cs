using AutoMapper;
using Cell.Domain.Dto.ImageDto;
using Cell.Domain.Entities;

namespace Cell.Application.Mapping.ImageMapping;

public class ImageMapping : Profile
{
    public ImageMapping()
    {
        CreateMap<Image, ImageDto>().ReverseMap();
    }
}