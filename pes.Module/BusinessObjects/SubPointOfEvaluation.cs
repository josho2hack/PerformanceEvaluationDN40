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
    [XafDisplayName("ตัวชี้วัดย่อย")]
    [DefaultProperty("Title")]
    public class SubPointOfEvaluation : XPObject
    { 
        public SubPointOfEvaluation(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            UnitOfPoint = Session.FindObject<UnitOfPoint>(new BinaryOperator("Title", "ร้อยละ"));
        }

        uint no;
        [RuleRequiredField("RuleRequiredField for SubPointOfEvaluation.No", DefaultContexts.Save, "ต้องกำหนดข้อตัวชี้วัดย่อย")]
        [XafDisplayName("ตัวชี้วัดย่อยที่")]
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
        [XafDisplayName("ตัวชี้วัดย่อย"), ToolTip("ตัวชี้วัดย่อยผลการปฏิบัติราชการ")]
        public string Title
        {
            get
            {
                title = PointOfEvaluation.No + "." + No + "." + Name;
                return title;
            }
        }

        private string name;
        [XafDisplayName("ตัวชี้วัดย่อยผลการปฏิบัติราชการ"), ToolTip("รายละเอียดของตัวชี้วัดย่อย")]
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
        [XafDisplayName("น้ำหนักร้อยละ")]
        public decimal Weight
        {
            get
            {
                return weight;
            }
            set
            {
                if(SetPropertyValue("Weight", ref weight, value)) WeightForST = Weight;
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

        PointOfEvaluation poE;
        [Association("PointOfEvaluation-SubPointOfEvaluations")]
        [XafDisplayName("ตัวชี้วัดหลัก")]
        public PointOfEvaluation PoE
        {
            get
            {
                return PoE;
            }
            set
            {
                SetPropertyValue("PointOfEvaluation", ref poE, value);
            }
        }
    }
}