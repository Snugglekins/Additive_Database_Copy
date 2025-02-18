﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Additive_DB_Refresh.Models;

[Table("ProductRentalItems", Schema = "Merchandise")]
public partial class Merchandise_ProductRentalItem
{
    [Key]
    public int ProductRentalItemKey { get; set; }

    public int ProductKey { get; set; }

    public int ClientLocationKey { get; set; }

    public bool IsActive { get; set; }

    public bool IsRented { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime RowAdded { get; set; }

    [Required]
    [Column("SKU")]
    [StringLength(50)]
    [Unicode(false)]
    public string Sku { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DateDeleted { get; set; }

    [ForeignKey("ClientLocationKey")]
    [InverseProperty("Merchandise_ProductRentalItems")]
    public virtual ClientLocation ClientLocationKeyNavigation { get; set; }

    [InverseProperty("ProductRentalItemKeyNavigation")]
    public virtual ICollection<Merchandise_ProductRentalItemNote> Merchandise_ProductRentalItemNotes { get; set; } = new List<Merchandise_ProductRentalItemNote>();

    [ForeignKey("ProductKey")]
    [InverseProperty("Merchandise_ProductRentalItems")]
    public virtual Merchandise_Product ProductKeyNavigation { get; set; }
}