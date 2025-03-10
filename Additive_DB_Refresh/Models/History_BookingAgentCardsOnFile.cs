﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Additive_DB_Refresh.Models;

[Table("BookingAgentCardsOnFile", Schema = "History")]
public partial class History_BookingAgentCardsOnFile
{
    [Key]
    public long BookingAgentCardOnFileHistoryKey { get; set; }

    public long BookingAgentCardOnFileKey { get; set; }

    public long? BookingAgentKey { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string CustomerId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string CustomerCardId { get; set; }

    [StringLength(255)]
    public string NameOnCard { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ExpDate { get; set; }

    [StringLength(4)]
    [Unicode(false)]
    public string Last4 { get; set; }

    public bool? HasAffiliateApproval { get; set; }

    public long? AffiliateApprovalClientLoginKey { get; set; }

    public DateTime? DateFrom { get; set; }

    public DateTime? DateTo { get; set; }

    public bool? IsCurrent { get; set; }

    public byte? CardTypeKey { get; set; }

    [InverseProperty("BookingAgentCardOnFileHistoryKeyNavigation")]
    public virtual ICollection<BookingAgentBilling> BookingAgentBillings { get; set; } = new List<BookingAgentBilling>();

    [ForeignKey("BookingAgentCardOnFileKey")]
    [InverseProperty("History_BookingAgentCardsOnFiles")]
    public virtual BookingAgentCardsOnFile BookingAgentCardOnFileKeyNavigation { get; set; }
}