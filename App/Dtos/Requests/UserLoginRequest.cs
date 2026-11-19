using System.ComponentModel.DataAnnotations;

namespace FinanceTracker.API.App.Dtos.Requests;

public record UserLoginRequest(
  [Required] string UserName,
  [Required] string Password  
);
