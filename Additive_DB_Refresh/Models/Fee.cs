﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Additive_DB_Refresh.Models;

public partial class Fee
{
    [Key]
    public int FeeKey { get; set; }

    public int? ClientLocationKey { get; set; }

    public int FeeTypeKey { get; set; }

    public string FeeName { get; set; }

    [Column(TypeName = "numeric(9, 5)")]
    public decimal? FeeValue { get; set; }

    public bool IsTaxable { get; set; }

    public bool PerPerson { get; set; }

    public bool IsSalesTax { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DateDeleted { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime RowAdded { get; set; }

    public bool Global { get; set; }

    public int? PriorFeeKey { get; set; }

    public bool IgnoreTaxExempt { get; set; }

    public short FeeClassKey { get; set; }

    public long? OnlineTravelAgencyKey { get; set; }

    [ForeignKey("ClientLocationKey")]
    [InverseProperty("Fees")]
    public virtual ClientLocation ClientLocationKeyNavigation { get; set; }

    [InverseProperty("FeeKeyNavigation")]
    public virtual ICollection<FeeAssociation> FeeAssociations { get; set; } = new List<FeeAssociation>();

    [ForeignKey("FeeTypeKey")]
    [InverseProperty("Fees")]
    public virtual FeeType FeeTypeKeyNavigation { get; set; }

    [InverseProperty("PriorFeeKeyNavigation")]
    public virtual ICollection<Fee> InversePriorFeeKeyNavigation { get; set; } = new List<Fee>();

    [ForeignKey("OnlineTravelAgencyKey")]
    [InverseProperty("Fees")]
    public virtual OnlineTravelAgency OnlineTravelAgencyKeyNavigation { get; set; }

    [ForeignKey("PriorFeeKey")]
    [InverseProperty("InversePriorFeeKeyNavigation")]
    public virtual Fee PriorFeeKeyNavigation { get; set; }
}