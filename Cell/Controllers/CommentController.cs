﻿using Cell.Domain.Dto.CommentDto;
using Cell.Domain.Entities;
using Cell.Domain.Interfaces.Services;
using Cell.Domain.Result;
using Microsoft.AspNetCore.Mvc;

namespace Cell.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommentController : ControllerBase
{
    private readonly ICommentService _commentService;

    public CommentController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    /// <summary>
    /// Получение комментариев о пользователе.
    /// </summary>
    /// <param name="userId"> Идентификатор пользователя. </param>
    /// <returns> Комментарии к пользователю. </returns>
    [Route("getUserComments")]
    [HttpGet]
    public async Task<ActionResult<CollectionResult<CommentDto>>> GetUserComments(Guid userId)
    {
        var response = await _commentService.GetUserCommentsAsync(userId);

        if (response.IsSuccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }

    /// <summary>
    /// Создание комментария.
    /// </summary>
    /// <param name="dto"> Комментарий. </param>
    /// <returns> Комментарий. </returns>
    [Route("createComment")]
    [HttpPost]
    public async Task<ActionResult<CollectionResult<CommentDto>>> CreateComment(CreateCommentDto dto)
    {
        var response = await _commentService.CreateCommentAsync(dto);

        if (response.IsSuccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }

    [Route("updateComment")]
    [HttpPut]
    public async Task<ActionResult<BaseResult<Comment>>> UpdateCommentAsync(UpdateCommentDto dto)
    {
        var response = await _commentService.UpdateCommentAsync(dto);

        if (response.IsSuccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }

    /// <summary>
    /// Удаление комментария.
    /// </summary>
    /// <param name="Id"> Id удаляемого комментария. </param>
    /// <returns> Удаляемый комментарий. </returns>
    [HttpDelete("deleteComment")]
    public async Task<ActionResult<BaseResult<CommentDto>>> DeleteComment(Guid id)
    {
        var response = await _commentService.DeleteCommentByIdAsync(id);

        if (response.IsSuccess)
        {
            return Ok(response);

        }

        return BadRequest(response);
    }
}