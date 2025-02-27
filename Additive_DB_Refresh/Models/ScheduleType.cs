﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Additive_DB_Refresh.Models;

public partial class ScheduleType
{
    [Key]
    public short ScheduleTypeKey { get; set; }

    [Required]
    [StringLength(500)]
    [Unicode(false)]
    public string Description { get; set; }

    public bool IncludeInScheduling { get; set; }

    [InverseProperty("ScheduleTypeKeyNavigation")]
    public virtual ICollection<ClientLocationEntitySchedule> ClientLocationEntitySchedules { get; set; } = new List<ClientLocationEntitySchedule>();

    [InverseProperty("ScheduleTypeKeyNavigation")]
    public virtual ICollection<ClientLocationScheduleDaysEnum> ClientLocationScheduleDaysEnums { get; set; } = new List<ClientLocationScheduleDaysEnum>();
}