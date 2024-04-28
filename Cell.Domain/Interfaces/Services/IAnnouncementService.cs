using Cell.Domain.Dto.AnnouncementDto;
using Cell.Domain.Result;

namespace Cell.Domain.Interfaces.Services;
public interface IAnnouncementService
{
    Task<BaseResult<AnnouncementDto>> GetAnnouncementByIdAsync(Guid id);
    Task<BaseResult<AnnouncementDto>> CreateAnnouncementAsync(AnnouncementDto dto);
    Task<BaseResult<AnnouncementDto>> DeleteAnnouncementByIdAsync(Guid id);
    Task<BaseResult<AnnouncementDto>> UpdateAnnouncementAsync(AnnouncementDto dto);
    Task<CollectionResult<AnnouncementDto>> GetUserAnnouncementsAsync(Guid userId);
    Task<CollectionResult<AnnouncementDto>> GetAllAnnouncementsAsync();
}