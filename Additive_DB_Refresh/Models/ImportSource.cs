﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Additive_DB_Refresh.Models;

public partial class ImportSource
{
    [Key]
    public int ImportSourceKey { get; set; }

    [StringLength(250)]
    public string ImportSourceName { get; set; }
}