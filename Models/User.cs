using System;
using System.Collections.Generic;

namespace NewBank2.Models;

public partial class User
{
    public Guid Id { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? ProfilePicture { get; set; }

    public string? VerificationCode { get; set; }

    public DateTime? VerificationCodeExpiration { get; set; }

    public virtual ICollection<Account> Accounts { get; } = new List<Account>();
}
