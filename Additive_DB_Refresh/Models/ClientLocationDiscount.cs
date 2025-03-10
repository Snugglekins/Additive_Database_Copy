﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Additive_DB_Refresh.Models;

[Index("ClientLocationKey", "PromoCode", "RequiredParing", Name = "nci_wi_ClientLocationDiscounts_245F9CCB121160A0AC255D9FA03487C5")]
[Index("ClientLocationKey", "Deleted", "IsActive", "PromoCode", Name = "nci_wi_ClientLocationDiscounts_7E4516609C8C79D2EF0AD8AFCA4F595B")]
public partial class ClientLocationDiscount
{
    [Key]
    public long ClientLocationDiscountKey { get; set; }

    public int ClientKey { get; set; }

    public int? ClientLocationKey { get; set; }

    [Required]
    public string DiscountName { get; set; }

    [Column(TypeName = "numeric(12, 9)")]
    public decimal? Value { get; set; }

    public short? DiscountTypeKey { get; set; }

    public bool IsActive { get; set; }

    public DateOnly? FromDate { get; set; }

    public DateOnly? ToDate { get; set; }

    public bool IsAutoApply { get; set; }

    public int? GroupLargerEqualTo { get; set; }

    public bool RequiredParing { get; set; }

    public bool ApplyOnce { get; set; }

    public bool CanBeCombined { get; set; }

    [StringLength(100)]
    public string PromoCode { get; set; }

    public bool UseAppointmentDate { get; set; }

    [Column("IsBOGO")]
    public bool IsBogo { get; set; }

    public short DiscountApplicationTypeKey { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? Deleted { get; set; }

    public bool IsComp { get; set; }

    public bool RequireNote { get; set; }

    [Column("SKU")]
    [StringLength(50)]
    [Unicode(false)]
    public string Sku { get; set; }

    public short? MultiplePromoCodeLength { get; set; }

    public bool IsMultiplePromoCodeListProvided { get; set; }

    public bool UseMultiplePromoCodes { get; set; }

    public int? MultiplePromoCodeCount { get; set; }

    public long? BookingAgentKey { get; set; }

    public short? MaxPeople { get; set; }

    public int? MaxNumberOfUses { get; set; }

    public bool RestrictDaysOfWeek { get; set; }

    public bool IsValidSunday { get; set; }

    public bool IsValidMonday { get; set; }

    public bool IsValidTuesday { get; set; }

    public bool IsValidWednesday { get; set; }

    public bool IsValidThursday { get; set; }

    public bool IsValidFriday { get; set; }

    public bool IsValidSaturday { get; set; }

    public bool RestrictTimeOfDay { get; set; }

    public bool IsLimitedMaxPeople { get; set; }

    public bool IsLimitedNumberOfUses { get; set; }

    public int? DaysFromApptGreaterThanEqualTo { get; set; }

    [Column("HasSKU")]
    public bool HasSku { get; set; }

    public bool IsAssignedVendor { get; set; }

    public int? TotalNumberOfUses { get; set; }

    public int? NumberOfGuests { get; set; }

    public int? MaxAdults { get; set; }

    public int? MaxChildren { get; set; }

    public bool IsLimitedNumberOfGuests { get; set; }

    public bool InternalUseOnly { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? RowAdded { get; set; }

    [Column("Deleted_Original", TypeName = "datetime")]
    public DateTime? DeletedOriginal { get; set; }

    public bool IsBeforeRowAdded { get; set; }

    public bool CanUseOnPackages { get; set; }

    public bool EnableBalanceCheckout { get; set; }

    public bool IncludeInSearch { get; set; }

    [ForeignKey("ClientKey")]
    [InverseProperty("ClientLocationDiscounts")]
    public virtual Client ClientKeyNavigation { get; set; }

    [InverseProperty("ClientLocationDiscountKeyNavigation")]
    public virtual ICollection<ClientLocationDiscountTime> ClientLocationDiscountTimes { get; set; } = new List<ClientLocationDiscountTime>();

    [InverseProperty("ClientLocationDiscountKeyNavigation")]
    public virtual ICollection<ClientLocationEntityHierarchyDiscount> ClientLocationEntityHierarchyDiscounts { get; set; } = new List<ClientLocationEntityHierarchyDiscount>();

    [ForeignKey("ClientLocationKey")]
    [InverseProperty("ClientLocationDiscounts")]
    public virtual ClientLocation ClientLocationKeyNavigation { get; set; }

    [InverseProperty("ClientLocationDiscountKeyNavigation")]
    public virtual ICollection<ClientLocationProductDiscount> ClientLocationProductDiscounts { get; set; } = new List<ClientLocationProductDiscount>();

    [ForeignKey("DiscountApplicationTypeKey")]
    [InverseProperty("ClientLocationDiscounts")]
    public virtual DiscountApplicationType DiscountApplicationTypeKeyNavigation { get; set; }

    [ForeignKey("DiscountTypeKey")]
    [InverseProperty("ClientLocationDiscounts")]
    public virtual DiscountType DiscountTypeKeyNavigation { get; set; }

    [InverseProperty("ClientLocationDiscountKeyNavigation")]
    public virtual ICollection<History_ClientLocationEntityHierarchyDiscount> History_ClientLocationEntityHierarchyDiscounts { get; set; } = new List<History_ClientLocationEntityHierarchyDiscount>();
}