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
    [XafDisplayName("ตัวชี้วัด")]
    [DefaultProperty("Title")]
    //[RuleCombinationOfPropertiesIsUnique("PoERule", DefaultContexts.Save, "No,Year,VRound")]
    public class PointOfEvaluation : XPObject
    {
        public PointOfEvaluation(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            UnitOfPoint = Session.FindObject<UnitOfPoint>(new BinaryOperator("Title", "ร้อยละ"));
            Evaluation = Session.FindObject<Evaluation>(new BinaryOperator("Title", "การประเมินประสิทธิผลและการประเมินคุณภาพ"));
        }

        //[Indexed("No;Title;Year;Round", Unique = true)]

        uint no;
        //[RuleRequiredField("RuleRequiredField for PointOfEvaluation.No", DefaultContexts.Save,"ต้องกำหนดข้อตัวชี้วัด")]
        [XafDisplayName("ตัวชี้วัดที่")]
        public uint No
        {
            get
            {
                return no;
            }
            set
            {
                SetPropertyValue("No", ref no, value);
            }
        }

        private string title;
        [XafDisplayName("ตัวชี้วัด"), ToolTip("ตัวชี้วัดผลการปฏิบัติราชการ")]
        public string Title
        {
            get
            {
                title = No + "." + Name;
                return title;
            }
        }

        private string name;
        [XafDisplayName("ตัวชี้วัดผลการปฏิบัติราชการ"), ToolTip("รายละเอียดของตัวชี้วัด")]
        [Persistent("Name"), RuleRequiredField(DefaultContexts.Save)]
        [Size(250)]
        public string Name
        {
            get { return name; }
            set { SetPropertyValue("Name", ref name, value); }
        }

        UnitOfPoint unitOfPoint;
        [XafDisplayName("หน่วยวัด"), ToolTip("ร้อยละ, ระดับ, ราย, ราย/แบบ")]
        public UnitOfPoint UnitOfPoint
        {
            get
            {
                return unitOfPoint;
            }
            set
            {
                SetPropertyValue("UnitOfPoint", ref unitOfPoint, value);
            }
        }

        decimal weight;
        [XafDisplayName("น้ำหนักร้อยละ(สำหรับ ส่วนกลาง และ สภ.)")]
        public decimal Weight
        {
            get
            {
                return weight;
            }
            set
            {
                if (SetPropertyValue("Weight", ref weight, value)) WeightForST = Weight;
            }
        }

        decimal weightForST;
        [XafDisplayName("น้ำหนักร้อยละ(สำหรับ สท.)")]
        public decimal WeightForST
        {
            get
            {
                return weightForST;
            }
            set
            {
                SetPropertyValue("WeightForST", ref weightForST, value);
            }
        }

        Evaluation evaluation;
        [XafDisplayName("การประเมิน")]
        [Association("Evaluation-PointOfEvaluations")]
        public Evaluation Evaluation
        {
            get
            {
                return evaluation;
            }
            set
            {
                SetPropertyValue("Evaluation", ref evaluation, value);
            }
        }

        [Association("PointOfEvaluation-SubPointOfEvaluations"), DevExpress.Xpo.Aggregated]
        [XafDisplayName("ตัวชี้วัดย่อยทั้งหมด")]
        public XPCollection<SubPointOfEvaluation> SPoEs
        {
            get
            {
                return GetCollection<SubPointOfEvaluation>("SubPointOfEvaluations");
            }
        }

        //[Persistent("SubPointOfEvaluationCount")]
        //private int? fSubPointOfEvaluationCount = null;
        //[XafDisplayName("จำนวนตัวชี้วัดย่อยทั้งหมด")]
        //[PersistentAlias("fSubPointOfEvaluationCount")]
        //public int? SubPointOfEvaluationCount
        //{
        //    get
        //    {
        //        if (!IsLoading && !IsSaving && fSubPointOfEvaluationCount == null)
        //            UpdateSubPointOfEvaluationCount(false);
        //        return fSubPointOfEvaluationCount;
        //    }
        //}

        //[Persistent("SubPointWeightTotal")]
        //private float? fSubPointWeightTotal = null;
        //[XafDisplayName("จำนวนน้ำหนักร้อยละตัวชี้วัดย่อยทั้งหมด")]
        //[PersistentAlias("fSubPointWeightTotal")]
        //public float? SubPointWeightTotal
        //{
        //    get
        //    {
        //        if (!IsLoading && !IsSaving && fSubPointWeightTotal == null)
        //            UpdateSubPointWeightTotal(false);
        //        return fSubPointWeightTotal;
        //    }
        //}

        //[Persistent("SubPointWeightTotalForST")]
        //private float? fSubPointWeightTotalForST = null;
        //[XafDisplayName("จำนวนน้ำหนักร้อยละตัวชี้วัดย่อยทั้งหมด(สท.)")]
        //[PersistentAlias("fSubPointWeightTotalForST")]
        //public float? SubPointWeightTotalForST
        //{
        //    get
        //    {
        //        if (!IsLoading && !IsSaving && fSubPointWeightTotalForST == null)
        //            UpdateSubPointWeightForSTTotal(false);
        //        return fSubPointWeightTotalForST;
        //    }
        //}

        //public void UpdateSubPointOfEvaluationCount(bool forceChangeEvents)
        //{
        //    int? oldSubPointOfEvaluationCount = fSubPointOfEvaluationCount;
        //    fSubPointOfEvaluationCount = Convert.ToInt32(Evaluate(CriteriaOperator.Parse("SubPointOfEvaluations.Count")));
        //    if (forceChangeEvents)
        //        OnChanged("SubPointOfEvaluationCount", oldSubPointOfEvaluationCount, fSubPointOfEvaluationCount);
        //}
        //public void UpdateSubPointWeightTotal(bool forceChangeEvents)
        //{
        //    float? oldSubPointWeightTotal = fSubPointWeightTotal;
        //    float? oldSWeight = Weight;
        //    float tempTotal = 0f;
        //    foreach (SubPointOfEvaluation detail in SubPointOfEvaluations)
        //        tempTotal += detail.Weight;
        //    fSubPointWeightTotal = tempTotal;
        //    if (forceChangeEvents)
        //    {
        //        OnChanged("SubPointWeightTotal", oldSubPointWeightTotal, fSubPointWeightTotal);
        //        OnChanged("Weight", oldSWeight, fSubPointWeightTotal);
        //    }
        //}

        //public void UpdateSubPointWeightForSTTotal(bool forceChangeEvents)
        //{
        //    float? oldSubPointWeightTotalForST = fSubPointWeightTotalForST;
        //    float? oldSWeightForST = WeightForST;
        //    float? tempTotal = 0f;
        //    foreach (SubPointOfEvaluation detail in SubPointOfEvaluations)
        //        tempTotal += detail.WeightForST;
        //    fSubPointWeightTotalForST = tempTotal;
        //    if (forceChangeEvents)
        //    {
        //        OnChanged("SubPointWeightTotalForST", oldSubPointWeightTotalForST, fSubPointWeightTotalForST);
        //        OnChanged("WeightForST", oldSWeightForST, fSubPointWeightTotalForST);
        //    }
        //}

        [Association("PointOfEvaluation-OwnScores")]
        [XafDisplayName("ผลการประเมินของหน่วยงานทั้งหมด")]
        public XPCollection<OwnScore> OwnScores
        {
            get
            {
                return GetCollection<OwnScore>("OwnScores");
            }
        }

        [Association("ERounds-PoEs")]
        [XafDisplayName("รอบการประเมิน")]
        public XPCollection<EvaluationRound> ERounds
        {
            get
            {
                return GetCollection<EvaluationRound>(nameof(ERounds));
            }
        }

        Office auditOffice;
        [Association("Office-PointOfEvaluations")]
        [XafDisplayName("หน่วยงานกำกับ")]
        public Office AuditOffice
        {
            get => auditOffice;
            set => SetPropertyValue(nameof(AuditOffice), ref auditOffice, value);
        }
    }
}