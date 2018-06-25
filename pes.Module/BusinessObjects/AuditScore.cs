using System;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;

namespace pes.Module.BusinessObjects
{
    [NavigationItem("pes")]
    [XafDisplayName("ผลการประเมินโดยหน่วยกำกับ")]
    [DefaultProperty("OwnScore.PointOfEvaluation.AuditOffice")]
    public class AuditScore : XPObject
    { 
        public AuditScore(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            if (SecuritySystem.CurrentUser != null)
            {
                Owner = Session.GetObjectByKey<Employee>(SecuritySystem.CurrentUserId); 
            }
        }
        private decimal expect;
        [XafDisplayName("ประมาณการ/เป้าหมาย"), ToolTip("ประมาณการผลการปฏิบัติงานที่ทำได้")]
        //[ModelDefault("EditMask", "0.000"), Index(1), VisibleInListView(true)]
        [Persistent("Expect"), RuleRequiredField(DefaultContexts.Save)]
        [DbType("decimal(38,8)")]
        public decimal Expect
        {
            get
            {
                return expect;
            }
            set
            {
                SetPropertyValue("Expect", ref expect, value);
            }
        }

        private decimal result;
        [XafDisplayName("ผลการปฏิบัติ"), ToolTip("ผลการปฏิบัติงานที่ทำได้")]
        //[ModelDefault("EditMask", "0.000"), Index(2), VisibleInListView(true)]
        [Persistent("Result"), RuleRequiredField(DefaultContexts.Save)]
        [DbType("decimal(38,8)")]
        public decimal Result
        {
            get { return result; }
            set
            {
                bool modified = SetPropertyValue("Result", ref result, value);
                if (!IsLoading && !IsSaving && modified)
                {
                    DifExpectResult = Result - Expect;

                    if (Expect != 0)
                    {
                        PercentDif = Result / Expect;
                    }
                    if(PercentDif != 0)
                    {
                        if (OwnScore.PointOfEvaluation.No == 1)
                        {
                            if (PercentDif <= 0.96m) score = 1;
                            else if (PercentDif < 0.97m) score = 1m + (PercentDif - 0.96m)/0.01m;
                            else if (PercentDif < 0.98m) score = 2m + (PercentDif - 0.97m)/0.01m;
                            else if (PercentDif < 0.99m) score = 3m + (PercentDif - 0.98m)/0.01m;
                            else if (PercentDif < 1m) score = 4m + (PercentDif - 0.99m)/ 0.01m;
                            else Score = 5m;
                        }
                        if (OwnScore.PointOfEvaluation.No == 2 || OwnScore.PointOfEvaluation.No == 3 || OwnScore.PointOfEvaluation.No == 4 || OwnScore.PointOfEvaluation.No == 5 || OwnScore.PointOfEvaluation.No == 6 || OwnScore.PointOfEvaluation.No == 10)
                        {
                            if (PercentDif <= 0.6m) score = 1;
                            else if (PercentDif < 0.7m) score = 1m + (PercentDif - 0.6m)/0.1m;
                            else if (PercentDif < 0.8m) score = 2m + (PercentDif - 0.7m)/0.1m;
                            else if (PercentDif < 0.9m) score = 3m + (PercentDif - 0.8m)/0.1m;
                            else if (PercentDif < 1m) score = 4m + (PercentDif - 0.9m)/0.1m;
                            else Score = 5m;
                        }
                        if (OwnScore.PointOfEvaluation.No == 7)
                        {
                            if (PercentDif <= 0.9m) score = 1;
                            else if (PercentDif < 0.95m) score = 1m + (PercentDif - 0.9m)/0.05m;
                            else if (PercentDif < 1m) score = 2m + (PercentDif - 0.95m)/0.05m;
                            else if (PercentDif < 1.05m) score = 3m + (PercentDif - 1m)/0.05m;
                            else if (PercentDif < 1.1m) score = 4m + (PercentDif - 1.05m)/0.05m;
                            else Score = 5m;
                        }
                        if (OwnScore.PointOfEvaluation.No == 8)
                        {
                            if (PercentDif <= 0.9950m) score = 1;
                            else if (PercentDif < 0.9975m) score = 1m + (PercentDif - 0.9950m) / 0.0025m;
                            else if (PercentDif < 1.0000m) score = 2m + (PercentDif - 0.9975m) / 0.0025m;
                            else if (PercentDif < 1.0025m) score = 3m + (PercentDif - 1.0000m) / 0.0025m;
                            else if (PercentDif < 1.0050m) score = 4m + (PercentDif - 1.0025m) / 0.0025m;
                            else Score = 5m;
                        }
                        if (OwnScore.PointOfEvaluation.No == 9)
                        {
                            if (PercentDif <= 0.9900m) score = 1;
                            else if (PercentDif < 0.9950m) score = 1m + (PercentDif - 0.9900m) / 0.0050m;
                            else if (PercentDif < 1.0000m) score = 2m + (PercentDif - 0.9950m) / 0.0050m;
                            else if (PercentDif < 1.0050m) score = 3m + (PercentDif - 1.0000m) / 0.0050m;
                            else if (PercentDif < 1.0100m) score = 4m + (PercentDif - 1.0050m) / 0.0050m;
                            else Score = 5m;
                        }
                        if (OwnScore.PointOfEvaluation.No == 11)
                        {
                            if (PercentDif <= 0.10m) score = 1;
                            else if (PercentDif < 0.20m) score = 1m + (PercentDif - 0.10m) / 0.10m;
                            else if (PercentDif < 0.30m) score = 2m + (PercentDif - 0.20m) / 0.10m;
                            else if (PercentDif < 0.40m) score = 3m + (PercentDif - 0.30m) / 0.10m;
                            else if (PercentDif < 0.50m) score = 4m + (PercentDif - 0.40m) / 0.10m;
                            else Score = 5m;
                        }
                        if (OwnScore.PointOfEvaluation.No == 12)
                        {
                            if (PercentDif <= 0.79m) score = 1;
                            else if (PercentDif < 0.84m) score = 1m + (PercentDif - 0.79m) / 0.05m;
                            else if (PercentDif < 0.89m) score = 2m + (PercentDif - 0.84m) / 0.05m;
                            else if (PercentDif < 0.94m) score = 3m + (PercentDif - 0.89m) / 0.05m;
                            else if (PercentDif < 0.99m) score = 4m + (PercentDif - 0.94m) / 0.05m;
                            else Score = 5m;
                        }
                        if (OwnScore.PointOfEvaluation.No == 13 || OwnScore.PointOfEvaluation.No == 14 || OwnScore.PointOfEvaluation.No == 15 || OwnScore.PointOfEvaluation.No == 16)
                        {
                            if (PercentDif <= 0.20m) score = 1;
                            else if (PercentDif < 0.40m) score = 1m + (PercentDif - 0.20m) / 0.20m;
                            else if (PercentDif < 0.60m) score = 2m + (PercentDif - 0.40m) / 0.20m;
                            else if (PercentDif < 0.80m) score = 3m + (PercentDif - 0.60m) / 0.20m;
                            else if (PercentDif < 1.00m) score = 4m + (PercentDif - 0.80m) / 0.20m;
                            else Score = 5m;
                        }
                    }

                    if(Score != 0)
                    {
                        if (OwnScore.SubPointOfEvaluation != null)
                        {
                            if ((OwnScore.MainScore.Office.OfficeId.Substring(0, 3) == "000" && OwnScore.MainScore.Office.OfficeId.Substring(5, 3) == "000") || (OwnScore.MainScore.Office.OfficeId.Substring(2, 6) == "000000"))
                            {
                                WeightScore = Score * OwnScore.SubPointOfEvaluation.Weight / 100;
                            }
                            else
                            {
                                WeightScore = Score * OwnScore.SubPointOfEvaluation.WeightForST / 100;
                            }
                        }
                        else
                        {
                            if ((OwnScore.MainScore.Office.OfficeId.Substring(0, 3) == "000" && OwnScore.MainScore.Office.OfficeId.Substring(5, 3) == "000") || (OwnScore.MainScore.Office.OfficeId.Substring(2, 6) == "000000"))
                            {
                                WeightScore = Score * OwnScore.PointOfEvaluation.Weight / 100;
                            }
                            else
                            {
                                WeightScore = Score * OwnScore.PointOfEvaluation.WeightForST / 100;
                            }
                        }
                    }
                }
            }
        }

        //[Persistent(nameof(DifExpectResult))]
        decimal difExpectResult;
        [DbType("decimal(38,8)")]
        //[PersistentAlias("Result - Expect")]
        [XafDisplayName("ผลต่างเทียบ ปมก."), ToolTip("ผลต่างเที่ยบประมาณการ/เป้าหมาย")]
        public decimal DifExpectResult
        {
            get
            {
                return difExpectResult;
            }
            set
            {
                SetPropertyValue("DifExpectResult", ref difExpectResult, value);
            }
        }

        //[Persistent(nameof(PercentDif))]
        decimal percentDif;
        //[PersistentAlias("Result / Expect")]
        [XafDisplayName("% เทียบประมาณการ/เป้าหมาย"), ToolTip("% ผลเทียบกับประมาณการ/เป้าหมาย")]
        [DbType("decimal(38,8)")]
        public decimal PercentDif
        {
            get
            {
                return percentDif;
            }
            set
            {
                SetPropertyValue("PercentDif", ref percentDif, value);
            }
        }

        //[Persistent(nameof(Score))]
        decimal score;
        //[PersistentAlias("Iif(PercentDif<=0.96,1,PercentDif<0.97,1+(PercentDif)*100,PercentDif<0.98,2+((PercentDif-0.97))*100,PercentDif<0.99,3+((PercentDif-0.98))*100,PercentDif<1,4+((PercentDif-0.99))*100,5)")]
        [XafDisplayName("ค่าคะแนน"), ToolTip("ค่าคะแนนจากการคำนวณ")]
        [DbType("decimal(38,8)")]
        public decimal Score
        {
            get
            {
                return score;
            }
            set
            {
                SetPropertyValue("Score", ref score, value);
            }
        }

        //[Persistent(nameof(WeightScore))]
        decimal weightScore;
        ////[PersistentAlias("Iif(,Score * 0.2")]
        [XafDisplayName("คะแนนถ่วงน้ำหนัก"), ToolTip("ค่าคะแนนถ่วงน้ำหนักจากการคำนวณ")]
        [DbType("decimal(38,8)")]
        public decimal WeightScore
        {
            get
            {

                return weightScore;
            }
            set
            {
                SetPropertyValue("WeightScore", ref weightScore, value);
            }
        }

        Employee owner;
        [XafDisplayName("ผู้บันทึก")]
        public Employee Owner
        {
            get { return owner; }
            set { SetPropertyValue("Owner", ref owner, value); }
        }

        OwnScore ownScore = null;
        [XafDisplayName("เจ้าของตะแนน")]
        public OwnScore OwnScore
        {
            get => ownScore;
            set
            {
                if (ownScore == value) return;
                OwnScore prevOwnScore = ownScore;
                ownScore = value;

                if (IsLoading) return;

                if (prevOwnScore != null && prevOwnScore.AuditScore == this)
                    prevOwnScore.AuditScore = null;

                if (ownScore != null)
                    ownScore.AuditScore = this;

                OnChanged("OwnScore");
            }
        }

        [Association("DetailExpects-AuditScores")]
        [XafDisplayName("รายละเอียดประมาณการ")]
        public XPCollection<DetailExpect> DetailExpects
        {
            get
            {
                return GetCollection<DetailExpect>(nameof(DetailExpects));
            }
        }

        [Association("AuditResults-AuditScores")]
        [XafDisplayName("ผลการดำเนินการรายเดือนโดยหน่วยกำกับ")]
        public XPCollection<AuditResult> AuditResults
        {
            get
            {
                return GetCollection<AuditResult>(nameof(AuditResults));
            }
        }
    }
}