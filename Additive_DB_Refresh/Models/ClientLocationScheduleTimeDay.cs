﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Additive_DB_Refresh.Models;

[PrimaryKey("ClientLocationScheduleTimeKey", "ClientLocationScheduleTimeDayKey")]
[Index("ClientLocationScheduleTimeKey", "DaysOfTheWeekKey", "EffectiveDate", Name = "IX_ClientLocationScheduleTimeDays_DaysOfTheWeekKey")]
[Index("ClientLocationScheduleTimeKey", "DaysOfTheWeekKey", Name = "ix1ClientLocationScheduleTimeDays")]
[Index("EffectiveDate", Name = "ix2ClientLocationScheduleTimeDays")]
[Index("PickupRouteKey", Name = "ix3ClientLocationScheduleTimeDays")]
[Index("DaysOfTheWeekKey", Name = "ix4ClientLocationScheduleTimeDays")]
[Index("DaysOfTheWeekKey", Name = "ix5ClientLocationScheduleTimeDays")]
[Index("DaysOfTheWeekKey", "PickupRouteKey", Name = "ix6ClientLocationScheduleTimeDays")]
[Index("ClientLocationScheduleTimeDayKey", Name = "ix_ClientLocationScheduleTimeDays")]
public partial class ClientLocationScheduleTimeDay
{
    [Key]
    public int ClientLocationScheduleTimeDayKey { get; set; }

    [Key]
    public int ClientLocationScheduleTimeKey { get; set; }

    public byte DaysOfTheWeekKey { get; set; }

    public int? PickupRouteKey { get; set; }

    public DateOnly? EffectiveDate { get; set; }

    public short? MaxPeople { get; set; }

    public bool HideFromCustomer { get; set; }

    [StringLength(250)]
    public string Description { get; set; }

    public bool HasLimitations { get; set; }

    public int? ClientLocationScheduleLimitationKey { get; set; }

    public int? ClientLocationScheduleWhenKey { get; set; }

    public short? HideMinutesBeforeStart { get; set; }

    public bool IsClosed { get; set; }

    public bool HideAfterFirstBooking { get; set; }

    public int? HideMinutesBeforeStartIfBooked { get; set; }

    public bool ReleaseReservedSpaces { get; set; }

    [ForeignKey("ClientLocationScheduleLimitationKey")]
    [InverseProperty("ClientLocationScheduleTimeDays")]
    public virtual ClientLocationScheduleLimitation ClientLocationScheduleLimitationKeyNavigation { get; set; }

    [InverseProperty("ClientLocationScheduleTimeDay")]
    public virtual ICollection<ClientLocationScheduleTimeDayOption> ClientLocationScheduleTimeDayOptions { get; set; } = new List<ClientLocationScheduleTimeDayOption>();

    [InverseProperty("ClientLocationScheduleTimeDay")]
    public virtual ICollection<ClientLocationScheduleTimeDayRateResource> ClientLocationScheduleTimeDayRateResources { get; set; } = new List<ClientLocationScheduleTimeDayRateResource>();

    [InverseProperty("ClientLocationScheduleTimeDay")]
    public virtual ICollection<ClientLocationScheduleTimeDayRate> ClientLocationScheduleTimeDayRates { get; set; } = new List<ClientLocationScheduleTimeDayRate>();

    [InverseProperty("ClientLocationScheduleTimeDay")]
    public virtual ICollection<ClientLocationScheduleTimeDayResource> ClientLocationScheduleTimeDayResources { get; set; } = new List<ClientLocationScheduleTimeDayResource>();

    [ForeignKey("ClientLocationScheduleWhenKey")]
    [InverseProperty("ClientLocationScheduleTimeDays")]
    public virtual ClientLocationScheduleWhen ClientLocationScheduleWhenKeyNavigation { get; set; }

    [ForeignKey("DaysOfTheWeekKey")]
    [InverseProperty("ClientLocationScheduleTimeDays")]
    public virtual DaysOfTheWeek DaysOfTheWeekKeyNavigation { get; set; }

    [ForeignKey("PickupRouteKey")]
    [InverseProperty("ClientLocationScheduleTimeDays")]
    public virtual PickupRoute PickupRouteKeyNavigation { get; set; }
}