﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Additive_DB_Refresh.Models;

[Table("Products", Schema = "History")]
[Index("ProductHistoryKey", "ProductKey", Name = "uq_History_Products", IsUnique = true)]
public partial class History_Product
{
    [Key]
    public int ProductHistoryKey { get; set; }

    public int ProductKey { get; set; }

    public short? ProductCategoryKey { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string ProductCode { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string ProductName { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string ProductDescription { get; set; }

    public short? ReorderLevel { get; set; }

    public int? ReorderQuantity { get; set; }

    [Column(TypeName = "money")]
    public decimal? PublishedPrice { get; set; }

    [Column(TypeName = "money")]
    public decimal? MinimumPrice { get; set; }

    public long? ProductColorKey { get; set; }

    public short? ProductSizeKey { get; set; }

    [Column("SKU")]
    [StringLength(50)]
    [Unicode(false)]
    public string Sku { get; set; }

    public short? Sequence { get; set; }

    public bool? Taxable { get; set; }

    public bool? ManageStock { get; set; }

    public bool? IsSimpleProduct { get; set; }

    public bool? UseColor { get; set; }

    public bool? UseSize { get; set; }

    public long? ProductFlavorKey { get; set; }

    public bool? UseFlavor { get; set; }

    public bool? HasMediumImage { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string PhotoVersion { get; set; }

    [StringLength(4000)]
    public string ParentProductKey { get; set; }

    public bool? IsGiftCard { get; set; }

    public bool? IsTip { get; set; }

    public bool? EnableAddOn { get; set; }

    [StringLength(250)]
    public string AddOnName { get; set; }

    public string AddOnDescription { get; set; }

    [Column(TypeName = "money")]
    public decimal? AddOnDiscount { get; set; }

    [Column(TypeName = "decimal(19, 5)")]
    public decimal? Cost { get; set; }

    public bool? AddOnIsRequired { get; set; }

    public bool? AddOnRequiredPerPerson { get; set; }

    public bool? IsAddOnLimitedByTime { get; set; }

    public short? AddOnTimeLimitQuantity { get; set; }

    public DateTime DateFrom { get; set; }

    public DateTime? DateTo { get; set; }

    public bool IsCurrent { get; set; }

    public bool? IsTransportation { get; set; }

    [ForeignKey("ProductKey")]
    [InverseProperty("History_Products")]
    public virtual Merchandise_Product ProductKeyNavigation { get; set; }
}