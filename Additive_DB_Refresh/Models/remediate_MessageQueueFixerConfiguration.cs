﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Additive_DB_Refresh.Models;

[Table("MessageQueueFixerConfiguration", Schema = "remediate")]
public partial class remediate_MessageQueueFixerConfiguration
{
    [Key]
    public int ConfigurationKey { get; set; }

    [Required]
    [StringLength(512)]
    [Unicode(false)]
    public string ProcedureName { get; set; }

    [Required]
    [StringLength(100)]
    [Unicode(false)]
    public string OrderFilePrefix { get; set; }

    [Required]
    [Unicode(false)]
    public string RegExSearch { get; set; }

    public bool RequiresFollowup { get; set; }

    public bool IsActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime RowAdded { get; set; }
}