using Cell.Domain.Dto.ImageDto;
using Cell.Domain.Dto.UserDto;
using Cell.Domain.Interfaces.Services;
using Cell.Domain.Result;
using Microsoft.AspNetCore.Mvc;

namespace Cell.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ImageController : ControllerBase
{
    private readonly IImageService _imageService;
    private readonly IWebHostEnvironment _environment;

    public ImageController(IImageService imageService, IWebHostEnvironment environment)
    {
        _imageService = imageService;
        _environment = environment;
    }

    [Route("uploadImages")]
    [HttpPost]
    public async Task<ActionResult<CollectionResult<ImageDto>>> UploadImages(IFormFileCollection files, Guid entityId)
    {
        var response = await _imageService.UploadImagesAsync(files, entityId, _environment.WebRootPath + "\\Images\\");

        if (response.IsSuccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }

    [HttpDelete("deleteImagesByLinkedEntityId")]
    public async Task<ActionResult<CollectionResult<ImageDto>>> DeleteUser(Guid entityId)
    {
        var response = await _imageService.DeleteImagesByLinkedEntityId(entityId);

        if (response.IsSuccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }

    [HttpDelete("deleteImageById")]
    public async Task<ActionResult<BaseResult<ImageDto>>> DeleteImageById(Guid id)
    {
        var response = await _imageService.DeleteImageById(id);

        if (response.IsSuccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }
}