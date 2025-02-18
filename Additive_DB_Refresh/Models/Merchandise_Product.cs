﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Additive_DB_Refresh.Models;

[Table("Products", Schema = "Merchandise")]
[Index("ClientKey", "ClientLocationKey", "DateDeleted", Name = "ix2Merchandise_Products")]
public partial class Merchandise_Product
{
    [Key]
    public int ProductKey { get; set; }

    public short? ProductCategoryKey { get; set; }

    public int ClientKey { get; set; }

    public int? ClientLocationKey { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string ProductCode { get; set; }

    [Required]
    [StringLength(255)]
    [Unicode(false)]
    public string ProductName { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string ProductDescription { get; set; }

    public short? ReorderLevel { get; set; }

    public int? ReorderQuantity { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime RowAdded { get; set; }

    [Column(TypeName = "money")]
    public decimal? PublishedPrice { get; set; }

    [Column(TypeName = "money")]
    public decimal? MinimumPrice { get; set; }

    public bool? IsTopLevel { get; set; }

    public long? ProductColorKey { get; set; }

    public short? ProductSizeKey { get; set; }

    [Column("SKU")]
    [StringLength(50)]
    [Unicode(false)]
    public string Sku { get; set; }

    public bool IsActive { get; set; }

    public long? ClientLoginKey { get; set; }

    public short Sequence { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DateDeleted { get; set; }

    public bool Taxable { get; set; }

    public bool ManageStock { get; set; }

    public bool IsSimpleProduct { get; set; }

    public bool UseColor { get; set; }

    public bool UseSize { get; set; }

    public long? ProductFlavorKey { get; set; }

    public bool UseFlavor { get; set; }

    public short? SortIndex { get; set; }

    public bool HasMediumImage { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string PhotoVersion { get; set; }

    public bool IsGiftCard { get; set; }

    public bool IsTip { get; set; }

    public bool EnableAddOn { get; set; }

    [StringLength(250)]
    public string AddOnName { get; set; }

    public string AddOnDescription { get; set; }

    [Column(TypeName = "money")]
    public decimal? AddOnDiscount { get; set; }

    [Column(TypeName = "decimal(19, 5)")]
    public decimal? Cost { get; set; }

    public bool AddOnIsRequired { get; set; }

    public bool AddOnRequiredPerPerson { get; set; }

    public bool IsAddOnLimitedByTime { get; set; }

    public short? AddOnTimeLimitQuantity { get; set; }

    [StringLength(4000)]
    public string ParentProductKey { get; set; }

    [StringLength(250)]
    public string FromPrice { get; set; }

    public bool UseFromPrice { get; set; }

    public bool IsRentalItem { get; set; }

    public int? RentalLowLevel { get; set; }

    public bool IsConsentRequired { get; set; }

    [Column("TrackIndividualSKUs")]
    public bool TrackIndividualSkus { get; set; }

    public bool IsTransportation { get; set; }

    public bool AddOnDefaultToParticipantCount { get; set; }

    [ForeignKey("ClientKey")]
    [InverseProperty("Merchandise_Products")]
    public virtual Client ClientKeyNavigation { get; set; }

    [InverseProperty("ProductKeyNavigation")]
    public virtual ICollection<ClientLocationAddOn> ClientLocationAddOns { get; set; } = new List<ClientLocationAddOn>();

    [InverseProperty("ProductKeyNavigation")]
    public virtual ICollection<ClientLocationItemDirectory> ClientLocationItemDirectories { get; set; } = new List<ClientLocationItemDirectory>();

    [ForeignKey("ClientLocationKey")]
    [InverseProperty("Merchandise_Products")]
    public virtual ClientLocation ClientLocationKeyNavigation { get; set; }

    [InverseProperty("ProductKeyNavigation")]
    public virtual ICollection<ClientLocationProductDiscount> ClientLocationProductDiscounts { get; set; } = new List<ClientLocationProductDiscount>();

    [ForeignKey("ClientLoginKey")]
    [InverseProperty("Merchandise_Products")]
    public virtual ClientLogin ClientLoginKeyNavigation { get; set; }

    [InverseProperty("ProductKeyNavigation")]
    public virtual ICollection<FeeAssociation> FeeAssociations { get; set; } = new List<FeeAssociation>();

    [InverseProperty("ProductKeyNavigation")]
    public virtual ICollection<History_ProductVariablePrice> History_ProductVariablePrices { get; set; } = new List<History_ProductVariablePrice>();

    [InverseProperty("ProductKeyNavigation")]
    public virtual ICollection<History_Product> History_Products { get; set; } = new List<History_Product>();

    [InverseProperty("ProductKeyNavigation")]
    public virtual Merchandise_Inventory Merchandise_Inventory { get; set; }

    [InverseProperty("ProductKeyNavigation")]
    public virtual ICollection<Merchandise_InventoryChangeLog> Merchandise_InventoryChangeLogs { get; set; } = new List<Merchandise_InventoryChangeLog>();

    [InverseProperty("ProductKeyNavigation")]
    public virtual ICollection<Merchandise_ProductAddOnEntityHierarchy> Merchandise_ProductAddOnEntityHierarchies { get; set; } = new List<Merchandise_ProductAddOnEntityHierarchy>();

    [InverseProperty("ProductKeyNavigation")]
    public virtual ICollection<Merchandise_ProductCategoryAssociation> Merchandise_ProductCategoryAssociations { get; set; } = new List<Merchandise_ProductCategoryAssociation>();

    [InverseProperty("ProductKeyNavigation")]
    public virtual ICollection<Merchandise_ProductRentalItem> Merchandise_ProductRentalItems { get; set; } = new List<Merchandise_ProductRentalItem>();

    [InverseProperty("ProductKeyNavigation")]
    public virtual ICollection<Merchandise_ProductVariablePrice> Merchandise_ProductVariablePrices { get; set; } = new List<Merchandise_ProductVariablePrice>();

    [InverseProperty("ProductKeyNavigation")]
    public virtual ICollection<PackageDetail> PackageDetails { get; set; } = new List<PackageDetail>();

    [ForeignKey("ProductCategoryKey")]
    [InverseProperty("Merchandise_Products")]
    public virtual Merchandise_ProductCategory ProductCategoryKeyNavigation { get; set; }

    [ForeignKey("ProductColorKey")]
    [InverseProperty("Merchandise_Products")]
    public virtual Merchandise_ProductColor ProductColorKeyNavigation { get; set; }

    [ForeignKey("ProductFlavorKey")]
    [InverseProperty("Merchandise_Products")]
    public virtual Merchandise_ProductFlavor ProductFlavorKeyNavigation { get; set; }

    [ForeignKey("ProductSizeKey")]
    [InverseProperty("Merchandise_Products")]
    public virtual Merchandise_ProductSize ProductSizeKeyNavigation { get; set; }
}