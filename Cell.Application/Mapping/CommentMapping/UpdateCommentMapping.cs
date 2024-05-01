﻿using AutoMapper;
using Cell.Domain.Dto.CommentDto;
using Cell.Domain.Entities;

namespace Cell.Application.Mapping.CommentMapping;

public class UpdateCommentMapping : Profile
{
    public UpdateCommentMapping()
    {
        CreateMap<Comment, UpdateCommentDto>().ReverseMap();
    }
}