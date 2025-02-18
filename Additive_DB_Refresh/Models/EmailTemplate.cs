﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Additive_DB_Refresh.Models;

public partial class EmailTemplate
{
    [Key]
    public long EmailTemplateKey { get; set; }

    public bool Enabled { get; set; }

    [StringLength(250)]
    public string FromName { get; set; }

    [StringLength(250)]
    public string FromEmail { get; set; }

    public bool EnableFooter { get; set; }

    public string Footer { get; set; }

    public byte Sequence { get; set; }

    [StringLength(250)]
    public string Subject { get; set; }

    public byte? HoursAfterAppointment { get; set; }

    public int ClientLocationKey { get; set; }

    public short NotificationTypeKey { get; set; }

    public short NotificationDeliveryTypeKey { get; set; }

    public long? EntityHierarchyKey { get; set; }

    [ForeignKey("ClientLocationKey")]
    [InverseProperty("EmailTemplates")]
    public virtual ClientLocation ClientLocationKeyNavigation { get; set; }

    [InverseProperty("EmailTemplateKeyNavigation")]
    public virtual ICollection<EmailTemplateSection> EmailTemplateSections { get; set; } = new List<EmailTemplateSection>();

    [ForeignKey("EntityHierarchyKey")]
    [InverseProperty("EmailTemplates")]
    public virtual EntityHierarchy EntityHierarchyKeyNavigation { get; set; }

    [ForeignKey("NotificationDeliveryTypeKey")]
    [InverseProperty("EmailTemplates")]
    public virtual NotificationSystem_NotificationDeliveryType NotificationDeliveryTypeKeyNavigation { get; set; }

    [ForeignKey("NotificationTypeKey")]
    [InverseProperty("EmailTemplates")]
    public virtual NotificationSystem_NotificationType NotificationTypeKeyNavigation { get; set; }
}