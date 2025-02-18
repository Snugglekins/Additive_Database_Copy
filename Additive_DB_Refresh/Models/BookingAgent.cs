﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Additive_DB_Refresh.Models;

[Index("ResellerId", "SupplierId", Name = "idx_BookingAgents_ResellerSupplier")]
public partial class BookingAgent
{
    [Key]
    public long BookingAgentKey { get; set; }

    [Required]
    [StringLength(500)]
    public string Name { get; set; }

    public bool IsActive { get; set; }

    public int ClientLocationKey { get; set; }

    public long? ClientAgentKey { get; set; }

    public short? SortOrder { get; set; }

    public bool HidePrice { get; set; }

    public DateTime RowAdded { get; set; }

    public DateTime? DateDeleted { get; set; }

    public bool EnableBilling { get; set; }

    public int NextInvoiceNumber { get; set; }

    [StringLength(4)]
    public string LetterPrefix { get; set; }

    public int StartingInvoiceNumber { get; set; }

    public bool DoesNotCollectTaxes { get; set; }

    public long? OnlineTravelAgencyKey { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string ResellerId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string SupplierId { get; set; }

    /// <summary>
    /// Online Travel Agency Api Key
    /// </summary>
    [StringLength(100)]
    [Unicode(false)]
    public string ApiKey { get; set; }

    [StringLength(50)]
    public string Code { get; set; }

    public short CommissionTypeKey { get; set; }

    [StringLength(256)]
    [Unicode(false)]
    public string ActivationEmail { get; set; }

    [Column("Partner_ClientLocationKey")]
    public int? PartnerClientLocationKey { get; set; }

    public byte BookingAgentTypeKey { get; set; }

    public bool CanSellTransportation { get; set; }

    public bool IncludeCustomFields { get; set; }

    public bool RedeamDelivery { get; set; }

    [InverseProperty("BookingAgentKeyNavigation")]
    public virtual ICollection<BookingAgentBilling> BookingAgentBillings { get; set; } = new List<BookingAgentBilling>();

    [InverseProperty("BookingAgentKeyNavigation")]
    public virtual ICollection<BookingAgentCardOnFileRequest> BookingAgentCardOnFileRequests { get; set; } = new List<BookingAgentCardOnFileRequest>();

    [InverseProperty("BookingAgentKeyNavigation")]
    public virtual ICollection<BookingAgentCardsOnFile> BookingAgentCardsOnFiles { get; set; } = new List<BookingAgentCardsOnFile>();

    [InverseProperty("BookingAgentKeyNavigation")]
    public virtual ICollection<BookingAgentCommission> BookingAgentCommissions { get; set; } = new List<BookingAgentCommission>();

    [InverseProperty("BookingAgentKeyNavigation")]
    public virtual ICollection<BookingAgentCrossSellEvent> BookingAgentCrossSellEvents { get; set; } = new List<BookingAgentCrossSellEvent>();

    [InverseProperty("BookingAgentKeyNavigation")]
    public virtual ICollection<BookingAgentEntityHierarchy> BookingAgentEntityHierarchies { get; set; } = new List<BookingAgentEntityHierarchy>();

    [InverseProperty("BookingAgentKeyNavigation")]
    public virtual ICollection<BookingAgentEntityHierarchyRate> BookingAgentEntityHierarchyRates { get; set; } = new List<BookingAgentEntityHierarchyRate>();

    [InverseProperty("BookingAgentKeyNavigation")]
    public virtual ICollection<BookingAgentPackage> BookingAgentPackages { get; set; } = new List<BookingAgentPackage>();

    [InverseProperty("BookingAgentKeyNavigation")]
    public virtual ICollection<BookingAgentTime> BookingAgentTimes { get; set; } = new List<BookingAgentTime>();

    [ForeignKey("BookingAgentTypeKey")]
    [InverseProperty("BookingAgents")]
    public virtual BookingAgentType BookingAgentTypeKeyNavigation { get; set; }

    [InverseProperty("BookingAgentKeyNavigation")]
    public virtual ICollection<ClientLocationEntityScheduleBookingAgent> ClientLocationEntityScheduleBookingAgents { get; set; } = new List<ClientLocationEntityScheduleBookingAgent>();

    [ForeignKey("ClientLocationKey")]
    [InverseProperty("BookingAgentClientLocationKeyNavigations")]
    public virtual ClientLocation ClientLocationKeyNavigation { get; set; }

    [InverseProperty("BookingAgentKeyNavigation")]
    public virtual ICollection<ClientLocationScheduleDayBookingAgent> ClientLocationScheduleDayBookingAgents { get; set; } = new List<ClientLocationScheduleDayBookingAgent>();

    [InverseProperty("BookingAgentKeyNavigation")]
    public virtual ICollection<ClientLocationScheduleTimeDayBookingAgentRateEnum> ClientLocationScheduleTimeDayBookingAgentRateEnums { get; set; } = new List<ClientLocationScheduleTimeDayBookingAgentRateEnum>();

    [InverseProperty("BookingAgentKeyNavigation")]
    public virtual ICollection<ClientLocationScheduleTimeDayBookingAgentRate> ClientLocationScheduleTimeDayBookingAgentRates { get; set; } = new List<ClientLocationScheduleTimeDayBookingAgentRate>();

    [InverseProperty("BookingAgentKeyNavigation")]
    public virtual ICollection<ClientLocationScheduleTimeDayOption> ClientLocationScheduleTimeDayOptions { get; set; } = new List<ClientLocationScheduleTimeDayOption>();

    [ForeignKey("CommissionTypeKey")]
    [InverseProperty("BookingAgents")]
    public virtual CommissionType CommissionTypeKeyNavigation { get; set; }

    [ForeignKey("OnlineTravelAgencyKey")]
    [InverseProperty("BookingAgents")]
    public virtual OnlineTravelAgency OnlineTravelAgencyKeyNavigation { get; set; }

    [ForeignKey("PartnerClientLocationKey")]
    [InverseProperty("BookingAgentPartnerClientLocationKeyNavigations")]
    public virtual ClientLocation PartnerClientLocationKeyNavigation { get; set; }
}