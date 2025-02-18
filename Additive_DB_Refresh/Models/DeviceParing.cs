﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Additive_DB_Refresh.Models;

public partial class DeviceParing
{
    [Key]
    public long DeviceParingKey { get; set; }

    public long TerminalKey { get; set; }

    public long DeviceKey { get; set; }

    [ForeignKey("DeviceKey")]
    [InverseProperty("DeviceParingDeviceKeyNavigations")]
    public virtual ClientLocationDevice DeviceKeyNavigation { get; set; }

    [ForeignKey("TerminalKey")]
    [InverseProperty("DeviceParingTerminalKeyNavigations")]
    public virtual ClientLocationDevice TerminalKeyNavigation { get; set; }
}