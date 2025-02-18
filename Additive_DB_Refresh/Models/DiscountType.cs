﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Additive_DB_Refresh.Models;

public partial class DiscountType
{
    [Key]
    public short DiscountTypeKey { get; set; }

    [Required]
    [StringLength(50)]
    public string Description { get; set; }

    [InverseProperty("DiscountTypeKeyNavigation")]
    public virtual ICollection<ClientLocationDiscount> ClientLocationDiscounts { get; set; } = new List<ClientLocationDiscount>();

    [InverseProperty("DiscountTypeKeyNavigation")]
    public virtual ICollection<ClientLocationEntityHierarchyDiscount> ClientLocationEntityHierarchyDiscounts { get; set; } = new List<ClientLocationEntityHierarchyDiscount>();

    [InverseProperty("DiscountTypeKeyNavigation")]
    public virtual ICollection<History_ClientLocationEntityHierarchyDiscount> History_ClientLocationEntityHierarchyDiscounts { get; set; } = new List<History_ClientLocationEntityHierarchyDiscount>();
}