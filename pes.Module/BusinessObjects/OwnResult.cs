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
    [XafDisplayName("ผลการดำเนินการรายเดือน")]
    public class OwnResult : BaseObject
    { 
        public OwnResult(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        Emonth resultMonth;
        [XafDisplayName("เดือน")]
        public Emonth ResultMonth
        {
            get => resultMonth;
            set => SetPropertyValue(nameof(ResultMonth), ref resultMonth, value);
        }

        decimal resultMonthValue;
        [XafDisplayName("ผลการดำเนินงาน")]
        [DbType("decimal(38,8)")]
        public decimal ResultMonthValue
        {
            get => resultMonthValue;
            set => SetPropertyValue(nameof(ResultMonthValue), ref resultMonthValue, value);
        }

        int year;
        [Size(4)]
        [XafDisplayName("ปีงบประมาณ")]
        public int Year
        {
            get => year;
            set => SetPropertyValue(nameof(Year), ref year, value);
        }

        Office office;
        [XafDisplayName("หน่วยงาน")]
        //[Association("Office-OwnResults")]
        public Office Office
        {
            get => office;
            set => SetPropertyValue(nameof(Office), ref office, value);
        }

        PointOfEvaluation poE;
        [XafDisplayName("ตัวชี้วัด")]
        //[Association("PointOfEvaluation-OwnResults")]
        public PointOfEvaluation PoE
        {
            get => poE;
            set => SetPropertyValue(nameof(PoE), ref poE, value);
        }

        SubPointOfEvaluation sPoE;
        [XafDisplayName("ตัวชี้วัดย่อย")]
        //[Association("SubPointOfEvaluation-OwnResults")]
        public SubPointOfEvaluation SPoE
        {
            get => sPoE;
            set => SetPropertyValue(nameof(SPoE), ref sPoE, value);
        }

        [Association("OwnResults-OwnScores")]
        [XafDisplayName("รายละเอียดผลการประเมิน")]
        public XPCollection<OwnScore> OwnScores
        {
            get
            {
                return GetCollection<OwnScore>(nameof(OwnScores));
            }
        }
    }
}