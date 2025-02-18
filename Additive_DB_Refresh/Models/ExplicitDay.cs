﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Additive_DB_Refresh.Models;

[Index("ExplicitDate", "ClientLocationKey", Name = "ix_ExplicitDays")]
public partial class ExplicitDay
{
    [Key]
    public int ExplicitDayKey { get; set; }

    public int ClientLocationKey { get; set; }

    public DateOnly? ExplicitDate { get; set; }

    public string Notes { get; set; }

    public long? ClientLocationEntityKey { get; set; }

    public int? HolidayRuleKey { get; set; }

    [ForeignKey("ClientLocationEntityKey")]
    [InverseProperty("ExplicitDays")]
    public virtual ClientLocationEntity ClientLocationEntityKeyNavigation { get; set; }

    [ForeignKey("ClientLocationKey")]
    [InverseProperty("ExplicitDays")]
    public virtual ClientLocation ClientLocationKeyNavigation { get; set; }

    [ForeignKey("HolidayRuleKey")]
    [InverseProperty("ExplicitDays")]
    public virtual HolidayRule HolidayRuleKeyNavigation { get; set; }
}