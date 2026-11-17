namespace FinanceTracker.API.App.Dtos.Requests;

public record RegisterUserRequest(
    string UserName,
    string? Email,
    string DisplayName,
    string Password
);
