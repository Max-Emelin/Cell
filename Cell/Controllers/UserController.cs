using Cell.Domain.Dto.UserDto;
using Cell.Domain.Interfaces.Services;
using Cell.Domain.Result;
using Microsoft.AspNetCore.Mvc;

namespace Cell.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// Получение пользователя по Id.
    /// </summary>
    /// <param name="id"> Id пользователя. </param>
    /// <returns> Пользователь. </returns>
    [Route("getUser")]
    [HttpGet]
    public async Task<ActionResult<BaseResult<UserDto>>> GetUserById(Guid id)
    {
        var response = await _userService.GetUserByIdAsync(id);

        if (response.IsSuccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }

    /// <summary>
    /// Обновление данных о пользователе.
    /// </summary>
    /// <param name="dto"> Обновленные данные. </param>
    /// <returns> Пользователь с обновленными данными. </returns>
    [Route("updateUser")]
    [HttpPut]
    public async Task<ActionResult<BaseResult<UserDto>>> UpdateUser(UserDto dto)
    {
        var response = await _userService.UpdateUserAsync(dto);

        if (response.IsSuccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }

    /// <summary>
    /// Удаление пользователя.
    /// </summary>
    /// <param name="Id"> Id удаляемого пользователя. </param>
    /// <returns> Удаляемый пользователь. </returns>
    [HttpDelete("deleteUser")]
    public async Task<ActionResult<BaseResult<UserDto>>> DeleteUser(Guid id)
    {
        var response = await _userService.DeleteUserByIdAsync(id);

        if (response.IsSuccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }

    /// <summary>
    /// Авторизация пользователя.
    /// </summary>
    /// <param name="dto"> Введенные данные. </param>
    /// <returns> Идентификатор пользователя. </returns>
    [Route("loginUser")]
    [HttpPost]
    public async Task<ActionResult<BaseResult<Guid>>> LoginUser(LoginUserDto dto)
    {
        var response = await _userService.LoginUser(dto);

        if (response.IsSuccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }

    /// <summary>
    /// Создание пользователя.
    /// </summary>
    /// <param name="dto"> Пользователь. </param>
    /// <returns> Пользователь. </returns>
    [Route("registerUser")]
    [HttpPost]
    public async Task<ActionResult<CollectionResult<UserDto>>> RegisterUser(RegisterUserDto dto)
    {
        var response = await _userService.CreateUserAsync(dto);

        if (response.IsSuccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }
}