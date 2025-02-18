﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Additive_DB_Refresh.Models;

[Index("EntityHierarchyKey", "IsActive", "DateDeleted", Name = "ix_EntityHierarchyRates_EntityHierarchyKey", IsDescending = new[] { false, true, false })]
public partial class EntityHierarchyRate
{
    [Key]
    public long EntityHierarchyRateKey { get; set; }

    public long EntityHierarchyKey { get; set; }

    [Required]
    [StringLength(250)]
    public string Name { get; set; }

    [Required]
    [StringLength(250)]
    public string InternalName { get; set; }

    public string Description { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal Price { get; set; }

    public int? MinAge { get; set; }

    public int? MaxAge { get; set; }

    public short? MaxParticipants { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal? SlashThroughPrice { get; set; }

    public bool IsFromPrice { get; set; }

    public bool InternalUseOnly { get; set; }

    public int? Duration { get; set; }

    public short? MinParticipants { get; set; }

    public bool HideFromCustomer { get; set; }

    public bool IsPrivateTourRate { get; set; }

    public short? DefaultParticipants { get; set; }

    public bool DoesNotRequireWaiver { get; set; }

    public DateTime RowAdded { get; set; }

    public DateTime? DateDeleted { get; set; }

    public bool IsActive { get; set; }

    public int Sequence { get; set; }

    public bool IsRequired { get; set; }

    public bool MinForPrivateTourOnly { get; set; }

    public short? MaxParticipantsPerTime { get; set; }

    public bool SetPrivateTourPrice { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string PhotoVersion { get; set; }

    public int? RateClassKey { get; set; }

    [StringLength(50)]
    public string GlobalId { get; set; }

    public bool IsForPrivateToursOnly { get; set; }

    [Column("Resources_AskForWaiverCount")]
    public bool ResourcesAskForWaiverCount { get; set; }

    [Column("Resources_ExcludeFromWaiverCount")]
    public bool ResourcesExcludeFromWaiverCount { get; set; }

    [Column("Resources_SeatsPerRate")]
    public int ResourcesSeatsPerRate { get; set; }

    [Column("Resources_AskForWaiverCountLabel")]
    [StringLength(250)]
    public string ResourcesAskForWaiverCountLabel { get; set; }

    public bool ShowOnKiosk { get; set; }

    public bool EnableExpiration { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ExpirationDate { get; set; }

    [InverseProperty("EntityHierarchyRateKeyNavigation")]
    public virtual ICollection<BookingAgentEntityHierarchyRate> BookingAgentEntityHierarchyRates { get; set; } = new List<BookingAgentEntityHierarchyRate>();

    [InverseProperty("EntityHierarchyRateKeyNavigation")]
    public virtual ICollection<ClientLocationCustomFormAssociation> ClientLocationCustomFormAssociations { get; set; } = new List<ClientLocationCustomFormAssociation>();

    [InverseProperty("EntityHierarchyRateKeyNavigation")]
    public virtual ICollection<ClientLocationEntityHierarchyDiscountRate> ClientLocationEntityHierarchyDiscountRateEntityHierarchyRateKeyNavigations { get; set; } = new List<ClientLocationEntityHierarchyDiscountRate>();

    [InverseProperty("RequiredEntityHierarchyRateKeyNavigation")]
    public virtual ICollection<ClientLocationEntityHierarchyDiscountRate> ClientLocationEntityHierarchyDiscountRateRequiredEntityHierarchyRateKeyNavigations { get; set; } = new List<ClientLocationEntityHierarchyDiscountRate>();

    [InverseProperty("EntityHierarchyRateKeyNavigation")]
    public virtual ICollection<ClientLocationEntityScheduleRate> ClientLocationEntityScheduleRates { get; set; } = new List<ClientLocationEntityScheduleRate>();

    [InverseProperty("EntityHierarchyRateKeyNavigation")]
    public virtual ICollection<ClientLocationScheduleTimeDayBookingAgentRateEnum> ClientLocationScheduleTimeDayBookingAgentRateEnums { get; set; } = new List<ClientLocationScheduleTimeDayBookingAgentRateEnum>();

    [InverseProperty("EntityHierarchyRateKeyNavigation")]
    public virtual ICollection<ClientLocationScheduleTimeDayBookingAgentRate> ClientLocationScheduleTimeDayBookingAgentRates { get; set; } = new List<ClientLocationScheduleTimeDayBookingAgentRate>();

    [InverseProperty("EntityHierarchyRateKeyNavigation")]
    public virtual ICollection<ClientLocationScheduleTimeDayRateResource> ClientLocationScheduleTimeDayRateResources { get; set; } = new List<ClientLocationScheduleTimeDayRateResource>();

    [InverseProperty("EntityHierarchyRateKeyNavigation")]
    public virtual ICollection<ClientLocationScheduleTimeDayRate> ClientLocationScheduleTimeDayRates { get; set; } = new List<ClientLocationScheduleTimeDayRate>();

    [ForeignKey("EntityHierarchyKey")]
    [InverseProperty("EntityHierarchyRates")]
    public virtual EntityHierarchy EntityHierarchyKeyNavigation { get; set; }

    [InverseProperty("EntityHierarchyRateKeyNavigation")]
    public virtual ICollection<EntityHierarchyOptionRate> EntityHierarchyOptionRates { get; set; } = new List<EntityHierarchyOptionRate>();

    [InverseProperty("EntityHierarchyRateKeyNavigation")]
    public virtual ICollection<EntityHierarchyRateResource> EntityHierarchyRateResources { get; set; } = new List<EntityHierarchyRateResource>();

    [InverseProperty("EntityHierarchyRateKeyNavigation")]
    public virtual ICollection<History_ClientLocationEntityHierarchyDiscountRate> History_ClientLocationEntityHierarchyDiscountRateEntityHierarchyRateKeyNavigations { get; set; } = new List<History_ClientLocationEntityHierarchyDiscountRate>();

    [InverseProperty("RequiredEntityHierarchyRateKeyNavigation")]
    public virtual ICollection<History_ClientLocationEntityHierarchyDiscountRate> History_ClientLocationEntityHierarchyDiscountRateRequiredEntityHierarchyRateKeyNavigations { get; set; } = new List<History_ClientLocationEntityHierarchyDiscountRate>();

    [InverseProperty("EntityHierarchyRateKeyNavigation")]
    public virtual ICollection<History_ClientLocationEntityScheduleRate> History_ClientLocationEntityScheduleRates { get; set; } = new List<History_ClientLocationEntityScheduleRate>();

    [InverseProperty("EntityHierarchyRateKeyNavigation")]
    public virtual ICollection<History_EntityHierarchyRate> History_EntityHierarchyRates { get; set; } = new List<History_EntityHierarchyRate>();

    [InverseProperty("EntityHierarchyRateKeyNavigation")]
    public virtual ICollection<PackageDetailRate> PackageDetailRates { get; set; } = new List<PackageDetailRate>();

    [ForeignKey("RateClassKey")]
    [InverseProperty("EntityHierarchyRates")]
    public virtual RateClass RateClassKeyNavigation { get; set; }

    [InverseProperty("EntityHierarchyRateKeyNavigation")]
    public virtual ICollection<avail_ScheduleRateResource> avail_ScheduleRateResources { get; set; } = new List<avail_ScheduleRateResource>();
}