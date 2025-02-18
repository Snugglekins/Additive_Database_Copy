﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Additive_DB_Refresh.Models;

public partial class PackageDetail
{
    [Key]
    public Guid PackageDetailKey { get; set; }

    public Guid PackageKey { get; set; }

    public long? EntityHierarchyKey { get; set; }

    public long? PhotoPackageKey { get; set; }

    public int? ProductKey { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DateDeleted { get; set; }

    [Column(TypeName = "money")]
    public decimal? AdultDiscount { get; set; }

    [Column(TypeName = "money")]
    public decimal? ChildDiscount { get; set; }

    [Column(TypeName = "money")]
    public decimal? AdultNoInventoryDiscount { get; set; }

    [Column(TypeName = "money")]
    public decimal? ChildNoInventoryDiscount { get; set; }

    public short? SequenceInPackage { get; set; }

    public TimeOnly? MinimumTimeToNextActivity { get; set; }

    [ForeignKey("EntityHierarchyKey")]
    [InverseProperty("PackageDetails")]
    public virtual EntityHierarchy EntityHierarchyKeyNavigation { get; set; }

    [InverseProperty("PackageDetailKeyNavigation")]
    public virtual ICollection<PackageDetailGroup> PackageDetailGroups { get; set; } = new List<PackageDetailGroup>();

    [InverseProperty("PackageDetailKeyNavigation")]
    public virtual ICollection<PackageDetailRate> PackageDetailRates { get; set; } = new List<PackageDetailRate>();

    [ForeignKey("PackageKey")]
    [InverseProperty("PackageDetails")]
    public virtual Package PackageKeyNavigation { get; set; }

    [ForeignKey("PhotoPackageKey")]
    [InverseProperty("PackageDetails")]
    public virtual PhotoPackage PhotoPackageKeyNavigation { get; set; }

    [ForeignKey("ProductKey")]
    [InverseProperty("PackageDetails")]
    public virtual Merchandise_Product ProductKeyNavigation { get; set; }
}