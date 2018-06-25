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
    [XafDisplayName("ผลการดำเนินการรายเดือนโดยหน่วยกำกับ")]
    public class AuditResult : XPObject
    {
        public AuditResult(Session session)
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
        //[Association("Office-AuditResults")]
        public Office Office
        {
            get => office;
            set => SetPropertyValue(nameof(Office), ref office, value);
        }

        PointOfEvaluation poE;
        [XafDisplayName("ตัวชี้วัด")]
        //[Association("PointOfEvaluation-AuditResults")]
        public PointOfEvaluation PoE
        {
            get => poE;
            set => SetPropertyValue(nameof(PoE), ref poE, value);
        }

        SubPointOfEvaluation sPoE;
        [XafDisplayName("ตัวชี้วัดย่อย")]
        //[Association("SubPointOfEvaluation-AuditResults")]
        public SubPointOfEvaluation SPoE
        {
            get => sPoE;
            set => SetPropertyValue(nameof(SPoE), ref sPoE, value);
        }

        [Association("AuditResults-AuditScores")]
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