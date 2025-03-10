﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Additive_DB_Refresh.Models;

public partial class PackageDetailGroup
{
    [Key]
    public Guid PackageDetailGroupKey { get; set; }

    public Guid PackageKey { get; set; }

    public Guid PackageDetailKey { get; set; }

    public Guid PackageGroupKey { get; set; }

    public TimeOnly StartTime { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime RowAdded { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DateDeleted { get; set; }

    [ForeignKey("PackageDetailKey")]
    [InverseProperty("PackageDetailGroups")]
    public virtual PackageDetail PackageDetailKeyNavigation { get; set; }

    [ForeignKey("PackageGroupKey")]
    [InverseProperty("PackageDetailGroups")]
    public virtual PackageGroup PackageGroupKeyNavigation { get; set; }

    [ForeignKey("PackageKey")]
    [InverseProperty("PackageDetailGroups")]
    public virtual Package PackageKeyNavigation { get; set; }
}