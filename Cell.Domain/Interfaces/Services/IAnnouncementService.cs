using Cell.Domain.Dto.AnnouncementDto;
using Cell.Domain.Result;

namespace Cell.Domain.Interfaces.Services;
public interface IAnnouncementService
{
    Task<BaseResult<AnnouncementAnswerDto>> GetAnnouncementByIdAsync(Guid id);
    Task<BaseResult<AnnouncementDto>> CreateAnnouncementAsync(CreateAnnouncementDto dto);
    Task<BaseResult<AnnouncementDto>> DeleteAnnouncementByIdAsync(Guid id);
    Task<BaseResult<AnnouncementDto>> UpdateAnnouncementAsync(AnnouncementDto dto);
    Task<CollectionResult<AnnouncementAnswerDto>> GetUserAnnouncementsAsync(Guid userId);
    Task<CollectionResult<AnnouncementAnswerDto>> GetAllAnnouncementsAsync();
    Task<BaseResult<int>> DeleteUserAnnouncementsAsync(Guid userId);
}