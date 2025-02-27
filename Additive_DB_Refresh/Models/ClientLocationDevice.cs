﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Additive_DB_Refresh.Models;

public partial class ClientLocationDevice
{
    [Key]
    public long ClientLocationDeviceKey { get; set; }

    public int ClientLocationKey { get; set; }

    public int DeviceTypeKey { get; set; }

    [Required]
    [Column("IPV4Address")]
    [StringLength(30)]
    public string Ipv4address { get; set; }

    [Required]
    [StringLength(512)]
    public string DeviceName { get; set; }

    [Column("UseSSL")]
    public bool UseSsl { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string DeviceId { get; set; }

    [ForeignKey("ClientLocationKey")]
    [InverseProperty("ClientLocationDevices")]
    public virtual ClientLocation ClientLocationKeyNavigation { get; set; }

    [InverseProperty("DeviceKeyNavigation")]
    public virtual ICollection<DeviceParing> DeviceParingDeviceKeyNavigations { get; set; } = new List<DeviceParing>();

    [InverseProperty("TerminalKeyNavigation")]
    public virtual ICollection<DeviceParing> DeviceParingTerminalKeyNavigations { get; set; } = new List<DeviceParing>();

    [ForeignKey("DeviceTypeKey")]
    [InverseProperty("ClientLocationDevices")]
    public virtual DeviceType DeviceTypeKeyNavigation { get; set; }
}