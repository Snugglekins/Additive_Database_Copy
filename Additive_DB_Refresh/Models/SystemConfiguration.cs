﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Additive_DB_Refresh.Models;

[Keyless]
[Table("SystemConfiguration")]
public partial class SystemConfiguration
{
    [StringLength(500)]
    [Unicode(false)]
    public string ConfigurationParameter { get; set; }

    public string ConfigurationValue { get; set; }
}