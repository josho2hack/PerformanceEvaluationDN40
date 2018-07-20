using System;
using System.Web;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Drawing.Text;
using System.Configuration;
using System.Data;
using DevExpress.XtraRichEdit;
//using pes.Module.Web.Reports;

/// <summary>
/// Summary description for OwnerReport
/// </summary>
public class OwnerReport : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    protected DevExpress.Persistent.Base.ReportsV2.CollectionDataSource collectionDataSource1;
    private GroupHeaderBand GroupHeader1;
    private XRTable xrTable5;
    private XRTableRow xrTableRow7;
    private XRTableCell xrTableCell35;
    private XRTableCell xrTableCell36;
    private XRTableCell xrTableCell37;
    private XRTableCell xrTableCell38;
    private XRTableCell xrTableCell39;
    private XRTableCell xrTableCell40;
    private CalculatedField PoE_Title;
    private CalculatedField weight;
    private PageHeaderBand PageHeader;
    private ReportFooterBand ReportFooter;
    private CalculatedField StartM;
    private CalculatedField StopM;
    private CalculatedField weigthE;
    private XRControlStyle customFontStyle;
    private XRControlStyle customFontStyleB;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTableCell1;
    private XRTableCell xrTableCell2;
    private XRTableCell xrTableCell3;
    private XRTableCell xrTableCell20;
    private XRTableCell xrTableCell21;
    private XRTableCell xrTableCell22;
    private XRTable xrTable3;
    private XRTableRow xrTableRow5;
    private XRTableCell xrTableCell23;
    private XRTableCell xrTableCell24;
    private XRTableCell xrTableCell25;
    private XRTableCell xrTableCell26;
    private XRTableCell xrTableCell27;
    private XRTableCell xrTableCell28;
    private XRTable xrTable6;
    private XRTableRow xrTableRow8;
    private XRTableCell xrTableCell41;
    private XRTableCell xrTableCell42;
    private XRTableCell xrTableCell43;
    private XRTableCell xrTableCell44;
    private XRTableRow xrTableRow10;
    private XRTableCell xrTableCell53;
    private XRTableCell xrTableCell54;
    private XRTableCell xrTableCell55;
    private XRTableCell xrTableCell56;
    private XRTableCell xrTableCell57;
    private XRTableCell xrTableCell58;
    private XRTableRow xrTableRow9;
    private XRTableCell xrTableCell45;
    private XRTableCell xrTableCell46;
    private XRTableCell xrTableCell47;
    private XRTableCell xrTableCell48;
    private XRTableCell xrTableCell49;
    private XRTableCell xrTableCell50;
    private XRLabel xrLabel7;
    private XRLabel xrLabel6;
    private XRLabel xrLabel4;
    private XRLabel xrLabel5;
    private XRLabel xrLabel3;
    private XRLabel xrLabel2;
    private XRLabel xrLabel1;
    private XRTable xrTable2;
    private XRTableRow xrTableRow2;
    private XRTableCell xrTableCell4;
    private XRTableCell xrTableCell5;
    private XRTableCell xrTableCell6;
    private XRTableCell xrTableCell7;
    private XRTableCell xrTableCell8;
    private XRTableCell xrTableCell9;
    private CalculatedField calSumPoE;
    private CalculatedField calSumWeightScore;
    private CalculatedField weightD;

    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public OwnerReport()
    {
        InitializeComponent();
        customFontStyle.Font = new Font(FontCollection.Families[0], 14F, FontStyle.Regular, GraphicsUnit.Point);
        customFontStyleB.Font = new Font(FontCollectionB.Families[0], 14F, FontStyle.Bold, GraphicsUnit.Point);
    }

    static PrivateFontCollection fontCollection;
    public static FontCollection FontCollection
    {
        get
        {
            if (fontCollection == null)
            {
                fontCollection = new PrivateFontCollection();
                fontCollection.AddFontFile(HttpContext.Current.Server.MapPath("~/Fonts/THSarabunIT๙.ttf"));
            }
            return fontCollection;
        }
    }
    static PrivateFontCollection fontCollectionB;
    public static FontCollection FontCollectionB
    {
        get
        {
            if (fontCollectionB == null)
            {
                fontCollectionB = new PrivateFontCollection();
                fontCollectionB.AddFontFile(HttpContext.Current.Server.MapPath("~/Fonts/THSarabunIT๙ Bold.ttf"));
            }
            return fontCollectionB;
        }
    }

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary2 = new DevExpress.XtraReports.UI.XRSummary();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OwnerReport));
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow5 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell23 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell24 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell25 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell26 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell27 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell28 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell20 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell21 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell22 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable5 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow7 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell35 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell36 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell37 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell38 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell39 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell40 = new DevExpress.XtraReports.UI.XRTableCell();
            this.PoE_Title = new DevExpress.XtraReports.UI.CalculatedField();
            this.weight = new DevExpress.XtraReports.UI.CalculatedField();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTable6 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow8 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell41 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell42 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell43 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell44 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow9 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell45 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell46 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell47 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell48 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell49 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell50 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow10 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell53 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell54 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell55 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell56 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell57 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell58 = new DevExpress.XtraReports.UI.XRTableCell();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.StartM = new DevExpress.XtraReports.UI.CalculatedField();
            this.StopM = new DevExpress.XtraReports.UI.CalculatedField();
            this.weigthE = new DevExpress.XtraReports.UI.CalculatedField();
            this.customFontStyle = new DevExpress.XtraReports.UI.XRControlStyle();
            this.customFontStyleB = new DevExpress.XtraReports.UI.XRControlStyle();
            this.calSumPoE = new DevExpress.XtraReports.UI.CalculatedField();
            this.calSumWeightScore = new DevExpress.XtraReports.UI.CalculatedField();
            this.weightD = new DevExpress.XtraReports.UI.CalculatedField();
            this.collectionDataSource1 = new DevExpress.Persistent.Base.ReportsV2.CollectionDataSource();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.collectionDataSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2});
            this.Detail.Dpi = 254F;
            this.Detail.HeightF = 63.5F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.Detail.SortFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("PointOfEvaluation.No", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending),
            new DevExpress.XtraReports.UI.GroupField("SubPointOfEvaluation.No", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrTable2
            // 
            this.xrTable2.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable2.Dpi = 254F;
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrTable2.SizeF = new System.Drawing.SizeF(2000F, 63.5F);
            this.xrTable2.StylePriority.UseBorders = false;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell4,
            this.xrTableCell5,
            this.xrTableCell6,
            this.xrTableCell7,
            this.xrTableCell8,
            this.xrTableCell9});
            this.xrTableRow2.Dpi = 254F;
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Weight = 1D;
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.Dpi = 254F;
            this.xrTableCell4.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[PoE_Title]")});
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
            this.xrTableCell4.StyleName = "customFontStyle";
            this.xrTableCell4.StylePriority.UsePadding = false;
            this.xrTableCell4.Weight = 3.6102110968146421D;
            // 
            // xrTableCell5
            // 
            this.xrTableCell5.Dpi = 254F;
            this.xrTableCell5.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[PointOfEvaluation.UnitOfPoint.Title]")});
            this.xrTableCell5.Name = "xrTableCell5";
            this.xrTableCell5.StyleName = "customFontStyle";
            this.xrTableCell5.StylePriority.UseTextAlignment = false;
            this.xrTableCell5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell5.Weight = 0.4512766388718652D;
            // 
            // xrTableCell6
            // 
            this.xrTableCell6.Dpi = 254F;
            this.xrTableCell6.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[weight]")});
            this.xrTableCell6.Name = "xrTableCell6";
            this.xrTableCell6.StyleName = "customFontStyle";
            this.xrTableCell6.StylePriority.UseTextAlignment = false;
            this.xrTableCell6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell6.TextFormatString = "{0:#.00}";
            this.xrTableCell6.Weight = 0.451276272660905D;
            // 
            // xrTableCell7
            // 
            this.xrTableCell7.Dpi = 254F;
            this.xrTableCell7.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Result]")});
            this.xrTableCell7.Name = "xrTableCell7";
            this.xrTableCell7.StyleName = "customFontStyle";
            this.xrTableCell7.StylePriority.UseTextAlignment = false;
            this.xrTableCell7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell7.TextFormatString = "{0:#,#.000}";
            this.xrTableCell7.Weight = 0.54153152719308628D;
            // 
            // xrTableCell8
            // 
            this.xrTableCell8.Dpi = 254F;
            this.xrTableCell8.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Score]")});
            this.xrTableCell8.Name = "xrTableCell8";
            this.xrTableCell8.StyleName = "customFontStyle";
            this.xrTableCell8.StylePriority.UseTextAlignment = false;
            this.xrTableCell8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell8.TextFormatString = "{0:#,#.0000}";
            this.xrTableCell8.Weight = 0.4512762726609052D;
            // 
            // xrTableCell9
            // 
            this.xrTableCell9.Dpi = 254F;
            this.xrTableCell9.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[WeightScore]")});
            this.xrTableCell9.Name = "xrTableCell9";
            this.xrTableCell9.StyleName = "customFontStyle";
            this.xrTableCell9.StylePriority.UseTextAlignment = false;
            this.xrTableCell9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell9.TextFormatString = "{0:#,#.0000}";
            this.xrTableCell9.Weight = 0.49442819179859565D;
            // 
            // TopMargin
            // 
            this.TopMargin.Dpi = 254F;
            this.TopMargin.HeightF = 100F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.Dpi = 254F;
            this.BottomMargin.HeightF = 50F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // GroupHeader1
            // 
            this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable3,
            this.xrTable1});
            this.GroupHeader1.Dpi = 254F;
            this.GroupHeader1.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("PointOfEvaluation.Evaluation.Title", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            this.GroupHeader1.HeightF = 127F;
            this.GroupHeader1.Name = "GroupHeader1";
            // 
            // xrTable3
            // 
            this.xrTable3.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable3.Dpi = 254F;
            this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 63.5F);
            this.xrTable3.Name = "xrTable3";
            this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow5});
            this.xrTable3.SizeF = new System.Drawing.SizeF(2000F, 63.5F);
            this.xrTable3.StylePriority.UseBorders = false;
            // 
            // xrTableRow5
            // 
            this.xrTableRow5.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell23,
            this.xrTableCell24,
            this.xrTableCell25,
            this.xrTableCell26,
            this.xrTableCell27,
            this.xrTableCell28});
            this.xrTableRow5.Dpi = 254F;
            this.xrTableRow5.Name = "xrTableRow5";
            this.xrTableRow5.Weight = 1D;
            // 
            // xrTableCell23
            // 
            this.xrTableCell23.Dpi = 254F;
            this.xrTableCell23.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[PointOfEvaluation.Evaluation.Title]")});
            this.xrTableCell23.Name = "xrTableCell23";
            this.xrTableCell23.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
            this.xrTableCell23.StyleName = "customFontStyleB";
            this.xrTableCell23.StylePriority.UsePadding = false;
            this.xrTableCell23.StylePriority.UseTextAlignment = false;
            this.xrTableCell23.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell23.TextFormatString = "* {0}";
            this.xrTableCell23.Weight = 3.6102110968146421D;
            // 
            // xrTableCell24
            // 
            this.xrTableCell24.Dpi = 254F;
            this.xrTableCell24.Name = "xrTableCell24";
            this.xrTableCell24.StyleName = "customFontStyle";
            this.xrTableCell24.StylePriority.UseTextAlignment = false;
            this.xrTableCell24.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell24.Weight = 0.4512766388718652D;
            // 
            // xrTableCell25
            // 
            this.xrTableCell25.Dpi = 254F;
            this.xrTableCell25.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[weigthE]")});
            this.xrTableCell25.Name = "xrTableCell25";
            this.xrTableCell25.StyleName = "customFontStyleB";
            this.xrTableCell25.StylePriority.UseTextAlignment = false;
            this.xrTableCell25.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell25.TextFormatString = "{0:#}";
            this.xrTableCell25.Weight = 0.451276272660905D;
            // 
            // xrTableCell26
            // 
            this.xrTableCell26.Dpi = 254F;
            this.xrTableCell26.Name = "xrTableCell26";
            this.xrTableCell26.StyleName = "customFontStyle";
            this.xrTableCell26.StylePriority.UseTextAlignment = false;
            this.xrTableCell26.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell26.Weight = 0.54153152719308628D;
            // 
            // xrTableCell27
            // 
            this.xrTableCell27.Dpi = 254F;
            this.xrTableCell27.Name = "xrTableCell27";
            this.xrTableCell27.StyleName = "customFontStyle";
            this.xrTableCell27.StylePriority.UseTextAlignment = false;
            this.xrTableCell27.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell27.Weight = 0.4512762726609052D;
            // 
            // xrTableCell28
            // 
            this.xrTableCell28.Dpi = 254F;
            this.xrTableCell28.Name = "xrTableCell28";
            this.xrTableCell28.StyleName = "customFontStyle";
            this.xrTableCell28.StylePriority.UseTextAlignment = false;
            this.xrTableCell28.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell28.Weight = 0.49442819179859565D;
            // 
            // xrTable1
            // 
            this.xrTable1.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable1.Dpi = 254F;
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0.0003229777F, 0F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTable1.SizeF = new System.Drawing.SizeF(2000F, 63.5F);
            this.xrTable1.StylePriority.UseBorders = false;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1,
            this.xrTableCell2,
            this.xrTableCell3,
            this.xrTableCell20,
            this.xrTableCell21,
            this.xrTableCell22});
            this.xrTableRow1.Dpi = 254F;
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 1D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.Dpi = 254F;
            this.xrTableCell1.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[PointOfEvaluation.Evaluation.Dimension.Title]")});
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
            this.xrTableCell1.StyleName = "customFontStyleB";
            this.xrTableCell1.StylePriority.UsePadding = false;
            this.xrTableCell1.StylePriority.UseTextAlignment = false;
            this.xrTableCell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell1.Weight = 3.6102110968146421D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.Dpi = 254F;
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.StyleName = "customFontStyle";
            this.xrTableCell2.StylePriority.UseTextAlignment = false;
            this.xrTableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell2.TextFormatString = "{0:#}";
            this.xrTableCell2.Weight = 0.4512766388718652D;
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.Dpi = 254F;
            this.xrTableCell3.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[weightD]")});
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.StyleName = "customFontStyleB";
            this.xrTableCell3.StylePriority.UseTextAlignment = false;
            this.xrTableCell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell3.TextFormatString = "{0:#}";
            this.xrTableCell3.Weight = 0.451276272660905D;
            // 
            // xrTableCell20
            // 
            this.xrTableCell20.Dpi = 254F;
            this.xrTableCell20.Name = "xrTableCell20";
            this.xrTableCell20.StyleName = "customFontStyle";
            this.xrTableCell20.StylePriority.UseTextAlignment = false;
            this.xrTableCell20.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell20.Weight = 0.54153152719308628D;
            // 
            // xrTableCell21
            // 
            this.xrTableCell21.Dpi = 254F;
            this.xrTableCell21.Name = "xrTableCell21";
            this.xrTableCell21.StyleName = "customFontStyle";
            this.xrTableCell21.StylePriority.UseTextAlignment = false;
            this.xrTableCell21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell21.Weight = 0.4512762726609052D;
            // 
            // xrTableCell22
            // 
            this.xrTableCell22.Dpi = 254F;
            this.xrTableCell22.Name = "xrTableCell22";
            this.xrTableCell22.StyleName = "customFontStyle";
            this.xrTableCell22.StylePriority.UseTextAlignment = false;
            this.xrTableCell22.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell22.Weight = 0.49442819179859565D;
            // 
            // xrTable5
            // 
            this.xrTable5.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable5.Dpi = 254F;
            this.xrTable5.LocationFloat = new DevExpress.Utils.PointFloat(8.074442E-05F, 0F);
            this.xrTable5.Name = "xrTable5";
            this.xrTable5.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow7});
            this.xrTable5.SizeF = new System.Drawing.SizeF(2000F, 63.5F);
            this.xrTable5.StylePriority.UseBorders = false;
            // 
            // xrTableRow7
            // 
            this.xrTableRow7.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell35,
            this.xrTableCell36,
            this.xrTableCell37,
            this.xrTableCell38,
            this.xrTableCell39,
            this.xrTableCell40});
            this.xrTableRow7.Dpi = 254F;
            this.xrTableRow7.Name = "xrTableRow7";
            this.xrTableRow7.Weight = 1D;
            // 
            // xrTableCell35
            // 
            this.xrTableCell35.Dpi = 254F;
            this.xrTableCell35.Name = "xrTableCell35";
            this.xrTableCell35.StyleName = "customFontStyleB";
            this.xrTableCell35.Text = "รวม";
            this.xrTableCell35.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell35.Weight = 3.6102110968146421D;
            // 
            // xrTableCell36
            // 
            this.xrTableCell36.Dpi = 254F;
            this.xrTableCell36.Name = "xrTableCell36";
            this.xrTableCell36.Weight = 0.4512766388718652D;
            // 
            // xrTableCell37
            // 
            this.xrTableCell37.Dpi = 254F;
            this.xrTableCell37.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "sumSum([PointOfEvaluation].[Weight])")});
            this.xrTableCell37.Name = "xrTableCell37";
            this.xrTableCell37.StyleName = "customFontStyle";
            xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Page;
            this.xrTableCell37.Summary = xrSummary1;
            this.xrTableCell37.TextFormatString = "{0:#}";
            this.xrTableCell37.Weight = 0.451276272660905D;
            // 
            // xrTableCell38
            // 
            this.xrTableCell38.Dpi = 254F;
            this.xrTableCell38.Name = "xrTableCell38";
            this.xrTableCell38.Weight = 0.54153152719308628D;
            // 
            // xrTableCell39
            // 
            this.xrTableCell39.Dpi = 254F;
            this.xrTableCell39.Name = "xrTableCell39";
            this.xrTableCell39.Weight = 0.4512762726609052D;
            // 
            // xrTableCell40
            // 
            this.xrTableCell40.Dpi = 254F;
            this.xrTableCell40.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[calSumWeightScore]")});
            this.xrTableCell40.Name = "xrTableCell40";
            this.xrTableCell40.StyleName = "customFontStyleB";
            xrSummary2.FormatString = "{0:#,#.0000}";
            xrSummary2.IgnoreNullValues = true;
            this.xrTableCell40.Summary = xrSummary2;
            this.xrTableCell40.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell40.TextFormatString = "{0:#,#.0000}";
            this.xrTableCell40.Weight = 0.49442819179859565D;
            // 
            // PoE_Title
            // 
            this.PoE_Title.Expression = "Iif(IsNull([SubPointOfEvaluation]), [PointOfEvaluation.Title] , [SubPointOfEvalua" +
    "tion.Title])";
            this.PoE_Title.Name = "PoE_Title";
            // 
            // weight
            // 
            this.weight.Expression = resources.GetString("weight.Expression");
            this.weight.Name = "weight";
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel7,
            this.xrLabel6,
            this.xrLabel4,
            this.xrLabel5,
            this.xrLabel3,
            this.xrLabel2,
            this.xrLabel1,
            this.xrTable6});
            this.PageHeader.Dpi = 254F;
            this.PageHeader.HeightF = 366.1833F;
            this.PageHeader.Name = "PageHeader";
            // 
            // xrLabel7
            // 
            this.xrLabel7.Dpi = 254F;
            this.xrLabel7.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[MainScore].[ERound].[Year]")});
            this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(1323.532F, 0F);
            this.xrLabel7.Name = "xrLabel7";
            this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel7.SizeF = new System.Drawing.SizeF(191.1351F, 58.42F);
            this.xrLabel7.StyleName = "customFontStyleB";
            this.xrLabel7.StylePriority.UseFont = false;
            this.xrLabel7.StylePriority.UseTextAlignment = false;
            this.xrLabel7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel6
            // 
            this.xrLabel6.Dpi = 254F;
            this.xrLabel6.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[MainScore].[ERound].[MonthStop]")});
            this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(1098.245F, 58.41995F);
            this.xrLabel6.Name = "xrLabel6";
            this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel6.SizeF = new System.Drawing.SizeF(191.1351F, 58.42F);
            this.xrLabel6.StyleName = "customFontStyleB";
            this.xrLabel6.StylePriority.UseFont = false;
            this.xrLabel6.StylePriority.UseTextAlignment = false;
            this.xrLabel6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel4
            // 
            this.xrLabel4.Dpi = 254F;
            this.xrLabel4.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[MainScore].[ERound].[MonthStart]")});
            this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(855.5101F, 58.41998F);
            this.xrLabel4.Name = "xrLabel4";
            this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel4.SizeF = new System.Drawing.SizeF(191.1351F, 58.42F);
            this.xrLabel4.StyleName = "customFontStyleB";
            this.xrLabel4.StylePriority.UseFont = false;
            this.xrLabel4.StylePriority.UseTextAlignment = false;
            this.xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel5
            // 
            this.xrLabel5.Dpi = 254F;
            this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(1046.645F, 58.41998F);
            this.xrLabel5.Name = "xrLabel5";
            this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel5.SizeF = new System.Drawing.SizeF(51.59973F, 58.42F);
            this.xrLabel5.StyleName = "customFontStyleB";
            this.xrLabel5.StylePriority.UseFont = false;
            this.xrLabel5.StylePriority.UseTextAlignment = false;
            this.xrLabel5.Text = "-";
            this.xrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel3
            // 
            this.xrLabel3.Dpi = 254F;
            this.xrLabel3.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[MainScore].[Office].[OfficeName]")});
            this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(0.0002906799F, 116.84F);
            this.xrLabel3.Name = "xrLabel3";
            this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel3.SizeF = new System.Drawing.SizeF(2000F, 58.42F);
            this.xrLabel3.StyleName = "customFontStyleB";
            this.xrLabel3.StylePriority.UseFont = false;
            this.xrLabel3.StylePriority.UseTextAlignment = false;
            this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel2
            // 
            this.xrLabel2.Dpi = 254F;
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(748.6654F, 58.41998F);
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(106.8447F, 58.42F);
            this.xrLabel2.StyleName = "customFontStyleB";
            this.xrLabel2.StylePriority.UseFont = false;
            this.xrLabel2.StylePriority.UseTextAlignment = false;
            this.xrLabel2.Text = "เดือน";
            this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel1
            // 
            this.xrLabel1.Dpi = 254F;
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(550.5452F, 0F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(772.9872F, 58.42F);
            this.xrLabel1.StyleName = "customFontStyleB";
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.StylePriority.UseTextAlignment = false;
            this.xrLabel1.Text = "คำรับรองการปฏิบัติราชการ ประจำปีงบประมาณ พ.ศ. ";
            this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTable6
            // 
            this.xrTable6.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable6.Dpi = 254F;
            this.xrTable6.LocationFloat = new DevExpress.Utils.PointFloat(0.0001614888F, 175.6833F);
            this.xrTable6.Name = "xrTable6";
            this.xrTable6.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow8,
            this.xrTableRow9,
            this.xrTableRow10});
            this.xrTable6.SizeF = new System.Drawing.SizeF(2000F, 190.5F);
            this.xrTable6.StylePriority.UseBorders = false;
            // 
            // xrTableRow8
            // 
            this.xrTableRow8.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell41,
            this.xrTableCell42,
            this.xrTableCell43,
            this.xrTableCell44});
            this.xrTableRow8.Dpi = 254F;
            this.xrTableRow8.Name = "xrTableRow8";
            this.xrTableRow8.Weight = 1D;
            // 
            // xrTableCell41
            // 
            this.xrTableCell41.Dpi = 254F;
            this.xrTableCell41.Name = "xrTableCell41";
            this.xrTableCell41.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
            this.xrTableCell41.RowSpan = 3;
            this.xrTableCell41.StyleName = "customFontStyleB";
            this.xrTableCell41.StylePriority.UsePadding = false;
            this.xrTableCell41.Text = "ตัวชี้วัดผลการปฏิบัติราชการ";
            this.xrTableCell41.Weight = 3.6102110968146421D;
            // 
            // xrTableCell42
            // 
            this.xrTableCell42.Dpi = 254F;
            this.xrTableCell42.Name = "xrTableCell42";
            this.xrTableCell42.RowSpan = 3;
            this.xrTableCell42.StyleName = "customFontStyleB";
            this.xrTableCell42.StylePriority.UseTextAlignment = false;
            this.xrTableCell42.Text = "หน่วยวัด";
            this.xrTableCell42.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell42.Weight = 0.4512766388718652D;
            // 
            // xrTableCell43
            // 
            this.xrTableCell43.Dpi = 254F;
            this.xrTableCell43.Name = "xrTableCell43";
            this.xrTableCell43.RowSpan = 3;
            this.xrTableCell43.StyleName = "customFontStyleB";
            this.xrTableCell43.StylePriority.UseTextAlignment = false;
            this.xrTableCell43.Text = "น้ำหนักร้อยละ";
            this.xrTableCell43.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell43.Weight = 0.451276272660905D;
            // 
            // xrTableCell44
            // 
            this.xrTableCell44.Dpi = 254F;
            this.xrTableCell44.Name = "xrTableCell44";
            this.xrTableCell44.StyleName = "customFontStyleB";
            this.xrTableCell44.StylePriority.UseTextAlignment = false;
            this.xrTableCell44.Text = "ผลการประเมินของหน่วยงาน";
            this.xrTableCell44.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell44.Weight = 1.4872359916525872D;
            // 
            // xrTableRow9
            // 
            this.xrTableRow9.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell45,
            this.xrTableCell46,
            this.xrTableCell47,
            this.xrTableCell48,
            this.xrTableCell49,
            this.xrTableCell50});
            this.xrTableRow9.Dpi = 254F;
            this.xrTableRow9.Name = "xrTableRow9";
            this.xrTableRow9.Weight = 1D;
            // 
            // xrTableCell45
            // 
            this.xrTableCell45.Dpi = 254F;
            this.xrTableCell45.Name = "xrTableCell45";
            this.xrTableCell45.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
            this.xrTableCell45.StylePriority.UsePadding = false;
            this.xrTableCell45.Text = "xrTableCell45";
            this.xrTableCell45.Weight = 3.6102110968146421D;
            // 
            // xrTableCell46
            // 
            this.xrTableCell46.Dpi = 254F;
            this.xrTableCell46.Name = "xrTableCell46";
            this.xrTableCell46.StylePriority.UseTextAlignment = false;
            this.xrTableCell46.Text = "xrTableCell46";
            this.xrTableCell46.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell46.Weight = 0.4512766388718652D;
            // 
            // xrTableCell47
            // 
            this.xrTableCell47.Dpi = 254F;
            this.xrTableCell47.Name = "xrTableCell47";
            this.xrTableCell47.StylePriority.UseTextAlignment = false;
            this.xrTableCell47.Text = "xrTableCell47";
            this.xrTableCell47.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell47.Weight = 0.451276272660905D;
            // 
            // xrTableCell48
            // 
            this.xrTableCell48.Dpi = 254F;
            this.xrTableCell48.Name = "xrTableCell48";
            this.xrTableCell48.RowSpan = 2;
            this.xrTableCell48.StyleName = "customFontStyleB";
            this.xrTableCell48.StylePriority.UseTextAlignment = false;
            this.xrTableCell48.Text = "ผลการดำเนินงาน";
            this.xrTableCell48.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell48.Weight = 0.54153152719308628D;
            // 
            // xrTableCell49
            // 
            this.xrTableCell49.Dpi = 254F;
            this.xrTableCell49.Name = "xrTableCell49";
            this.xrTableCell49.RowSpan = 2;
            this.xrTableCell49.StyleName = "customFontStyleB";
            this.xrTableCell49.StylePriority.UseTextAlignment = false;
            this.xrTableCell49.Text = "ค่าคะแนนที่ได้";
            this.xrTableCell49.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell49.Weight = 0.4512762726609052D;
            // 
            // xrTableCell50
            // 
            this.xrTableCell50.Dpi = 254F;
            this.xrTableCell50.Name = "xrTableCell50";
            this.xrTableCell50.RowSpan = 2;
            this.xrTableCell50.StyleName = "customFontStyleB";
            this.xrTableCell50.StylePriority.UseTextAlignment = false;
            this.xrTableCell50.Text = "ค่าคะแนนถ่วงน้ำหนัก";
            this.xrTableCell50.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell50.Weight = 0.49442819179859565D;
            // 
            // xrTableRow10
            // 
            this.xrTableRow10.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell53,
            this.xrTableCell54,
            this.xrTableCell55,
            this.xrTableCell56,
            this.xrTableCell57,
            this.xrTableCell58});
            this.xrTableRow10.Dpi = 254F;
            this.xrTableRow10.Name = "xrTableRow10";
            this.xrTableRow10.Weight = 1D;
            // 
            // xrTableCell53
            // 
            this.xrTableCell53.Dpi = 254F;
            this.xrTableCell53.Name = "xrTableCell53";
            this.xrTableCell53.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
            this.xrTableCell53.StylePriority.UsePadding = false;
            this.xrTableCell53.Text = "xrTableCell53";
            this.xrTableCell53.Weight = 3.6102110968146421D;
            // 
            // xrTableCell54
            // 
            this.xrTableCell54.Dpi = 254F;
            this.xrTableCell54.Name = "xrTableCell54";
            this.xrTableCell54.StylePriority.UseTextAlignment = false;
            this.xrTableCell54.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell54.Weight = 0.4512766388718652D;
            // 
            // xrTableCell55
            // 
            this.xrTableCell55.Dpi = 254F;
            this.xrTableCell55.Name = "xrTableCell55";
            this.xrTableCell55.StylePriority.UseTextAlignment = false;
            this.xrTableCell55.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell55.Weight = 0.451276272660905D;
            // 
            // xrTableCell56
            // 
            this.xrTableCell56.Dpi = 254F;
            this.xrTableCell56.Name = "xrTableCell56";
            this.xrTableCell56.StylePriority.UseTextAlignment = false;
            this.xrTableCell56.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell56.Weight = 0.54153152719308628D;
            // 
            // xrTableCell57
            // 
            this.xrTableCell57.Dpi = 254F;
            this.xrTableCell57.Name = "xrTableCell57";
            this.xrTableCell57.StylePriority.UseTextAlignment = false;
            this.xrTableCell57.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell57.Weight = 0.4512762726609052D;
            // 
            // xrTableCell58
            // 
            this.xrTableCell58.Dpi = 254F;
            this.xrTableCell58.Name = "xrTableCell58";
            this.xrTableCell58.StylePriority.UseTextAlignment = false;
            this.xrTableCell58.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell58.Weight = 0.49442819179859565D;
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable5});
            this.ReportFooter.Dpi = 254F;
            this.ReportFooter.HeightF = 63.5F;
            this.ReportFooter.Name = "ReportFooter";
            this.ReportFooter.SnapLinePadding = new DevExpress.XtraPrinting.PaddingInfo(25, 25, 0, 25, 254F);
            // 
            // StartM
            // 
            this.StartM.Expression = resources.GetString("StartM.Expression");
            this.StartM.Name = "StartM";
            // 
            // StopM
            // 
            this.StopM.Expression = resources.GetString("StopM.Expression");
            this.StopM.Name = "StopM";
            // 
            // weigthE
            // 
            this.weigthE.Expression = "[][[PointOfEvaluation].[Evaluation].[Title] == [^.PointOfEvaluation].[Evaluation]" +
    ".[Title] && IsNull([^.SubPointOfEvaluation])].Sum([weight])";
            this.weigthE.Name = "weigthE";
            // 
            // customFontStyle
            // 
            this.customFontStyle.Name = "customFontStyle";
            // 
            // customFontStyleB
            // 
            this.customFontStyleB.Name = "customFontStyleB";
            // 
            // calSumPoE
            // 
            this.calSumPoE.Expression = "[PointOfEvaluation][[SPoEs] == null].Sum([Weight])";
            this.calSumPoE.Name = "calSumPoE";
            // 
            // calSumWeightScore
            // 
            this.calSumWeightScore.Expression = "[][[PointOfEvaluation] == [^.PointOfEvaluation] && IsNull([^.SubPointOfEvaluation" +
    "])].Sum([WeightScore])";
            this.calSumWeightScore.Name = "calSumWeightScore";
            // 
            // weightD
            // 
            this.weightD.Expression = "[][[PointOfEvaluation].[Evaluation].[Dimension].[Title] == [^.PointOfEvaluation]." +
    "[Evaluation].[Dimension].[Title] && IsNull([^.SubPointOfEvaluation])].Sum([weigh" +
    "t])";
            this.weightD.Name = "weightD";
            // 
            // collectionDataSource1
            // 
            this.collectionDataSource1.Name = "collectionDataSource1";
            this.collectionDataSource1.ObjectTypeName = "pes.Module.BusinessObjects.OwnScore";
            this.collectionDataSource1.TopReturnedRecords = 30;
            // 
            // OwnerReport
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.GroupHeader1,
            this.PageHeader,
            this.ReportFooter});
            this.CalculatedFields.AddRange(new DevExpress.XtraReports.UI.CalculatedField[] {
            this.PoE_Title,
            this.weight,
            this.StartM,
            this.StopM,
            this.weigthE,
            this.calSumPoE,
            this.calSumWeightScore,
            this.weightD});
            this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
            this.collectionDataSource1});
            this.DataSource = this.collectionDataSource1;
            this.Dpi = 254F;
            this.Font = new System.Drawing.Font("TH SarabunIT๙", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Margins = new System.Drawing.Printing.Margins(50, 50, 100, 50);
            this.PageHeight = 2970;
            this.PageWidth = 2100;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
            this.SnapGridSize = 25F;
            this.StyleSheet.AddRange(new DevExpress.XtraReports.UI.XRControlStyle[] {
            this.customFontStyle,
            this.customFontStyleB});
            this.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.Version = "17.2";
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.collectionDataSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion
}
