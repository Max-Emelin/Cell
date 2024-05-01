using AutoMapper;
using Cell.Application.Resources;
using Cell.Domain.Dto.AnnouncementDto;
using Cell.Domain.Entities;
using Cell.Domain.Enum;
using Cell.Domain.Interfaces;
using Cell.Domain.Interfaces.Services;
using Cell.Domain.Result;
using Microsoft.EntityFrameworkCore;

namespace Trashcan.Application.Services;

public class AnnouncementService : IAnnouncementService
{
    private readonly IBaseRepository<Announcement> _repository;
    private readonly IImageService _imageService;
    private readonly IMapper _mapper;

    public AnnouncementService(IBaseRepository<Announcement> repository, IImageService imageService, IMapper mapper)
    {
        _repository = repository;
        _imageService = imageService;
        _mapper = mapper;
    }

    public async Task<BaseResult<AnnouncementAnswerDto>> GetAnnouncementByIdAsync(Guid id)
    {
        try
        {
            var announcement = await _repository.GetAll()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (announcement == null)
            {
                return new BaseResult<AnnouncementAnswerDto>()
                {
                    ErrorMassage = ErrorMessage.DataNotFount,
                    ErrorCode = (int)ErrorCode.DataNotFount
                };
            }

            var announcementAnswer = _mapper.Map<AnnouncementAnswerDto>(announcement);

            announcementAnswer.ImagePaths = await _imageService.GetEntityImagePaths(id);

            return new BaseResult<AnnouncementAnswerDto>()
            {
                Data = announcementAnswer
            };
        }
        catch (Exception e)
        {
            return new BaseResult<AnnouncementAnswerDto>()
            {
                ErrorMassage = ErrorMessage.InternalServerError,
                ErrorCode = (int)ErrorCode.InternalServerError
            };
        }
    }

    public async Task<BaseResult<AnnouncementDto>> CreateAnnouncementAsync(CreateAnnouncementDto dto)
    {
        try
        {
            if (dto == null)
            {
                return new BaseResult<AnnouncementDto>()
                {
                    ErrorMassage = ErrorMessage.DataNotFount,
                    ErrorCode = (int)ErrorCode.DataNotFount
                };
            }

            await _repository.CreateAsync(_mapper.Map<Announcement>(dto));

            return new BaseResult<AnnouncementDto>()
            {
                Data = _mapper.Map<AnnouncementDto>(
                    _repository.GetAll()
                        .OrderBy(x => x.Created)
                        .Last()
                    )
            };

        }
        catch (Exception e)
        {
            return new BaseResult<AnnouncementDto>()
            {
                ErrorMassage = ErrorMessage.InternalServerError,
                ErrorCode = (int)ErrorCode.InternalServerError
            };
        }
    }

    public async Task<BaseResult<AnnouncementDto>> DeleteAnnouncementByIdAsync(Guid id)
    {
        try
        {
            var announcement = await _repository.GetAll()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (announcement == null)
            {
                return new BaseResult<AnnouncementDto>()
                {
                    ErrorMassage = ErrorMessage.DataNotFount,
                    ErrorCode = (int)ErrorCode.DataNotFount
                };
            }

            await _imageService.DeleteImagesByLinkedEntityId(id);
            await _repository.RemoveAsync(announcement);

            return new BaseResult<AnnouncementDto>()
            {
                Data = _mapper.Map<AnnouncementDto>(announcement)
            };
        }
        catch (Exception e)
        {
            return new BaseResult<AnnouncementDto>()
            {
                ErrorMassage = ErrorMessage.InternalServerError,
                ErrorCode = (int)ErrorCode.InternalServerError
            };
        }
    }

    public async Task<BaseResult<int>> DeleteUserAnnouncementsAsync(Guid userId)
    {
        try
        {
            var announcements = await _repository.GetAll()
              .Where(x => x.UserId == userId)
              .ToArrayAsync();

            foreach (var announcement in announcements)
                await DeleteAnnouncementByIdAsync(announcement.Id);

            return new BaseResult<int>()
            {
                Data = announcements.Count()
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

    public async Task<BaseResult<AnnouncementDto>> UpdateAnnouncementAsync(AnnouncementDto dto)
    {
        try
        {
            await _repository.UpdateAsync(_mapper.Map<Announcement>(dto));

            return new BaseResult<AnnouncementDto>()
            {
                Data = dto
            };
        }
        catch (Exception e)
        {
            return new BaseResult<AnnouncementDto>()
            {
                ErrorMassage = ErrorMessage.InternalServerError,
                ErrorCode = (int)ErrorCode.InternalServerError
            };
        }
    }

    public async Task<CollectionResult<AnnouncementAnswerDto>> GetUserAnnouncementsAsync(Guid userId)
    {
        try
        {
            var announcements = await _repository.GetAll()
                .Where(x => x.UserId == userId)
                .ToArrayAsync();

            if (!announcements.Any())
            {
                return new CollectionResult<AnnouncementAnswerDto>()
                {
                    ErrorMassage = ErrorMessage.DataNotFount,
                    ErrorCode = (int)ErrorCode.DataNotFount
                };
            }

            var ans = new List<AnnouncementAnswerDto>();

            foreach (var announcement in announcements)
            {
                var announcementAnswer = _mapper.Map<AnnouncementAnswerDto>(announcement);

                announcementAnswer.ImagePaths = new List<string>()
                    { await _imageService.GetEntityPreviewImagePath(announcement.Id) };

                ans.Add(announcementAnswer);
            }

            return new CollectionResult<AnnouncementAnswerDto>()
            {
                Data = ans,
                Count = ans.Count
            };
        }
        catch (Exception e)
        {
            return new CollectionResult<AnnouncementAnswerDto>()
            {
                ErrorMassage = ErrorMessage.InternalServerError,
                ErrorCode = (int)ErrorCode.InternalServerError
            };
        }
    }

    public async Task<CollectionResult<AnnouncementAnswerDto>> GetAllAnnouncementsAsync()
    {
        try
        {
            var announcements = await _repository.GetAll()
                .ToArrayAsync();

            if (!announcements.Any())
            {
                return new CollectionResult<AnnouncementAnswerDto>()
                {
                    ErrorMassage = ErrorMessage.DataNotFount,
                    ErrorCode = (int)ErrorCode.DataNotFount
                };
            }

            var ans = new List<AnnouncementAnswerDto>();

            foreach (var announcement in announcements)
            {
                var announcementAnswer = _mapper.Map<AnnouncementAnswerDto>(announcement);

                announcementAnswer.ImagePaths = new List<string>()
                    { await _imageService.GetEntityPreviewImagePath(announcement.Id) };

                ans.Add(announcementAnswer);
            }

            return new CollectionResult<AnnouncementAnswerDto>()
            {
                Data = ans,
                Count = ans.Count
            };
        }
        catch (Exception e)
        {
            return new CollectionResult<AnnouncementAnswerDto>()
            {
                ErrorMassage = ErrorMessage.InternalServerError,
                ErrorCode = (int)ErrorCode.InternalServerError
            };
        }
    }
}