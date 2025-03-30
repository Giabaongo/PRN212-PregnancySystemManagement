using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Payment
{
    public int Id { get; set; }

    public int MembershipId { get; set; }

    public decimal Amount { get; set; }

    public string PaymentDescription { get; set; } = null!;

    public string PaymentMethod { get; set; } = null!;

    public string? VnpayToken { get; set; }

    public string? VnpayTransactionNo { get; set; }

    public string? VnpayResponseCode { get; set; }

    public DateTime PaymentDate { get; set; }

    public string PaymentStatus { get; set; } = null!;

    public virtual Membership Membership { get; set; } = null!;
}
