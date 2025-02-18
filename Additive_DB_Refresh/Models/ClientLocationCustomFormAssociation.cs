﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Additive_DB_Refresh.Models;

public partial class ClientLocationCustomFormAssociation
{
    [Key]
    public int ClientLocationCustomFormAssociationKey { get; set; }

    public int ClientLocationCustomFormKey { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime RowAdded { get; set; }

    public int ClientLocationKey { get; set; }

    public long? EntityHierarchyKey { get; set; }

    public long? EntityHierarchyRateKey { get; set; }

    public int? ConsentFormKey { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DateDeleted { get; set; }

    [ForeignKey("ClientLocationCustomFormKey")]
    [InverseProperty("ClientLocationCustomFormAssociations")]
    public virtual ClientLocationCustomForm ClientLocationCustomFormKeyNavigation { get; set; }

    [ForeignKey("ClientLocationKey")]
    [InverseProperty("ClientLocationCustomFormAssociations")]
    public virtual ClientLocation ClientLocationKeyNavigation { get; set; }

    [ForeignKey("ConsentFormKey")]
    [InverseProperty("ClientLocationCustomFormAssociations")]
    public virtual ConsentForm ConsentFormKeyNavigation { get; set; }

    [ForeignKey("EntityHierarchyKey")]
    [InverseProperty("ClientLocationCustomFormAssociations")]
    public virtual EntityHierarchy EntityHierarchyKeyNavigation { get; set; }

    [ForeignKey("EntityHierarchyRateKey")]
    [InverseProperty("ClientLocationCustomFormAssociations")]
    public virtual EntityHierarchyRate EntityHierarchyRateKeyNavigation { get; set; }
}