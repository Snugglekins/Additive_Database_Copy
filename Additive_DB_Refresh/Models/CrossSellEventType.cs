﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Additive_DB_Refresh.Models;

public partial class CrossSellEventType
{
    [Key]
    public short CrossSellEventTypeKey { get; set; }

    [StringLength(50)]
    public string CrossSellEventName { get; set; }

    [InverseProperty("CrossSellEventTypeKeyNavigation")]
    public virtual ICollection<BookingAgentCrossSellEvent> BookingAgentCrossSellEvents { get; set; } = new List<BookingAgentCrossSellEvent>();
}