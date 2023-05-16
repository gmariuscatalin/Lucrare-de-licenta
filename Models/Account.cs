using System;
using System.Collections.Generic;

namespace NewBank2.Models;

public partial class Account
{
    public Guid AccountId { get; set; }

    public string? Username { get; set; }

    public decimal? Balance { get; set; }

    public string? Currency { get; set; }

    public string? Status { get; set; }

    public DateTime? DateOpened { get; set; }

    public DateTime? LastTransactionDate { get; set; }

    public virtual ICollection<Transaction> Transactions { get; } = new List<Transaction>();

    public virtual User? UsernameNavigation { get; set; }
}
