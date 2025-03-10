﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Additive_DB_Refresh.Models;

[Table("DaysOfTheWeek")]
public partial class DaysOfTheWeek
{
    [Key]
    public byte DaysOfTheWeekKey { get; set; }

    [Required]
    [StringLength(50)]
    public string Weekday { get; set; }

    [Required]
    [StringLength(12)]
    public string ShortName { get; set; }

    public bool IsOpen { get; set; }

    public string Notes { get; set; }

    [InverseProperty("DaysOfTheWeekKeyNavigation")]
    public virtual ICollection<ClientLocationDaysOfTheWeek> ClientLocationDaysOfTheWeeks { get; set; } = new List<ClientLocationDaysOfTheWeek>();

    [InverseProperty("DaysOfTheWeekKeyNavigation")]
    public virtual ICollection<ClientLocationEntityScheduleHour> ClientLocationEntityScheduleHours { get; set; } = new List<ClientLocationEntityScheduleHour>();

    [InverseProperty("DaysOfTheWeekKeyNavigation")]
    public virtual ICollection<ClientLocationScheduleDayBookingAgentRate> ClientLocationScheduleDayBookingAgentRates { get; set; } = new List<ClientLocationScheduleDayBookingAgentRate>();

    [InverseProperty("DaysOfTheWeekKeyNavigation")]
    public virtual ICollection<ClientLocationScheduleTimeDayBookingAgent> ClientLocationScheduleTimeDayBookingAgents { get; set; } = new List<ClientLocationScheduleTimeDayBookingAgent>();

    [InverseProperty("DaysOfTheWeekKeyNavigation")]
    public virtual ICollection<ClientLocationScheduleTimeDay> ClientLocationScheduleTimeDays { get; set; } = new List<ClientLocationScheduleTimeDay>();

    [InverseProperty("DaysOfTheWeekKeyNavigation")]
    public virtual ICollection<PickupRouteScheduleTimeDay> PickupRouteScheduleTimeDays { get; set; } = new List<PickupRouteScheduleTimeDay>();
}