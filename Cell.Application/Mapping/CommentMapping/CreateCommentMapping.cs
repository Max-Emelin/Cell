using AutoMapper;
using Cell.Domain.Dto.CommentDto;
using Cell.Domain.Entities;

namespace Cell.Application.Mapping.CommentMapping;

public class CreateCommentMapping : Profile
{
    public CreateCommentMapping()
    {
        CreateMap<Comment, CreateCommentDto>().ReverseMap();
    }
}