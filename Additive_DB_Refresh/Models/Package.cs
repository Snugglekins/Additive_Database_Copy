﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Additive_DB_Refresh.Models;

public partial class Package
{
    [Key]
    public Guid PackageKey { get; set; }

    public int ClientLocationKey { get; set; }

    [Required]
    [StringLength(200)]
    [Unicode(false)]
    public string Name { get; set; }

    [StringLength(2000)]
    [Unicode(false)]
    public string Description { get; set; }

    public bool IsActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DateDeleted { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string PhotoVersion { get; set; }

    public short SortIndex { get; set; }

    [Column(TypeName = "money")]
    public decimal? SlashThroughPrice { get; set; }

    public bool IsSequential { get; set; }

    [Column(TypeName = "money")]
    public decimal? ChildSlashThroughPrice { get; set; }

    public bool HideFromCustomer { get; set; }

    [StringLength(250)]
    public string DisplayPrice { get; set; }

    public bool OverrideAutoPrice { get; set; }

    public bool IgnoreDynamicPricing { get; set; }

    public bool HideRateDiscountFromCustomer { get; set; }

    [StringLength(250)]
    public string DisplayDiscount { get; set; }

    public bool ShowFirstActivityOnly { get; set; }

    public Guid? RedeamProductId { get; set; }

    public short? ReleaseReservedSpaceMins { get; set; }

    public bool RequireMatchingRates { get; set; }

    public byte PackageTypeKey { get; set; }

    public bool HideAddOns { get; set; }

    [StringLength(250)]
    public string KioskDisplayPrice { get; set; }

    [InverseProperty("PackageKeyNavigation")]
    public virtual ICollection<BookingAgentPackage> BookingAgentPackages { get; set; } = new List<BookingAgentPackage>();

    [ForeignKey("ClientLocationKey")]
    [InverseProperty("Packages")]
    public virtual ClientLocation ClientLocationKeyNavigation { get; set; }

    [InverseProperty("PackageKeyNavigation")]
    public virtual ICollection<PackageDetailGroup> PackageDetailGroups { get; set; } = new List<PackageDetailGroup>();

    [InverseProperty("PackageKeyNavigation")]
    public virtual ICollection<PackageDetail> PackageDetails { get; set; } = new List<PackageDetail>();

    [InverseProperty("PackageKeyNavigation")]
    public virtual ICollection<PackageGroup> PackageGroups { get; set; } = new List<PackageGroup>();

    [ForeignKey("PackageTypeKey")]
    [InverseProperty("Packages")]
    public virtual PackageType PackageTypeKeyNavigation { get; set; }
}