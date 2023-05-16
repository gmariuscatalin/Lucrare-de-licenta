using System;

namespace NewBank2.Models;

public partial class Transaction
{
    public Guid TransactionId { get; set; }

    public Guid? AccountId { get; set; }

    public string? CurrencyTr { get; set; }

    public decimal? AmountTr { get; set; }

    public string? UsernameTr { get; set; }

    public string? Category { get; set; }

    public DateTime? TransactionDate { get; set; }

    public virtual Account? Account { get; set; }
}
