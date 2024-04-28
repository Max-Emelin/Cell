using AutoMapper;
using Cell.Domain.Dto.CommentDto;
using Cell.Domain.Entities;

namespace Cell.Application.Mapping.CommentMapping;

public class CommentDtoMapping : Profile
{
    public CommentDtoMapping()
    {
        CreateMap<CommentDto, Comment>().ReverseMap();
    }
}