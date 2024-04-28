namespace Cell.Domain.Dto.UserDto;

public record UserDto
(
    Guid Id,
    string Name,
    string LastName,
    string? Email,
    string PhoneNumber,
    string? Address,
    DateTime Created
);