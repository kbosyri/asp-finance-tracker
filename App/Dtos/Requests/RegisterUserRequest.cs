using System.ComponentModel.DataAnnotations;

namespace FinanceTracker.API.App.Dtos.Requests;

public record RegisterUserRequest(
    [Required] string UserName,
    [Required][EmailAddress] string Email,
    [Required] string DisplayName,
    [Required] string Password
);
