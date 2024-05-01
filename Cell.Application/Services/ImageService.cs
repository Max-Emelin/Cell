using AutoMapper;
using Cell.Application.Resources;
using Cell.Domain.Dto.AnnouncementDto;
using Cell.Domain.Dto.ImageDto;
using Cell.Domain.Entities;
using Cell.Domain.Enum;
using Cell.Domain.Interfaces;
using Cell.Domain.Interfaces.Services;
using Cell.Domain.Result;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Trashcan.Application.Services;

public class ImageService : IImageService
{
    private readonly IBaseRepository<Image> _repository;
    private readonly IMapper _mapper;

    public ImageService
    (
        IBaseRepository<Image> repository,
        IMapper mapper
    )
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<BaseResult<ImageDto>> DeleteImageById(Guid id)
    {
        try
        {
            var image = await _repository.GetAll()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (image == null)
            {
                return new BaseResult<ImageDto>()
                {
                    ErrorMassage = ErrorMessage.DataNotFount,
                    ErrorCode = (int)ErrorCode.DataNotFount
                };
            }

            if (File.Exists(image.Path))
                File.Delete(image.Path);

            await _repository.RemoveAsync(image);

            return new BaseResult<ImageDto>()
            {
                Data = _mapper.Map<ImageDto>(image)
            };
        }
        catch (Exception e)
        {
            return new BaseResult<ImageDto>()
            {
                ErrorMassage = ErrorMessage.InternalServerError,
                ErrorCode = (int)ErrorCode.InternalServerError
            };
        }
    }

    public async Task<List<string>> GetEntityImagePaths(Guid entityId)
    {
        try
        {
            var imagesPaths = await _repository.GetAll()
                .Where(x => x.AnnouncementId == entityId)
                .Select(x => x.Path)
                .ToListAsync();
            
            return await _repository.GetAll()
                .Where(x => x.AnnouncementId == entityId)
                .Select(x => x.Path)
                .ToListAsync();
        }
        catch (Exception e)
        {
            return new List<string>();
        }
    }

    public async Task<string> GetEntityPreviewImagePath(Guid entityId)
    {
        try
        { 
            return _repository.GetAll().FirstOrDefaultAsync(x => x.AnnouncementId == entityId).Result.Path;
        }
        catch (Exception e)
        {
            return String.Empty;
        }
    }

    public async Task<BaseResult<int>> DeleteImagesByLinkedEntityId(Guid entityId)
    {
        try
        {
            var images = await _repository.GetAll()
                .Where(x => x.AnnouncementId == entityId)
                .Select(x => _mapper.Map<ImageDto>(x))
                .ToArrayAsync();

            foreach (var image in images)
                await DeleteImageById(image.Id);

            return new BaseResult<int>()
            {
                Data = images.Length
            };
        }
        catch (Exception e)
        {
            return new BaseResult<int>()
            {
                ErrorMassage = ErrorMessage.InternalServerError,
                ErrorCode = (int)ErrorCode.InternalServerError
            };
        }
    }

    public async Task<CollectionResult<ImageDto>> UploadImagesAsync(IFormFileCollection files, Guid entityId, string fileFolder)
    {
        try
        {
            List<string> PermittedFileTypes = new List<string> {
                "image/jpeg",
                "image/png",
            };

            var result = new List<ImageDto>();

            foreach (var file in files)
            {
                if (PermittedFileTypes.Contains(file.ContentType))
                {
                    var fileGuid = Guid.NewGuid();
                    var filePath = $"{fileFolder}{fileGuid}.jpg";

                    if (File.Exists(filePath))
                        File.Delete(filePath);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    var imageDto = new ImageDto() { Id = fileGuid, AnnouncementId = entityId, Path = filePath };

                    var image = await _repository.CreateAsync(_mapper.Map<Image>(imageDto));

                    result.Add(_mapper.Map<ImageDto>(image));
                }
            }

            return new CollectionResult<ImageDto>()
            {
                Data = result
            };

        }
        catch (Exception e)
        {
            return new CollectionResult<ImageDto>()
            {
                ErrorMassage = ErrorMessage.InternalServerError,
                ErrorCode = (int)ErrorCode.InternalServerError
            };
        }
    }
}