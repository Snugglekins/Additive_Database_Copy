﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Additive_DB_Refresh.Models;

[Keyless]
[Index("IsDeleted", "ClientLocationDiscountKey", Name = "nci_wi_ClientLocationDiscountMultiplePromoCodes_49DC810B8D07CF94DD3349E286577307")]
[Index("ClientLocationDiscountKey", "RedeemDateTime", Name = "nci_wi_ClientLocationDiscountMultiplePromoCodes_60FD04184E772CEB6E0E033D2C66B6F6")]
public partial class ClientLocationDiscountMultiplePromoCode
{
    public long ClientLocationDiscountMultiplePromoCodeKey { get; set; }

    public long ClientLocationDiscountKey { get; set; }

    public int ClientLocationKey { get; set; }

    [Required]
    [StringLength(100)]
    public string MultiplePromoCode { get; set; }

    public DateTime? RedeemDateTime { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime RowAdded { get; set; }

    [ForeignKey("ClientLocationDiscountKey")]
    public virtual ClientLocationDiscount ClientLocationDiscountKeyNavigation { get; set; }

    [ForeignKey("ClientLocationKey")]
    public virtual ClientLocation ClientLocationKeyNavigation { get; set; }
}