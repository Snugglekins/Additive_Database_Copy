﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Additive_DB_Refresh.Models;

[Index("ClientLocationEntityKey", "CameraId", Name = "U_ClientLocationEntityCameras", IsUnique = true)]
public partial class Camera
{
    [Key]
    public int CameraKey { get; set; }

    public long ClientLocationEntityKey { get; set; }

    public int Sequence { get; set; }

    public bool LastCamera { get; set; }

    [Required]
    [Column("CameraID")]
    [StringLength(50)]
    [Unicode(false)]
    public string CameraId { get; set; }

    public bool AutomatedCamera { get; set; }

    public short? TagUnassignmentWindow { get; set; }

    [StringLength(500)]
    public string LocationDescription { get; set; }

    public bool Debug { get; set; }

    public bool IsActive { get; set; }

    [Column("IPV4")]
    [StringLength(15)]
    [Unicode(false)]
    public string Ipv4 { get; set; }

    [ForeignKey("ClientLocationEntityKey")]
    [InverseProperty("Cameras")]
    public virtual ClientLocationEntity ClientLocationEntityKeyNavigation { get; set; }
}