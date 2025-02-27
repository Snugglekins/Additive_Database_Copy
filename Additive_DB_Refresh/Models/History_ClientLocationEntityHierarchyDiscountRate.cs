﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Additive_DB_Refresh.Models;

[Table("ClientLocationEntityHierarchyDiscountRates", Schema = "History")]
[Index("ClientLocationEntityHierarchyDiscountKey", "ClientLocationEntityHierarchyDiscountRateKey", Name = "ix_ClientLocationEntityHierarchyDiscountRatesHistory")]
[Index("IsCurrent", "ClientLocationEntityHierarchyDiscountRateKey", Name = "nci_wi_ClientLocationEntityHierarchyDiscountRates_FD32467239A3B1F6A8C72D642384C24C")]
public partial class History_ClientLocationEntityHierarchyDiscountRate
{
    [Key]
    public long ClientLocationEntityHierarchyDiscountRateHistoryKey { get; set; }

    public long ClientLocationEntityHierarchyDiscountRateKey { get; set; }

    public int ClientLocationEntityHierarchyDiscountKey { get; set; }

    public long EntityHierarchyRateKey { get; set; }

    [Column(TypeName = "numeric(12, 9)")]
    public decimal? Value { get; set; }

    public int? MaxParticipants { get; set; }

    public bool? IsActive { get; set; }

    public DateTime DateFrom { get; set; }

    public DateTime? DateTo { get; set; }

    public bool IsCurrent { get; set; }

    [Column("Required_EntityHierarchyRateKey")]
    public long? RequiredEntityHierarchyRateKey { get; set; }

    [Column(TypeName = "numeric(9, 6)")]
    public decimal? RequiredRateRatio { get; set; }

    [ForeignKey("ClientLocationEntityHierarchyDiscountKey")]
    [InverseProperty("History_ClientLocationEntityHierarchyDiscountRates")]
    public virtual ClientLocationEntityHierarchyDiscount ClientLocationEntityHierarchyDiscountKeyNavigation { get; set; }

    [ForeignKey("ClientLocationEntityHierarchyDiscountRateKey")]
    [InverseProperty("History_ClientLocationEntityHierarchyDiscountRates")]
    public virtual ClientLocationEntityHierarchyDiscountRate ClientLocationEntityHierarchyDiscountRateKeyNavigation { get; set; }

    [ForeignKey("EntityHierarchyRateKey")]
    [InverseProperty("History_ClientLocationEntityHierarchyDiscountRateEntityHierarchyRateKeyNavigations")]
    public virtual EntityHierarchyRate EntityHierarchyRateKeyNavigation { get; set; }

    [ForeignKey("RequiredEntityHierarchyRateKey")]
    [InverseProperty("History_ClientLocationEntityHierarchyDiscountRateRequiredEntityHierarchyRateKeyNavigations")]
    public virtual EntityHierarchyRate RequiredEntityHierarchyRateKeyNavigation { get; set; }
}