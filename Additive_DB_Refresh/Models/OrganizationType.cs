﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Additive_DB_Refresh.Models;

public partial class OrganizationType
{
    [Key]
    public long OrganizationTypeKey { get; set; }

    public bool IsActive { get; set; }

    public int ClientLocationKey { get; set; }

    [Required]
    [StringLength(250)]
    public string Name { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DateDeleted { get; set; }

    [ForeignKey("ClientLocationKey")]
    [InverseProperty("OrganizationTypes")]
    public virtual ClientLocation ClientLocationKeyNavigation { get; set; }
}