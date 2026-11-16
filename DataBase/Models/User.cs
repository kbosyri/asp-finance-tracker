using System;
using Microsoft.AspNetCore.Identity;

namespace FinanceTracker.API.DataBase.Models;

public class User : IdentityUser<int>
{

    public string Username { get; set; } = string.Empty;

    public string? DisplayName { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
