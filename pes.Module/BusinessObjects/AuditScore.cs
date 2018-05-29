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
    [XafDisplayName("บันทึกผลการประเมินโดยหน่วยกำกับ")]
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
                foreach (DetailExpect exp in DetailExpects)
                {
                    expect += exp.ExpectMonthValue;
                }
                return expect;
            }
            //set
            //{
            //    SetPropertyValue("Expect", ref _Expect, value);
            //}
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
                    difExpectResult = Result - Expect;
                    if (Expect != 0)
                    {
                        PercentDif = Result / Expect;
                    }
                    if(PercentDif != 0)
                    {
                        if (PercentDif <= 0.96m) score = 1;
                        else if (PercentDif <= 0.97m) score = 1m + (PercentDif - 0.96m) * 100m;
                        else if (PercentDif <= 0.98m) score = 2m + (PercentDif - 0.97m) * 100m;
                        else if (PercentDif <= 0.99m) score = 3m + (PercentDif - 0.98m) * 100m;
                        else if (PercentDif <= 1m) score = 4m + (PercentDif - 0.99m) * 100m;
                        else Score = 5m;
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

        //PointOfEvaluation poE;
        //[Association("PointOfEvaluation-AuditScores")]
        //[XafDisplayName("ตัวชี้วัด")]
        //public PointOfEvaluation PoE
        //{
        //    get => poE;
        //    set => SetPropertyValue(nameof(PoE), ref poE, value);
        //}

        //SubPointOfEvaluation sPoE;
        //[Association("SubPointOfEvaluation-AuditScores")]
        //[XafDisplayName("ตัวชี้วัดย่อย")]
        //public SubPointOfEvaluation SPoE
        //{
        //    get => sPoE;
        //    set => SetPropertyValue(nameof(SPoE), ref sPoE, value);
        //}

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
        [XafDisplayName("ผลการดำเนินการรายเดือน")]
        public XPCollection<AuditResult> AuditResults
        {
            get
            {
                return GetCollection<AuditResult>(nameof(AuditResults));
            }
        }
    }
}