﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Additive_DB_Refresh.Models;

public partial class FeeType
{
    [Key]
    public int FeeTypeKey { get; set; }

    [Required]
    public string FeeName { get; set; }

    [InverseProperty("FeeTypeKeyNavigation")]
    public virtual ICollection<Fee> Fees { get; set; } = new List<Fee>();
}