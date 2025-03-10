﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Additive_DB_Refresh.Models;

public partial class EmailTemplateSection
{
    [Key]
    public long EmailTemplateSectionKey { get; set; }

    public long EmailTemplateKey { get; set; }

    public bool IsActive { get; set; }

    [StringLength(250)]
    public string Title { get; set; }

    public string Message { get; set; }

    public bool Button1On { get; set; }

    [StringLength(250)]
    public string Button1Title { get; set; }

    [StringLength(250)]
    public string Button1Link { get; set; }

    public bool Button2On { get; set; }

    [StringLength(250)]
    public string Button2Title { get; set; }

    [StringLength(250)]
    public string Button2Link { get; set; }

    public bool Button3On { get; set; }

    [StringLength(250)]
    public string Button3Title { get; set; }

    [StringLength(250)]
    public string Button3Link { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string PhotoVersion { get; set; }

    public byte Sequence { get; set; }

    [ForeignKey("EmailTemplateKey")]
    [InverseProperty("EmailTemplateSections")]
    public virtual EmailTemplate EmailTemplateKeyNavigation { get; set; }
}