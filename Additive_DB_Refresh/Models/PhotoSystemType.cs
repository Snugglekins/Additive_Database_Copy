﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Additive_DB_Refresh.Models;

public partial class PhotoSystemType
{
    [Key]
    public short PhotoSystemTypeKey { get; set; }

    [Required]
    [StringLength(200)]
    [Unicode(false)]
    public string Name { get; set; }
}