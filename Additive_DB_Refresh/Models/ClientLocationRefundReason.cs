﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Additive_DB_Refresh.Models;

[Index("ClientLocationKey", "Reason", Name = "UK_ClientLocationRefundReasons", IsUnique = true)]
public partial class ClientLocationRefundReason
{
    [Key]
    public Guid ClientLocationRefundReasonKey { get; set; }

    public int ClientLocationKey { get; set; }

    [Required]
    [StringLength(150)]
    public string Reason { get; set; }

    public bool IncludeInBalance { get; set; }

    public DateTime? DateDeleted { get; set; }

    public short Sequence { get; set; }

    public bool IsActive { get; set; }

    [ForeignKey("ClientLocationKey")]
    [InverseProperty("ClientLocationRefundReasons")]
    public virtual ClientLocation ClientLocationKeyNavigation { get; set; }
}