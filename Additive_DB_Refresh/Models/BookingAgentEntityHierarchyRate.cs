﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Additive_DB_Refresh.Models;

[Index("BookingAgentKey", "EntityHierarchyKey", "EntityHierarchyRateKey", Name = "uq_BookingAgententityHierarchyRates", IsUnique = true)]
public partial class BookingAgentEntityHierarchyRate
{
    [Key]
    public long BookingAgentEntityHierarchyRateKey { get; set; }

    public long BookingAgentKey { get; set; }

    public long BookingAgentEntityHierarchyKey { get; set; }

    public long EntityHierarchyKey { get; set; }

    public long EntityHierarchyRateKey { get; set; }

    [Column(TypeName = "numeric(25, 7)")]
    public decimal Commission { get; set; }

    public bool Sellable { get; set; }

    [Column(TypeName = "money")]
    public decimal Price { get; set; }

    [Column(TypeName = "money")]
    public decimal? MaxPackageDiscount { get; set; }

    [Column(TypeName = "numeric(25, 7)")]
    public decimal TransportationCommission { get; set; }

    public bool ShowInPackageOnly { get; set; }

    public bool IsCommissionOnTaxedAmount { get; set; }

    public bool CollectsTransportCost { get; set; }

    [ForeignKey("BookingAgentEntityHierarchyKey")]
    [InverseProperty("BookingAgentEntityHierarchyRates")]
    public virtual BookingAgentEntityHierarchy BookingAgentEntityHierarchyKeyNavigation { get; set; }

    [ForeignKey("BookingAgentKey")]
    [InverseProperty("BookingAgentEntityHierarchyRates")]
    public virtual BookingAgent BookingAgentKeyNavigation { get; set; }

    [ForeignKey("EntityHierarchyKey")]
    [InverseProperty("BookingAgentEntityHierarchyRates")]
    public virtual EntityHierarchy EntityHierarchyKeyNavigation { get; set; }

    [ForeignKey("EntityHierarchyRateKey")]
    [InverseProperty("BookingAgentEntityHierarchyRates")]
    public virtual EntityHierarchyRate EntityHierarchyRateKeyNavigation { get; set; }

    [InverseProperty("BookingAgentEntityHierarchyRateKeyNavigation")]
    public virtual ICollection<History_BookingAgentEntityHierarchyRate> History_BookingAgentEntityHierarchyRates { get; set; } = new List<History_BookingAgentEntityHierarchyRate>();
}