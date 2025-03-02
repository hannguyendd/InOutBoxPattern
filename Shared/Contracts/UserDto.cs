namespace Shared.Contracts;

public record SaveUserRequest(string Name, string Email, DateTime DoB);

public record UserResponse(Guid Id, string Name, string Email, DateTime DoB);
