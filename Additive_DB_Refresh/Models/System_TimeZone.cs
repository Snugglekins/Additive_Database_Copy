﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Additive_DB_Refresh.Models;

[Table("TimeZones", Schema = "System")]
public partial class System_TimeZone
{
    [Key]
    public short TimeZoneKey { get; set; }

    [Required]
    [StringLength(12)]
    public string Abbreviation { get; set; }

    [Required]
    [StringLength(200)]
    public string FullName { get; set; }

    [Required]
    [StringLength(200)]
    public string Location { get; set; }

    [Column("UTCOffset", TypeName = "numeric(5, 1)")]
    public decimal Utcoffset { get; set; }
}