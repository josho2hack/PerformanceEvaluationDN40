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
    [XafDisplayName("ประมาณการ")]
    public class DetailExpect : XPObject
    { 
        public DetailExpect(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        Emonth expectMonth;
        [XafDisplayName("เดือนที่ประมาณการ")]
        public Emonth ExpectMonth
        {
            get => expectMonth;
            set => SetPropertyValue(nameof(ExpectMonth), ref expectMonth, value);
        }

        decimal expectMonthValue;
        [XafDisplayName("ประมาณการของเดือน")]
        [DbType("decimal(38,8)")]
        public decimal ExpectMonthValue
        {
            get => expectMonthValue;
            set => SetPropertyValue(nameof(ExpectMonthValue), ref expectMonthValue, value);
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
        //[Association("Office-DetailExpects")]
        public Office Office
        {
            get => office;
            set => SetPropertyValue(nameof(Office), ref office, value);
        }

        PointOfEvaluation poE;
        //[Association("PointOfEvaluation-DetailExpects")]
        public PointOfEvaluation PoE
        {
            get => poE;
            set => SetPropertyValue(nameof(PoE), ref poE, value);
        }

        SubPointOfEvaluation sPoE;
        //[Association("SubPointOfEvaluation-DetailExpects")]
        public SubPointOfEvaluation SPoE
        {
            get => sPoE;
            set => SetPropertyValue(nameof(SPoE), ref sPoE, value);
        }

        [Association("DetailExpects-AuditScores")]
        [XafDisplayName("ผลการประเมินโดยหน่วยกำกับ")]
        public XPCollection<AuditScore> AuditScores
        {
            get
            {
                return GetCollection<AuditScore>(nameof(AuditScores));
            }
        }
    }
}