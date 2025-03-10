﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Additive_DB_Refresh.Models;

[Index("ClientLocationKey", "ApplicationRoleKey", Name = "uq_ClientLocationRoles_ClientLoc_AppRole", IsUnique = true)]
public partial class ClientLocationRole
{
    [Key]
    public long ClientLocationRoleKey { get; set; }

    public int ClientLocationKey { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string RoleName { get; set; }

    [Unicode(false)]
    public string RoleDescription { get; set; }

    public short ApplicationRoleKey { get; set; }

    [ForeignKey("ApplicationRoleKey")]
    [InverseProperty("ClientLocationRoles")]
    public virtual System_ApplicationRole ApplicationRoleKeyNavigation { get; set; }

    [ForeignKey("ClientLocationKey")]
    [InverseProperty("ClientLocationRoles")]
    public virtual ClientLocation ClientLocationKeyNavigation { get; set; }

    [InverseProperty("ClientLocationRoleKeyNavigation")]
    public virtual ICollection<ClientLocationLoginRole> ClientLocationLoginRoles { get; set; } = new List<ClientLocationLoginRole>();

    [InverseProperty("ClientLocationRoleKeyNavigation")]
    public virtual ICollection<ClientLocationRoleApplicationObject> ClientLocationRoleApplicationObjects { get; set; } = new List<ClientLocationRoleApplicationObject>();

    [InverseProperty("ClientLocationRoleKeyNavigation")]
    public virtual ICollection<ClientLocationRolesEntityHierarchy> ClientLocationRolesEntityHierarchies { get; set; } = new List<ClientLocationRolesEntityHierarchy>();
}