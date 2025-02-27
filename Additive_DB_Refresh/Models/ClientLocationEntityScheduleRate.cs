﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Additive_DB_Refresh.Models;

[Index("ClientLocationEntityScheduleKey", "EntityHierarchyRateKey", Name = "IX_clesr", IsUnique = true)]
[Index("ClientLocationEntityScheduleKey", "EntityHierarchyRateKey", Name = "uq_ClientLocationEntityScheduleRates_ClientLocationEntityScheduleKey_EntityHierarchyRateKey", IsUnique = true)]
public partial class ClientLocationEntityScheduleRate
{
    [Key]
    public long ClientLocationEntityScheduleRateKey { get; set; }

    public long ClientLocationEntityScheduleKey { get; set; }

    public long EntityHierarchyRateKey { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal? Price { get; set; }

    public DateTime? RowAdded { get; set; }

    public string Notes { get; set; }

    public bool UseDefault { get; set; }

    public bool IsHidden { get; set; }

    public short? MaxParticipantsPerTime { get; set; }

    [ForeignKey("ClientLocationEntityScheduleKey")]
    [InverseProperty("ClientLocationEntityScheduleRates")]
    public virtual ClientLocationEntitySchedule ClientLocationEntityScheduleKeyNavigation { get; set; }

    [ForeignKey("EntityHierarchyRateKey")]
    [InverseProperty("ClientLocationEntityScheduleRates")]
    public virtual EntityHierarchyRate EntityHierarchyRateKeyNavigation { get; set; }

    [InverseProperty("ClientLocationEntityScheduleRateKeyNavigation")]
    public virtual ICollection<History_ClientLocationEntityScheduleRate> History_ClientLocationEntityScheduleRates { get; set; } = new List<History_ClientLocationEntityScheduleRate>();
}