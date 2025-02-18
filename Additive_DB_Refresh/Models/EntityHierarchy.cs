﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Additive_DB_Refresh.Models;

[Table("EntityHierarchy")]
public partial class EntityHierarchy
{
    [Key]
    public long EntityHierarchyKey { get; set; }

    public long? DefaultEntityHierarchyKey { get; set; }

    public int ClientLocationKey { get; set; }

    [Required]
    public string Name { get; set; }

    public string Tout { get; set; }

    [Required]
    public string Description { get; set; }

    public string MetaTitle { get; set; }

    public string MetaDescription { get; set; }

    public string MetaKeywords { get; set; }

    public bool IsActive { get; set; }

    public short SortIndex { get; set; }

    public bool Bookable { get; set; }

    [Required]
    [StringLength(500)]
    public string TimeZoneName { get; set; }

    public bool HasInventory { get; set; }

    public bool ConsentRequired { get; set; }

    public string ReceiptHeaderHtml { get; set; }

    public string ReceiptFooterHtml { get; set; }

    public TimeOnly? TransportationCutoffTime { get; set; }

    public bool OfferTransportation { get; set; }

    public bool HideFromTablet { get; set; }

    public bool EnableAutoPhotos { get; set; }

    public byte ChildMinAge { get; set; }

    public byte ChildMaxAge { get; set; }

    public bool AllowPhotos { get; set; }

    public short? PhotoSystemTypeKey { get; set; }

    public bool HideFromCustomer { get; set; }

    [Required]
    public string InternalName { get; set; }

    [StringLength(100)]
    public string Difficulty { get; set; }

    [StringLength(100)]
    public string Duration { get; set; }

    [StringLength(100)]
    public string AgeMinimum { get; set; }

    public string DirectionsLink { get; set; }

    public string ReminderMessage { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string PhotoVersion { get; set; }

    public int MaxPeople { get; set; }

    public int? MaxAdults { get; set; }

    public int? MaxChildren { get; set; }

    [Column(TypeName = "money")]
    public decimal? SlashThroughPrice { get; set; }

    public string ConfirmationMessage { get; set; }

    public bool AdultsOnly { get; set; }

    public bool ChildrenOnly { get; set; }

    public bool RequireAdults { get; set; }

    public bool DisableLargeGroup { get; set; }

    public short? HideSpotsLeftUntilLessThan { get; set; }

    public bool UseTicketing { get; set; }

    [Column(TypeName = "money")]
    public decimal? ChildSlashThroughPrice { get; set; }

    public bool EnablePostVisitEmail { get; set; }

    public bool EnablePhotoPurchaseEmail { get; set; }

    public string TicketDisclaimer { get; set; }

    [StringLength(100)]
    public string MinHeight { get; set; }

    public int? TransportationCutoffHours { get; set; }

    public string GroupEmailFooter { get; set; }

    public short? MinParticipants { get; set; }

    public bool EnablePrivateTours { get; set; }

    public bool EnableTips { get; set; }

    public bool EnableAutoTips { get; set; }

    public bool EnablePhotoAddOn { get; set; }

    [StringLength(250)]
    public string PhotoAddOnTitle { get; set; }

    public string PhotoAddOnDescription { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string PhotoAddOnImageVersion { get; set; }

    public bool PrivateToursOnly { get; set; }

    public bool CanAssignGuides { get; set; }

    [StringLength(250)]
    public string DisplayPrice { get; set; }

    public bool OverrideAutoPrice { get; set; }

    public bool EnableOnlineTips { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    public decimal? TipAddOn1Percent { get; set; }

    [StringLength(50)]
    public string TipAddOn1Name { get; set; }

    [StringLength(50)]
    public string TipAddOn1Description { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    public decimal? TipAddOn2Percent { get; set; }

    [StringLength(50)]
    public string TipAddOn2Name { get; set; }

    [StringLength(50)]
    public string TipAddOn2Description { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    public decimal? TipAddOn3Percent { get; set; }

    [StringLength(50)]
    public string TipAddOn3Name { get; set; }

    [StringLength(50)]
    public string TipAddOn3Description { get; set; }

    public DateOnly? NextAvailableDate { get; set; }

    public short CapacityTypeKey { get; set; }

    public bool IsTipRequired { get; set; }

    [StringLength(250)]
    public string GoogleCalendarId { get; set; }

    public bool OverrideRateLabel { get; set; }

    [StringLength(250)]
    public string RateLabelSingular { get; set; }

    [StringLength(250)]
    public string RateLabelPlural { get; set; }

    public bool OverrideAddOnLabel { get; set; }

    [StringLength(250)]
    public string AddOnLabel { get; set; }

    public string AddOnDescription { get; set; }

    public bool GoogleCalendarSendGuideInvite { get; set; }

    public short? TicketTypeKey { get; set; }

    public short AppointmentTypeKey { get; set; }

    [StringLength(250)]
    public string Color { get; set; }

    public bool PrintTickets { get; set; }

    public bool CompleteAfterAllTicketsPrinted { get; set; }

    public bool UseScanApp { get; set; }

    public bool AutoPrintTickets { get; set; }

    public string ZebraTicketTemplate { get; set; }

    public bool EnableRentals { get; set; }

    public bool IsForRentals { get; set; }

    public bool HidePriceFromCustomer { get; set; }

    public bool RedeamEnabled { get; set; }

    [StringLength(36)]
    [Unicode(false)]
    public string RedeamProductId { get; set; }

    public int? DurationInMinutes { get; set; }

    public string CancellationEmailHeader { get; set; }

    public bool EnableMultiRateRedeem { get; set; }

    public bool EnableDigitalTickets { get; set; }

    public byte? DefaultTip { get; set; }

    public bool AssignWaiversToTickets { get; set; }

    public bool AssignGuidesToAppointments { get; set; }

    public bool EnableDynamicPricing { get; set; }

    public bool EnableAutoLargeGroupTips { get; set; }

    public short? AutoTipGroupLargerThan { get; set; }

    public byte? LargeGroupAutoTip { get; set; }

    public bool AllowSelfReschedule { get; set; }

    public bool EnableCustomReschedulePolicy { get; set; }

    public string ReschedulePolicy { get; set; }

    public bool AllowSelfCancellation { get; set; }

    public bool EnableCustomCancellationPolicy { get; set; }

    public string CancellationPolicy { get; set; }

    public bool AllowRefundToGiftCard { get; set; }

    public bool AllowRefundToCard { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string PhotoBackgroundImageVersion { get; set; }

    [Column("Resources_ManageCapacity")]
    public bool ResourcesManageCapacity { get; set; }

    [Column("Resources_CombineParties")]
    public bool ResourcesCombineParties { get; set; }

    [Column("Resources_SplitParties")]
    public bool ResourcesSplitParties { get; set; }

    [Column("Resources_PreventOrphanParties")]
    public bool ResourcesPreventOrphanParties { get; set; }

    /// <summary>
    /// Control if timeslots are hidden when outside the timeout window. True = hide, False = show.
    /// </summary>
    public bool EnableTransportationTimeout { get; set; }

    [StringLength(250)]
    public string KioskDisplayPrice { get; set; }

    public bool PhotoAddOnInternalOnly { get; set; }

    public bool IsTransportationRequired { get; set; }

    public bool IsTransportationIncluded { get; set; }

    [ForeignKey("AppointmentTypeKey")]
    [InverseProperty("EntityHierarchies")]
    public virtual AppointmentType AppointmentTypeKeyNavigation { get; set; }

    [InverseProperty("EntityHierarchyKeyNavigation")]
    public virtual ICollection<BookingAgentEntityHierarchy> BookingAgentEntityHierarchies { get; set; } = new List<BookingAgentEntityHierarchy>();

    [InverseProperty("EntityHierarchyKeyNavigation")]
    public virtual ICollection<BookingAgentEntityHierarchyRate> BookingAgentEntityHierarchyRates { get; set; } = new List<BookingAgentEntityHierarchyRate>();

    [ForeignKey("CapacityTypeKey")]
    [InverseProperty("EntityHierarchies")]
    public virtual CapacityType CapacityTypeKeyNavigation { get; set; }

    [InverseProperty("EntityHierarchyKeyNavigation")]
    public virtual ICollection<ClientLocationAddOnsEntityHierarchy> ClientLocationAddOnsEntityHierarchies { get; set; } = new List<ClientLocationAddOnsEntityHierarchy>();

    [InverseProperty("EntityHierarchyKeyNavigation")]
    public virtual ICollection<ClientLocationCustomFormAssociation> ClientLocationCustomFormAssociations { get; set; } = new List<ClientLocationCustomFormAssociation>();

    [InverseProperty("EntityHierarchyKeyNavigation")]
    public virtual ICollection<ClientLocationEntity> ClientLocationEntities { get; set; } = new List<ClientLocationEntity>();

    [InverseProperty("EntityHierarchyKeyNavigation")]
    public virtual ICollection<ClientLocationEntityHierarchyDiscount> ClientLocationEntityHierarchyDiscounts { get; set; } = new List<ClientLocationEntityHierarchyDiscount>();

    [InverseProperty("EntityHierarchyKeyNavigation")]
    public virtual ICollection<ClientLocationItemDirectory> ClientLocationItemDirectories { get; set; } = new List<ClientLocationItemDirectory>();

    [ForeignKey("ClientLocationKey")]
    [InverseProperty("EntityHierarchies")]
    public virtual ClientLocation ClientLocationKeyNavigation { get; set; }

    [InverseProperty("EntityHierarchyKeyNavigation")]
    public virtual ICollection<ClientLocationRolesEntityHierarchy> ClientLocationRolesEntityHierarchies { get; set; } = new List<ClientLocationRolesEntityHierarchy>();

    [InverseProperty("EntityHierarchyKeyNavigation")]
    public virtual ICollection<ClientLocationScheduleDayBookingAgentRate> ClientLocationScheduleDayBookingAgentRates { get; set; } = new List<ClientLocationScheduleDayBookingAgentRate>();

    [InverseProperty("EntityHierarchyKeyNavigation")]
    public virtual ICollection<ClientLocationScheduleTimeDayBookingAgentRateEnum> ClientLocationScheduleTimeDayBookingAgentRateEnums { get; set; } = new List<ClientLocationScheduleTimeDayBookingAgentRateEnum>();

    [InverseProperty("EntityHierarchyKeyNavigation")]
    public virtual ICollection<ClientLocationScheduleTimeDayNote> ClientLocationScheduleTimeDayNotes { get; set; } = new List<ClientLocationScheduleTimeDayNote>();

    [InverseProperty("EntityHierarchyKeyNavigation")]
    public virtual ICollection<EmailTemplate> EmailTemplates { get; set; } = new List<EmailTemplate>();

    [InverseProperty("EntityHierarchyKeyNavigation")]
    public virtual ICollection<EntityHierarchyChangeWindow> EntityHierarchyChangeWindows { get; set; } = new List<EntityHierarchyChangeWindow>();

    [InverseProperty("EntityHierarchyKeyNavigation")]
    public virtual ICollection<EntityHierarchyEmployee> EntityHierarchyEmployees { get; set; } = new List<EntityHierarchyEmployee>();

    [InverseProperty("EntityHierarchyKeyNavigation")]
    public virtual ICollection<EntityHierarchyInventory> EntityHierarchyInventories { get; set; } = new List<EntityHierarchyInventory>();

    [InverseProperty("EntityHierarchyKeyNavigation")]
    public virtual ICollection<EntityHierarchyOption> EntityHierarchyOptions { get; set; } = new List<EntityHierarchyOption>();

    [InverseProperty("EntityHierarchyKeyNavigation")]
    public virtual ICollection<EntityHierarchyRateResource> EntityHierarchyRateResources { get; set; } = new List<EntityHierarchyRateResource>();

    [InverseProperty("EntityHierarchyKeyNavigation")]
    public virtual ICollection<EntityHierarchyRate> EntityHierarchyRates { get; set; } = new List<EntityHierarchyRate>();

    [InverseProperty("EntityHierarchyKeyNavigation")]
    public virtual ICollection<EntityHierarchyTranslation> EntityHierarchyTranslations { get; set; } = new List<EntityHierarchyTranslation>();

    [InverseProperty("EntityHierarchyKeyNavigation")]
    public virtual ICollection<History_ClientLocationEntityHierarchyDiscount> History_ClientLocationEntityHierarchyDiscounts { get; set; } = new List<History_ClientLocationEntityHierarchyDiscount>();

    [InverseProperty("EntityHierarchyKeyNavigation")]
    public virtual ICollection<History_EntityHierarchyRate> History_EntityHierarchyRates { get; set; } = new List<History_EntityHierarchyRate>();

    [InverseProperty("EntityHierarchyKeyNavigation")]
    public virtual ICollection<Merchandise_ProductAddOnEntityHierarchy> Merchandise_ProductAddOnEntityHierarchies { get; set; } = new List<Merchandise_ProductAddOnEntityHierarchy>();

    [InverseProperty("EntityHierarchyKeyNavigation")]
    public virtual ICollection<PackageDetail> PackageDetails { get; set; } = new List<PackageDetail>();

    [ForeignKey("TicketTypeKey")]
    [InverseProperty("EntityHierarchies")]
    public virtual Ticketing_TicketType TicketTypeKeyNavigation { get; set; }

    [InverseProperty("EntityHierarchyKeyNavigation")]
    public virtual ICollection<avail_ScheduleDay> avail_ScheduleDays { get; set; } = new List<avail_ScheduleDay>();

    [InverseProperty("EntityHierarchyKeyNavigation")]
    public virtual ICollection<avail_ScheduleRateResource> avail_ScheduleRateResources { get; set; } = new List<avail_ScheduleRateResource>();

    [InverseProperty("EntityHierarchyKeyNavigation")]
    public virtual ICollection<avail_ScheduleTimeResource> avail_ScheduleTimeResources { get; set; } = new List<avail_ScheduleTimeResource>();
}