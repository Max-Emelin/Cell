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
    private readonly IMapper _mapper;

    public AnnouncementService(IBaseRepository<Announcement> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<BaseResult<AnnouncementDto>> GetAnnouncementByIdAsync(Guid id)
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

    public async Task<BaseResult<AnnouncementDto>> CreateAnnouncementAsync(AnnouncementDto dto)
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
                Data = _mapper.Map<AnnouncementDto>(dto)
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

    public async Task<BaseResult<AnnouncementDto>> UpdateAnnouncementAsync(AnnouncementDto dto)
    {
        try
        {
            var announcement = await _repository.GetAll()
                .FirstOrDefaultAsync(x => x.Id == dto.Id);

            if (announcement == null)
            {
                return new BaseResult<AnnouncementDto>()
                {
                    ErrorMassage = ErrorMessage.DataNotFount,
                    ErrorCode = (int)ErrorCode.DataNotFount
                };
            }

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

    public async Task<CollectionResult<AnnouncementDto>> GetUserAnnouncementsAsync(Guid userId)
    {
        try
        {
            var announcements = await _repository.GetAll()
                .Select(x => _mapper.Map<AnnouncementDto>(x))
                .Where(x => x.UserId == userId)
                .ToArrayAsync();

            if (!announcements.Any())
            {
                return new CollectionResult<AnnouncementDto>()
                {
                    ErrorMassage = ErrorMessage.DataNotFount,
                    ErrorCode = (int)ErrorCode.DataNotFount
                };
            }

            return new CollectionResult<AnnouncementDto>()
            {
                Data = announcements,
                Count = announcements.Length
            };
        }
        catch (Exception e)
        {
            return new CollectionResult<AnnouncementDto>()
            {
                ErrorMassage = ErrorMessage.InternalServerError,
                ErrorCode = (int)ErrorCode.InternalServerError
            };
        }
    }

    public async Task<CollectionResult<AnnouncementDto>> GetAllAnnouncementsAsync()
    {
        try
        {
            var announcements = await _repository.GetAll()
                .Select(x => _mapper.Map<AnnouncementDto>(x))
                .ToArrayAsync();

            if (!announcements.Any())
            {
                return new CollectionResult<AnnouncementDto>()
                {
                    ErrorMassage = ErrorMessage.DataNotFount,
                    ErrorCode = (int)ErrorCode.DataNotFount
                };
            }

            return new CollectionResult<AnnouncementDto>()
            {
                Data = announcements,
                Count = announcements.Length
            };
        }
        catch (Exception e)
        {
            return new CollectionResult<AnnouncementDto>()
            {
                ErrorMassage = ErrorMessage.InternalServerError,
                ErrorCode = (int)ErrorCode.InternalServerError
            };
        }
    }
}