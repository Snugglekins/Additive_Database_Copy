﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Additive_DB_Refresh.Models;

public partial class ClientLocationEntityHierarchyDiscount
{
    [Key]
    public int ClientLocationEntityHierarchyDiscountKey { get; set; }

    public long EntityHierarchyKey { get; set; }

    public long ClientLocationDiscountKey { get; set; }

    public short? DiscountTypeKey { get; set; }

    [Column(TypeName = "numeric(30, 5)")]
    public decimal? Value { get; set; }

    public bool ApplyOnce { get; set; }

    public DateTime? Deleted { get; set; }

    [ForeignKey("ClientLocationDiscountKey")]
    [InverseProperty("ClientLocationEntityHierarchyDiscounts")]
    public virtual ClientLocationDiscount ClientLocationDiscountKeyNavigation { get; set; }

    [InverseProperty("ClientLocationEntityHierarchyDiscountKeyNavigation")]
    public virtual ICollection<ClientLocationEntityHierarchyDiscountRate> ClientLocationEntityHierarchyDiscountRates { get; set; } = new List<ClientLocationEntityHierarchyDiscountRate>();

    [ForeignKey("DiscountTypeKey")]
    [InverseProperty("ClientLocationEntityHierarchyDiscounts")]
    public virtual DiscountType DiscountTypeKeyNavigation { get; set; }

    [ForeignKey("EntityHierarchyKey")]
    [InverseProperty("ClientLocationEntityHierarchyDiscounts")]
    public virtual EntityHierarchy EntityHierarchyKeyNavigation { get; set; }

    [InverseProperty("ClientLocationEntityHierarchyDiscountKeyNavigation")]
    public virtual ICollection<History_ClientLocationEntityHierarchyDiscountRate> History_ClientLocationEntityHierarchyDiscountRates { get; set; } = new List<History_ClientLocationEntityHierarchyDiscountRate>();

    [InverseProperty("ClientLocationEntityHierarchyDiscountKeyNavigation")]
    public virtual ICollection<History_ClientLocationEntityHierarchyDiscount> History_ClientLocationEntityHierarchyDiscounts { get; set; } = new List<History_ClientLocationEntityHierarchyDiscount>();
}