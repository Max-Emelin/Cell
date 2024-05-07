using Cell.Domain.Dto.AnnouncementDto;
using Cell.Domain.Interfaces.Services;
using Cell.Domain.Result;
using Microsoft.AspNetCore.Mvc;

namespace Cell.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnnouncementController : ControllerBase
{
    private readonly IAnnouncementService _announcementService;

    public AnnouncementController(IAnnouncementService announcementService)
    {
        _announcementService = announcementService;
    }

    /// <summary>
    /// Получение объявления по Id.
    /// </summary>
    /// <param name="id"> Id объявления. </param>
    /// <returns> Объявление. </returns>
    [Route("getAnnouncement")]
    [HttpGet]
    public async Task<ActionResult<BaseResult<AnnouncementAnswerDto>>> GetAnnouncementById(Guid id)
    {
        var response = await _announcementService.GetAnnouncementByIdAsync(id);

        if (response.IsSuccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }

    /// <summary>
    /// Обновление данных об объявлении.
    /// </summary>
    /// <param name="dto"> Обновленные данные. </param>
    /// <returns> Объявление с обновленными данными. </returns>
    [Route("updateAnnouncement")]
    [HttpPut]
    public async Task<ActionResult<BaseResult<AnnouncementDto>>> UpdateAnnouncement(AnnouncementDto dto)
    {
        var response = await _announcementService.UpdateAnnouncementAsync(dto);

        if (response.IsSuccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }

    /// <summary>
    /// Создание объявления.
    /// </summary>
    /// <param name="dto"> Объявление. </param>
    /// <returns> Объявление. </returns>
    [Route("createAnnouncement")]
    [HttpPost]
    public async Task<ActionResult<CollectionResult<AnnouncementDto>>> CreateAnnouncement(CreateAnnouncementDto dto)
    {
        var response = await _announcementService.CreateAnnouncementAsync(dto);

        if (response.IsSuccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }

    /// <summary>
    /// Удаление объявления.
    /// </summary>
    /// <param name="Id"> Id удаляемого объявления. </param>
    /// <returns> Удаляемое объявление. </returns>
    [HttpDelete("deleteAnnouncement")]
    public async Task<ActionResult<BaseResult<AnnouncementDto>>> DeleteAnnouncement(Guid id)
    {
        var response = await _announcementService.DeleteAnnouncementByIdAsync(id);

        if (response.IsSuccess)
        {
            return Ok(response);

        }

        return BadRequest(response);
    }

    /// <summary>
    /// Получение всех объявлений пользователя.
    /// </summary>
    /// <param name="userId"> Идентификатор пользователя. </param>
    /// <returns> Объявления пользователя. </returns>
    [Route("getUserAnnouncements")]
    [HttpGet]
    public async Task<ActionResult<BaseResult<AnnouncementAnswerDto>>> GetUserAnnouncements(Guid userId)
    {
        var response = await _announcementService.GetUserAnnouncementsAsync(userId);

        if (response.IsSuccess)
        {
            return Ok(response);

        }

        return BadRequest(response);
    }

    /// <summary>
    /// Получение всех объявлений.
    /// </summary>
    /// <returns> Все объявления. </returns>
    [Route("getAllAnnouncements")]
    [HttpGet]
    public async Task<ActionResult<BaseResult<AnnouncementAnswerDto>>> GetAllAnnouncements()
    {
        var response = await _announcementService.GetAllAnnouncementsAsync();

        if (response.IsSuccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }
}