﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Additive_DB_Refresh.Models;

[Index("ClientLocationEntityScheduleKey", "ClientLocationResourceKey", Name = "uq_ClientLocationEntityScheduleResources_Schedule_Resource", IsUnique = true)]
public partial class ClientLocationEntityScheduleResource
{
    [Key]
    public int ClientLocationEntityScheduleResourceKey { get; set; }

    public long ClientLocationEntityScheduleKey { get; set; }

    public int ClientLocationResourceKey { get; set; }

    public int? MaxQuantity { get; set; }

    public bool UseResource { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime RowAdded { get; set; }

    [ForeignKey("ClientLocationEntityScheduleKey")]
    [InverseProperty("ClientLocationEntityScheduleResources")]
    public virtual ClientLocationEntitySchedule ClientLocationEntityScheduleKeyNavigation { get; set; }

    [ForeignKey("ClientLocationResourceKey")]
    [InverseProperty("ClientLocationEntityScheduleResources")]
    public virtual ClientLocationResource ClientLocationResourceKeyNavigation { get; set; }
}