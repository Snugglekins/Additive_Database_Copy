﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Additive_DB_Refresh.Models;

[Table("DIN_ExperienceLevels", Schema = "History")]
public partial class History_DIN_ExperienceLevel
{
    [Key]
    public int ExperienceLevelHistoryKey { get; set; }

    public int ExperienceLevelKey { get; set; }

    public int ClientLocationKey { get; set; }

    [StringLength(250)]
    public string ExperienceLevelDesc { get; set; }

    public DateTime DateFrom { get; set; }

    public DateTime? DateTo { get; set; }

    public bool IsCurrent { get; set; }

    [ForeignKey("ClientLocationKey")]
    [InverseProperty("History_DIN_ExperienceLevels")]
    public virtual ClientLocation ClientLocationKeyNavigation { get; set; }

    [ForeignKey("ExperienceLevelKey")]
    [InverseProperty("History_DIN_ExperienceLevels")]
    public virtual DIN_ExperienceLevel ExperienceLevelKeyNavigation { get; set; }
}