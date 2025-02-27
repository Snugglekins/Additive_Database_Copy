﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Additive_DB_Refresh.Models;

[Table("Colors", Schema = "System")]
public partial class System_Color
{
    [Key]
    public int ColorKey { get; set; }

    [Required]
    [StringLength(255)]
    [Unicode(false)]
    public string Description { get; set; }

    [Column("CSSColorCode")]
    [StringLength(12)]
    [Unicode(false)]
    public string CsscolorCode { get; set; }
}