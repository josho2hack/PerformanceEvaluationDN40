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
    [XafDisplayName("รายละเอียดผลการประเมินของหน่วยงาน")]
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
                    if (AuditScore.Expect != 0)
                    {
                        var diff = Result / AuditScore.Expect;
                        if (diff != 0)
                        {
                            if (diff <= 0.96m) Score = 1;
                            else if (diff <= 0.97m) Score = 1m + (diff - 0.96m) * 100m;
                            else if (diff <= 0.98m) Score = 2m + (diff - 0.97m) * 100m;
                            else if (diff <= 0.99m) Score = 3m + (diff - 0.98m) * 100m;
                            else if (diff <= 1m) Score = 4m + (diff - 0.99m) * 100m;
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
        [XafDisplayName("คะแนนจากหน่วยกำกับ")]
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
        [XafDisplayName("หนัาหลักคะแนน")]
        public Score MainScore
        {
            get => mainScore;
            set => SetPropertyValue(nameof(MainScore), ref mainScore, value);
        }

        [Association("OwnResults-OwnScores")]
        [XafDisplayName("รายละเอียดผลการดำเนินการ")]
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