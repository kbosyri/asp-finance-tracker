namespace FinanceTracker.API.App.Dtos.Requests;

public record RegisterUserRequest(
    string Username,
    string? Email,
    string Password
);
