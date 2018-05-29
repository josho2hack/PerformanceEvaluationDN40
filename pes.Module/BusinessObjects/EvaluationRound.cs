using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System;
using System.ComponentModel;
using System.Linq;
using DevExpress.Persistent.Validation;

namespace pes.Module.BusinessObjects
{
    [NavigationItem("pes")]
    [XafDisplayName("รอบการประเมิน")]
    [DefaultProperty("Title")]
    [RuleCombinationOfPropertiesIsUnique("Round", DefaultContexts.Save, "Year,MonthStart,MonthStop")]
    public class EvaluationRound : XPObject
    {
        public EvaluationRound(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            Year = DateTime.Now.Year + 543;
        }

        [Persistent(nameof(Title))]
        string title;
        [PersistentAlias(nameof(title))]
        public string Title
        {
            get
            {
                title = "ปีงบประมาณ " + Year + " ตั้งแต่เดือนที่ " + MonthStart + " - "+ MonthStop;
                return title;
            }
        }

        private int year;
        [Size(4)]
        [XafDisplayName("ปีงบประมาณ")]
        public int Year
        {
            get
            {
                return year;
            }
            set
            {
                SetPropertyValue("Year", ref year, value);
            }
        }

        Emonth monthStart;
        [XafDisplayName("เดือนที่เริ่มประเมิน")]
        public Emonth MonthStart
        {
            get
            {
                
                return monthStart;
            }
            set
            {
                SetPropertyValue("MonthStart", ref monthStart, value);
            }
        }

        Emonth monthStop ;
        [XafDisplayName("เดือนที่หยุดประเมิน")]
        public Emonth MonthStop
        {
            get
            {
                return monthStop;
            }
            set
            {
                
                SetPropertyValue("MonthStop", ref monthStop, value);
            }
        }

        [Association("EvaluationRound-Scores")]
        [XafDisplayName("ผลการประเมินของหน่วยงานในรอบ")]
        public XPCollection<Score> Scores
        {
            get
            {
                return GetCollection<Score>(nameof(Scores));
            }
        }

        [Association("ERounds-PoEs")]
        [XafDisplayName("ตัวชี้วัดของรอบ")]
        public XPCollection<PointOfEvaluation> PoEs
        {
            get
            {
                return GetCollection<PointOfEvaluation>(nameof(PoEs));
            }
        }
    }
    public enum Emonth
    {
        ตุลาคม,
        พฤศจิกายน,
        ธันวาคม,
        มกราคม,
        กุมภาพันธ์,
        มีนาคม,
        เมษายน,
        พฤษภาคม,
        มิถุนายน,
        กรกฎาคม,
        สิงหาคม,
        กันยายน
    };
    
    /*
    public enum Emonth
    {
        [XafDisplayName("ตุลาคม")] October,
        [XafDisplayName("พฤศจิกายน")] November,
        [XafDisplayName("ธันวาคม")] December,
        [XafDisplayName("มกราคม")] January,
        [XafDisplayName("กุมภาพันธ์")] February,
        [XafDisplayName("มีนาคม")] March,
        [XafDisplayName("เมษายน")] April,
        [XafDisplayName("พฤษภาคม")] May,
        [XafDisplayName("มิถุนายน")] June,
        [XafDisplayName("กรกฎาคม")] July,
        [XafDisplayName("สิงหาคม")] August,
        [XafDisplayName("กันยายน")] September,
    };*/
}