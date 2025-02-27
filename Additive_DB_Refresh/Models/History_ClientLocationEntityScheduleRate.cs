﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Additive_DB_Refresh.Models;

[Table("ClientLocationEntityScheduleRates", Schema = "History")]
public partial class History_ClientLocationEntityScheduleRate
{
    [Key]
    public long ClientLocationEntityScheduleRatesHistoryKey { get; set; }

    public long ClientLocationEntityScheduleRateKey { get; set; }

    public long ClientLocationEntityScheduleKey { get; set; }

    public long EntityHierarchyRateKey { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal? Price { get; set; }

    public DateTime? DateFrom { get; set; }

    public DateTime? DateTo { get; set; }

    public bool IsCurrent { get; set; }

    public short? MaxParticipantsPerTime { get; set; }

    [ForeignKey("ClientLocationEntityScheduleKey")]
    [InverseProperty("History_ClientLocationEntityScheduleRates")]
    public virtual ClientLocationEntitySchedule ClientLocationEntityScheduleKeyNavigation { get; set; }

    [ForeignKey("ClientLocationEntityScheduleRateKey")]
    [InverseProperty("History_ClientLocationEntityScheduleRates")]
    public virtual ClientLocationEntityScheduleRate ClientLocationEntityScheduleRateKeyNavigation { get; set; }

    [ForeignKey("EntityHierarchyRateKey")]
    [InverseProperty("History_ClientLocationEntityScheduleRates")]
    public virtual EntityHierarchyRate EntityHierarchyRateKeyNavigation { get; set; }
}