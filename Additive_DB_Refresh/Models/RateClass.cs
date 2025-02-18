﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Additive_DB_Refresh.Models;

public partial class RateClass
{
    [Key]
    public int RateClassKey { get; set; }

    [Required]
    [StringLength(100)]
    public string RateClassName { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime RowAdded { get; set; }

    [InverseProperty("RateClassKeyNavigation")]
    public virtual ICollection<EntityHierarchyRate> EntityHierarchyRates { get; set; } = new List<EntityHierarchyRate>();
}