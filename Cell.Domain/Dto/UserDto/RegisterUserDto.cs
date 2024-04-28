namespace Cell.Domain.Dto.UserDto;

public record RegisterUserDto
(
    string Name,
    string LastName,
    string? Email,
    string PhoneNumber,
    string? Address,
    string Login,
    string Password
);
