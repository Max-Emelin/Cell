using AutoMapper;
using Cell.Domain.Dto.CommentDto;
using Cell.Domain.Entities;

namespace Cell.Application.Mapping.CommentMapping;

public class CommentMapping : Profile
{
    public CommentMapping()
    {
        CreateMap<Comment, CommentDto>().ReverseMap();
    }
}