﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Additive_DB_Refresh.Models;

[Index("PickupRouteKey", "PickupLocationKey", "PickupDate", Name = "uq_PickupRouteStopDays_PR_PL_PickupDate", IsUnique = true)]
public partial class PickupRouteStopDay
{
    [Key]
    public int PickupRouteStopDayKey { get; set; }

    public int PickupRouteKey { get; set; }

    public long PickupLocationKey { get; set; }

    public byte StopSequence { get; set; }

    public TimeOnly DepartureTime { get; set; }

    public DateOnly PickupDate { get; set; }

    [ForeignKey("PickupLocationKey")]
    [InverseProperty("PickupRouteStopDays")]
    public virtual PickupLocation PickupLocationKeyNavigation { get; set; }

    [ForeignKey("PickupRouteKey")]
    [InverseProperty("PickupRouteStopDays")]
    public virtual PickupRoute PickupRouteKeyNavigation { get; set; }
}