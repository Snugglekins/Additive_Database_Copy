﻿#nullable disable
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AnalyzerTest.Models;

[Index("LocationCode", Name = "UQ_PhotoLocationCode", IsUnique = true)]
[Index("ClientKey", Name = "ix2ClientLocations")]
public partial class ClientLocationWoGeneratedTag
{
    [Key]
    public int ClientLocationKey { get; set; }

    public int ClientKey { get; set; }

    [Required]
    [StringLength(500)]
    public string TimeZone { get; set; }

    public bool ObserveDaylightSavingTime { get; set; }

    [Required]
    public string Name { get; set; }

    public short AppointmentCompletionWindow { get; set; }

    public bool Active { get; set; }

    [StringLength(50)]
    public string PublicPhoneNumber { get; set; }

    public short? ChargeBalanceDays { get; set; }

    public string ReceiptHeaderHtml { get; set; }

    public string ReceiptFooterHtml { get; set; }

    public string ReceiptLogoBase64 { get; set; }

    [Column("SnapSportzID")]
    [StringLength(50)]
    public string SnapSportzId { get; set; }

    [Column("UTCOffset", TypeName = "datetime")]
    public DateTime Utcoffset { get; set; }

    [StringLength(50)]
    public string LocationCode { get; set; }

    [Column(TypeName = "numeric(12, 5)")]
    public decimal? TransportationTimeSlotBookingWindow { get; set; }

    public Guid? AddressKey { get; set; }

    [Required]
    public string InternalName { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string PhotoVersion { get; set; }

    [Column("ClientLocationGUID")]
    public Guid ClientLocationGuid { get; set; }

    public short MerchandiseCompletionWindow { get; set; }

    public string ConfirmationMessageFooter { get; set; }

    [StringLength(250)]
    public string SquareAccessToken { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? SquareAccessTokenExpiresAt { get; set; }

    [StringLength(50)]
    public string SquareLocationId { get; set; }

    [Column("SquareSDKTypeKey")]
    public byte SquareSdktypeKey { get; set; }

    public byte SquareCurrencyCode { get; set; }

    public byte AdultAge { get; set; }

    public bool ConsentRequiresSignature { get; set; }

    public int CountryKey { get; set; }

    public string Website { get; set; }

    [Column("GATrackingID")]
    [StringLength(50)]
    public string GatrackingId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string LightLogoVersion { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string DarkLogoVersion { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string WatermarkVersion { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string KioskBgVersion { get; set; }

    public bool AmountPaymentRequireManager { get; set; }

    public bool AmountRefundRequireManager { get; set; }

    public string AddressLine1 { get; set; }

    public string AddressLine2 { get; set; }

    public string AddressLine3 { get; set; }

    public string City { get; set; }

    public string State { get; set; }

    public string PostalCode { get; set; }

    public string Country { get; set; }

    public int? HotelSearchRadius { get; set; }

    public bool EnablePostVisitEmail { get; set; }

    [StringLength(50)]
    public string LocalizeJsKey { get; set; }

    [Column("FacebookPixelID")]
    [StringLength(50)]
    public string FacebookPixelId { get; set; }

    [StringLength(250)]
    public string MailChimpAccessToken { get; set; }

    [Column("MailChimpDC")]
    [StringLength(10)]
    public string MailChimpDc { get; set; }

    [StringLength(250)]
    public string MailChimpLoginUrl { get; set; }

    [StringLength(250)]
    public string MailChimpApiEndpoint { get; set; }

    public string BillingAddress { get; set; }

    public string CustomCss { get; set; }

    public string CustomJs { get; set; }

    public string GroupEmailHeader { get; set; }

    public string GroupEmailFooter { get; set; }

    public bool ShowEmailSignature { get; set; }

    public bool FeesCollectedRealtime { get; set; }

    [StringLength(250)]
    public string FacebookAccessToken { get; set; }

    public long? FacebookAccessTokenExpiresAt { get; set; }

    [Column("FacebookOfflineEventSetID")]
    [StringLength(250)]
    public string FacebookOfflineEventSetId { get; set; }

    [Column("FacebookBusinessID")]
    [StringLength(250)]
    public string FacebookBusinessId { get; set; }

    [Column("FacebookAdAccountID")]
    [StringLength(250)]
    public string FacebookAdAccountId { get; set; }

    public bool HideFeeBreakdown { get; set; }

    [Column("POSPrinterFooter")]
    public string PosprinterFooter { get; set; }

    public bool ShowInStoreToggle { get; set; }

    [Column("SojernSearchPixelID")]
    [StringLength(250)]
    public string SojernSearchPixelId { get; set; }

    [Column("SojernCartPixelID")]
    [StringLength(250)]
    public string SojernCartPixelId { get; set; }

    [Column("SojernConversionPixelID")]
    [StringLength(250)]
    public string SojernConversionPixelId { get; set; }

    public bool EnableSojern { get; set; }

    [StringLength(250)]
    public string GoogleAdConversionId { get; set; }

    [StringLength(250)]
    public string GoogleAdConversionLabelId { get; set; }

    public bool EnableGoogleAdConversions { get; set; }

    [Column(TypeName = "numeric(18, 14)")]
    public decimal? Latitude { get; set; }

    [Column(TypeName = "numeric(18, 14)")]
    public decimal? Longitude { get; set; }

    public bool UseModalForSimpleProduct { get; set; }

    [Column("TOMISSiteID")]
    [StringLength(250)]
    public string TomissiteId { get; set; }

    public bool RequireEmailOptIn { get; set; }

    public bool EnableGiftCards { get; set; }

    public bool SellGiftCardsOnline { get; set; }

    public bool MultiLocationGiftCards { get; set; }

    public string GiftCardDescription { get; set; }

    public bool DisablePromoCodes { get; set; }

    [Column("EnableTOMISChatBot")]
    public bool EnableTomischatBot { get; set; }

    public bool EnableOnlineDeposits { get; set; }

    [Column(TypeName = "money")]
    public decimal? DepositMinOrderTotal { get; set; }

    public string PackageUpgradeDescription { get; set; }

    [StringLength(250)]
    public string SquareRefreshToken { get; set; }

    [Column(TypeName = "numeric(12, 7)")]
    public decimal? DepositPercent { get; set; }

    [StringLength(250)]
    public string GoogleAccessToken { get; set; }

    [StringLength(250)]
    public string GoogleRefreshToken { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? GoogleAccessTokenExpiresAt { get; set; }

    public bool EnablePostVisitSms { get; set; }

    public bool IncludePostVisitLinkInSms { get; set; }

    public string PostVisitSmsText { get; set; }

    [StringLength(20)]
    public string SpecificCulture { get; set; }

    public bool UseItemCategories { get; set; }

    public bool UseClientLocationItemDirectories { get; set; }

    public bool HideMapFromBookingProcess { get; set; }

    [StringLength(250)]
    public string BingMarketingId { get; set; }

    public bool IsFirstNameOptional { get; set; }

    public bool IsLastNameOptional { get; set; }

    public bool IsPhoneNumberOptional { get; set; }

    public bool IsHeardAboutUsOptional { get; set; }

    public bool IsEmailOptional { get; set; }

    public bool DisableApplePayOnline { get; set; }

    public bool DisableGooglePayOnline { get; set; }

    public bool DisableTapPayments { get; set; }

    public bool EnableBingMarketing { get; set; }

    public bool CollectMailingAddressForGiftCards { get; set; }

    [StringLength(250)]
    public string ExperiencesLabel { get; set; }

    public bool AllRefundsRequireManager { get; set; }

    public bool TurnOffPasscodeForGuideAssign { get; set; }

    [StringLength(50)]
    public string MailChimpListId { get; set; }

    public bool TurnOffPasscodes { get; set; }

    [StringLength(50)]
    public string TwilioNumber { get; set; }

    public bool EnableTexting { get; set; }

    [Column("TwilioAccountSID")]
    [StringLength(50)]
    public string TwilioAccountSid { get; set; }

    [StringLength(50)]
    public string TwilioAuthToken { get; set; }

    [Column("TwilioPhoneNumberSID")]
    [StringLength(50)]
    public string TwilioPhoneNumberSid { get; set; }

    public int? InntopiaClientId { get; set; }

    public Guid? RedeamSupplierId { get; set; }

    [Column("GA4TrackingID")]
    [StringLength(50)]
    public string Ga4trackingId { get; set; }

    public string CancellationEmailHeader { get; set; }

    [Column("CollectDINInfo")]
    public bool CollectDininfo { get; set; }

    public bool EnableRentals { get; set; }

    public bool HideTipsFromGuides { get; set; }

    public bool DisableEditingPastActivity { get; set; }

    public bool EnableWaiverExpiration { get; set; }

    public short? WaiverValidMonths { get; set; }

    public bool ShowBookingTimer { get; set; }

    public bool EnableOrderLevelTransportation { get; set; }

    public string TransportRequirementsLabel { get; set; }

    public string TransportRequirements { get; set; }

    public byte BookLogoTypeKey { get; set; }

    public byte GiftCardLogoTypeKey { get; set; }

}