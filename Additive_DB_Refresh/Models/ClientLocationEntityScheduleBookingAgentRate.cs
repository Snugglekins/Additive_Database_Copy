﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Additive_DB_Refresh.Models;

[PrimaryKey("ClientLocationEntityScheduleKey", "BookingAgentKey", "EntityHierarchyRateKey")]
public partial class ClientLocationEntityScheduleBookingAgentRate
{
    [Key]
    public long ClientLocationEntityScheduleKey { get; set; }

    [Key]
    public long BookingAgentKey { get; set; }

    [Key]
    public long EntityHierarchyRateKey { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal Price { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime RowAdded { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DateDeleted { get; set; }

    [ForeignKey("ClientLocationEntityScheduleKey, BookingAgentKey")]
    [InverseProperty("ClientLocationEntityScheduleBookingAgentRates")]
    public virtual ClientLocationEntityScheduleBookingAgent ClientLocationEntityScheduleBookingAgent { get; set; }

    [InverseProperty("ClientLocationEntityScheduleBookingAgentRate")]
    public virtual ICollection<ClientLocationScheduleDayBookingAgentRate> ClientLocationScheduleDayBookingAgentRates { get; set; } = new List<ClientLocationScheduleDayBookingAgentRate>();
}