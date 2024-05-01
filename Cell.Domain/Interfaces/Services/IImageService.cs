using Cell.Domain.Dto.ImageDto;
using Cell.Domain.Result;
using Microsoft.AspNetCore.Http;

namespace Cell.Domain.Interfaces.Services;
public interface IImageService
{
    Task<CollectionResult<ImageDto>> UploadImagesAsync(IFormFileCollection files, Guid entityId, string fileFolder);
    Task<BaseResult<int>> DeleteImagesByLinkedEntityId(Guid entityId);
    Task<BaseResult<ImageDto>> DeleteImageById(Guid id);
}