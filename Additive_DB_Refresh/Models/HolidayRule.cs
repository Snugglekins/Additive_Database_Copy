﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Additive_DB_Refresh.Models;

public partial class HolidayRule
{
    [Key]
    public int HolidayRuleKey { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? EffectiveDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ExpirationDate { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Holiday { get; set; }

    public byte? Month { get; set; }

    public byte? Day { get; set; }

    public byte? WeekDay { get; set; }

    public byte? Week { get; set; }

    public bool? IsLast { get; set; }

    public bool? IsFirst { get; set; }

    public bool? BeforeWeekend { get; set; }

    public bool? AfterWeekend { get; set; }

    public byte? IncrementCount { get; set; }

    public bool IsCompanyHoliday { get; set; }

    public byte RuleType { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string FunctionCall { get; set; }

    [InverseProperty("HolidayRuleKeyNavigation")]
    public virtual ICollection<ExplicitDay> ExplicitDays { get; set; } = new List<ExplicitDay>();
}