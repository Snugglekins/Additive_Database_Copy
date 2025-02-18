﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Additive_DB_Refresh.Models;

[Table("ClientLocationProcessingFeeHistory")]
public partial class ClientLocationProcessingFeeHistory
{
    [Key]
    public long ClientLocationProcessingFeeHistoryKey { get; set; }

    public long ClientLocationProcessingFeeKey { get; set; }

    public int ClientLocationKey { get; set; }

    public bool IsActive { get; set; }

    [Column(TypeName = "decimal(10, 8)")]
    public decimal FeePerc { get; set; }

    [Column(TypeName = "decimal(10, 8)")]
    public decimal FeePercCharge { get; set; }

    [Column(TypeName = "decimal(10, 8)")]
    public decimal FeePercTarget { get; set; }

    public DateTime? RowAdded { get; set; }

    public DateTime? DateDeleted { get; set; }

    [ForeignKey("ClientLocationKey")]
    [InverseProperty("ClientLocationProcessingFeeHistories")]
    public virtual ClientLocation ClientLocationKeyNavigation { get; set; }

    [ForeignKey("ClientLocationProcessingFeeKey")]
    [InverseProperty("ClientLocationProcessingFeeHistories")]
    public virtual ClientLocationProcessingFee ClientLocationProcessingFeeKeyNavigation { get; set; }
}