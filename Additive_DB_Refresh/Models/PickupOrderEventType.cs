﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Additive_DB_Refresh.Models;

[Index("PickupOrderEventTypeDesc", Name = "uq_PickupOrderEventTypes_PickupOrderEventTypeDesc", IsUnique = true)]
public partial class PickupOrderEventType
{
    [Key]
    public byte PickupOrderEventTypeKey { get; set; }

    [Required]
    [StringLength(50)]
    public string PickupOrderEventTypeDesc { get; set; }
}