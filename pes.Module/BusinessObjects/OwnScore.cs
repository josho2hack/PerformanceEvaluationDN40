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
using DevExpress.ExpressApp.Security.Strategy;

namespace pes.Module.BusinessObjects
{
    //[DefaultClassOptions]
    [XafDisplayName("รายละเอียดผลการประเมิน")]
    [DefaultProperty("MainScore.Office")]
    [RuleCombinationOfPropertiesIsUnique("OwnScoreRule", DefaultContexts.Save, "MainScore,PointOfEvaluation,SubPointOfEvaluation")]
    public class OwnScore : XPObject
    { 
        public OwnScore(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        decimal result;
        [XafDisplayName("ผลการดำเนินงาน")]
        [RuleRequiredField(DefaultContexts.Save)]
        [DbType("decimal(38,8)")]
        public decimal Result
        {
            get
            {
                return result;
            }
            set
            {
                bool modified = SetPropertyValue("Result", ref result, value);
                if (!IsLoading && !IsSaving && modified)
                {
                    decimal PercentDif = 0;
                    if (AuditScore.Expect != 0)
                    {
                        PercentDif = Result / AuditScore.Expect;
                    }
                    if (PercentDif != 0)
                    {
                        if (PointOfEvaluation.No == 1)
                        {
                            if (PercentDif <= 0.96m) score = 1;
                            else if (PercentDif < 0.97m) score = 1m + (PercentDif - 0.96m) / 0.01m;
                            else if (PercentDif < 0.98m) score = 2m + (PercentDif - 0.97m) / 0.01m;
                            else if (PercentDif < 0.99m) score = 3m + (PercentDif - 0.98m) / 0.01m;
                            else if (PercentDif < 1m) score = 4m + (PercentDif - 0.99m) / 0.01m;
                            else Score = 5m;
                        }
                        if (PointOfEvaluation.No == 2 || PointOfEvaluation.No == 3 || PointOfEvaluation.No == 4 || PointOfEvaluation.No == 5 || PointOfEvaluation.No == 6 || PointOfEvaluation.No == 10)
                        {
                            if (PercentDif <= 0.6m) score = 1;
                            else if (PercentDif < 0.7m) score = 1m + (PercentDif - 0.6m) / 0.1m;
                            else if (PercentDif < 0.8m) score = 2m + (PercentDif - 0.7m) / 0.1m;
                            else if (PercentDif < 0.9m) score = 3m + (PercentDif - 0.8m) / 0.1m;
                            else if (PercentDif < 1m) score = 4m + (PercentDif - 0.9m) / 0.1m;
                            else Score = 5m;
                        }
                        if (PointOfEvaluation.No == 7)
                        {
                            if (PercentDif <= 0.9m) score = 1;
                            else if (PercentDif < 0.95m) score = 1m + (PercentDif - 0.9m) / 0.05m;
                            else if (PercentDif < 1m) score = 2m + (PercentDif - 0.95m) / 0.05m;
                            else if (PercentDif < 1.05m) score = 3m + (PercentDif - 1m) / 0.05m;
                            else if (PercentDif < 1.1m) score = 4m + (PercentDif - 1.05m) / 0.05m;
                            else Score = 5m;
                        }
                        if (PointOfEvaluation.No == 8)
                        {
                            if (PercentDif <= 0.9950m) score = 1;
                            else if (PercentDif < 0.9975m) score = 1m + (PercentDif - 0.9950m) / 0.0025m;
                            else if (PercentDif < 1.0000m) score = 2m + (PercentDif - 0.9975m) / 0.0025m;
                            else if (PercentDif < 1.0025m) score = 3m + (PercentDif - 1.0000m) / 0.0025m;
                            else if (PercentDif < 1.0050m) score = 4m + (PercentDif - 1.0025m) / 0.0025m;
                            else Score = 5m;
                        }
                        if (PointOfEvaluation.No == 9)
                        {
                            if (PercentDif <= 0.9900m) score = 1;
                            else if (PercentDif < 0.9950m) score = 1m + (PercentDif - 0.9900m) / 0.0050m;
                            else if (PercentDif < 1.0000m) score = 2m + (PercentDif - 0.9950m) / 0.0050m;
                            else if (PercentDif < 1.0050m) score = 3m + (PercentDif - 1.0000m) / 0.0050m;
                            else if (PercentDif < 1.0100m) score = 4m + (PercentDif - 1.0050m) / 0.0050m;
                            else Score = 5m;
                        }
                        if (PointOfEvaluation.No == 11)
                        {
                            if (PercentDif <= 0.10m) score = 1;
                            else if (PercentDif < 0.20m) score = 1m + (PercentDif - 0.10m) / 0.10m;
                            else if (PercentDif < 0.30m) score = 2m + (PercentDif - 0.20m) / 0.10m;
                            else if (PercentDif < 0.40m) score = 3m + (PercentDif - 0.30m) / 0.10m;
                            else if (PercentDif < 0.50m) score = 4m + (PercentDif - 0.40m) / 0.10m;
                            else Score = 5m;
                        }
                        if (PointOfEvaluation.No == 12)
                        {
                            if (PercentDif <= 0.79m) score = 1;
                            else if (PercentDif < 0.84m) score = 1m + (PercentDif - 0.79m) / 0.05m;
                            else if (PercentDif < 0.89m) score = 2m + (PercentDif - 0.84m) / 0.05m;
                            else if (PercentDif < 0.94m) score = 3m + (PercentDif - 0.89m) / 0.05m;
                            else if (PercentDif < 0.99m) score = 4m + (PercentDif - 0.94m) / 0.05m;
                            else Score = 5m;
                        }
                        if (PointOfEvaluation.No == 13 || PointOfEvaluation.No == 14 || PointOfEvaluation.No == 15 || PointOfEvaluation.No == 16)
                        {
                            if (PercentDif <= 0.20m) score = 1;
                            else if (PercentDif < 0.40m) score = 1m + (PercentDif - 0.20m) / 0.20m;
                            else if (PercentDif < 0.60m) score = 2m + (PercentDif - 0.40m) / 0.20m;
                            else if (PercentDif < 0.80m) score = 3m + (PercentDif - 0.60m) / 0.20m;
                            else if (PercentDif < 1.00m) score = 4m + (PercentDif - 0.80m) / 0.20m;
                            else Score = 5m;
                        }
                    }
                }
            }
        }

        decimal score;
        [XafDisplayName("ค่าคะแนนที่ได้")]
        [ImmediatePostData(true)]
        [RuleRequiredField(DefaultContexts.Save)]
        [DbType("decimal(38,8)")]
        public decimal Score
        {
            get
            {
                return score;
            }
            set
            {
                bool modified = SetPropertyValue("Score", ref score, value);
                if (!IsLoading && !IsSaving && modified) //if set calculate WeightScore
                {
                    if (SubPointOfEvaluation != null)
                    {
                        if ((MainScore.Office.OfficeId.Substring(0, 3) == "000" && MainScore.Office.OfficeId.Substring(5, 3) == "000")||(MainScore.Office.OfficeId.Substring(2, 6) == "000000"))
                        {
                            WeightScore = Score * SubPointOfEvaluation.Weight / 100;
                        }
                        else
                        {
                            WeightScore = Score * SubPointOfEvaluation.WeightForST / 100;
                        }
                    }
                    else
                    {
                        if ((MainScore.Office.OfficeId.Substring(0, 3) == "000" && MainScore.Office.OfficeId.Substring(5, 3) == "000") || (MainScore.Office.OfficeId.Substring(2, 6) == "000000"))
                        {
                            WeightScore = Score * PointOfEvaluation.Weight / 100;
                        }
                        else
                        {
                            WeightScore = Score * PointOfEvaluation.WeightForST / 100;
                        }
                    }
                }
            }
        }

        decimal weightScore;
        [XafDisplayName("ค่าคะแนนถ่วงน้ำหนัก")]
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

        PointOfEvaluation pointOfEvaluation;
        [Association("PointOfEvaluation-OwnScores")]
        [RuleRequiredField(DefaultContexts.Save)]
        [XafDisplayName("ตัวชี้วัดผลการปฏิบัติราชการ")]
        public PointOfEvaluation PointOfEvaluation
        {
            get
            {
                return pointOfEvaluation;
            }
            set
            {
                SetPropertyValue("PointOfEvaluation", ref pointOfEvaluation, value);
            }
        }

        SubPointOfEvaluation subPointOfEvaluation;
        [DataSourceProperty("PointOfEvaluation.SubPointOfEvaluations")]
        [XafDisplayName("ตัวชี้วัดย่อยผลการปฏิบัติราชการ")]
        public SubPointOfEvaluation SubPointOfEvaluation
        {
            get
            {
                return subPointOfEvaluation;
            }
            set
            {
                SetPropertyValue("SubPointOfEvaluation", ref subPointOfEvaluation, value);
            }
        }


        AuditScore auditScore = null;
        [XafDisplayName("ผลการประเมินโดยหน่วยกำกับ")]
        public AuditScore AuditScore
        {
            get => auditScore;
            set
            {
                if (auditScore == value) return;
                AuditScore prevAuditScore = auditScore;
                auditScore = value;

                if (IsLoading) return;

                if (prevAuditScore != null && prevAuditScore.OwnScore == this)
                    prevAuditScore.OwnScore = null;

                if (auditScore != null)
                    auditScore.OwnScore = this;

                OnChanged("AuditScore");
            }
        }

        Score mainScore;
        [Association("Score-OwnScores")]
        [XafDisplayName("ผลการประเมินหลัก")]
        public Score MainScore
        {
            get => mainScore;
            set => SetPropertyValue(nameof(MainScore), ref mainScore, value);
        }

        [Association("OwnResults-OwnScores")]
        [XafDisplayName("ผลการดำเนินการรายเดือน")]
        public XPCollection<OwnResult> OwnResults
        {
            get
            {
                return GetCollection<OwnResult>(nameof(OwnResults));
            }
        }
    }


    //// file-------------------------------------------------------
    //[NonPersistent]
    //[XafDisplayName("ไฟล์อับโหลดคะแนน")]
    //public class OwnScoreFile : BaseObject
    //{
    //    public OwnScoreFile(Session session) : base(session) { }

    //    public override void AfterConstruction()
    //    {
    //        base.AfterConstruction();
    //        if (SecuritySystem.CurrentUser != null)
    //        {
    //            Owner = Session.GetObjectByKey<Employee>(SecuritySystem.CurrentUserId);
    //            Office = Owner.Office;
    //            Year = DateTime.Now.Year + 543;
    //        }

    //    }

    //    Office office;
    //    public Office Office
    //    {
    //        get
    //        {
    //            return office;
    //        }
    //        set
    //        {
    //            SetPropertyValue("Office", ref office, value);
    //        }
    //    }
    //    private int year;
    //    [XafDisplayName("ปีงบประมาณ")]
    //    public int Year
    //    {
    //        get
    //        {
    //            return year;
    //        }
    //        set
    //        {
    //            SetPropertyValue("Year", ref year, value);
    //        }
    //    }
    //    private int vround;
    //    //[VisibleInListView(false)]
    //    [XafDisplayName("รอบที่")]
    //    public int VRound
    //    {
    //        get
    //        {
    //            return vround;
    //        }
    //        set
    //        {
    //            SetPropertyValue("VRound", ref vround, value);
    //        }
    //    }

    //    private FileData file;
    //    [DevExpress.Xpo.Aggregated, ExpandObjectMembers(ExpandObjectMembers.Never)]
    //    public FileData File
    //    {
    //        get { return file; }
    //        set
    //        {
    //            SetPropertyValue("File", ref file, value);
    //        }
    //    }

    //    Employee owner;
    //    [XafDisplayName("ผู้บันทึก")]
    //    public Employee Owner
    //    {
    //        get { return owner; }
    //        set { SetPropertyValue("Owner", ref owner, value); }
    //    }

        
    //}
}