﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Additive_DB_Refresh.Models;

[Table("EventTypes", Schema = "CashDrawer")]
[Index("Description", Name = "ix2EventTyes")]
public partial class CashDrawer_EventType
{
    [Key]
    public short EventTypeKey { get; set; }

    [Required]
    [StringLength(100)]
    [Unicode(false)]
    public string Description { get; set; }

    public bool? IncludeInRegisterSummary { get; set; }
}