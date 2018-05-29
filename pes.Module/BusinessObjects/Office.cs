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
    [XafDisplayName("หน่วยงาน")]
    [DefaultProperty("OfficeName")]
    public class Office : XPObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public Office(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        string officeId;
        [Size(8)]
        public string OfficeId
        {
            get
            {
                return officeId;
            }
            set
            {
                SetPropertyValue("OfficeId", ref officeId, value);
            }
        }

        string officeName;
        [Size(200)]
        [XafDisplayName("ชื่อหน่วยงาน")]
        public string OfficeName
        {
            get
            {
                return officeName;
            }
            set
            {
                SetPropertyValue("OfficeName", ref officeName, value);
            }
        }

        [Association("Office-Scores")]
        [XafDisplayName("ผลการประเมินของหน่วยงาน")]
        public XPCollection<Score> Scores
        {
            get
            {
                return GetCollection<Score>("Scores");
            }
        }

        [Association("Office-Employees")]
        [XafDisplayName("เจ้าหน้าที่")]
        public XPCollection<Employee> Employees
        {
            get
            {
                return GetCollection<Employee>("Employees");
            }
        }

        [Association("Office-DetailExpects")]
        [XafDisplayName("ประมาณการของหน่วยงาน")]
        public XPCollection<DetailExpect> DetailExpects
        {
            get
            {
                return GetCollection<DetailExpect>(nameof(DetailExpects));
            }
        }

        [Association("Office-PointOfEvaluations")]
        [XafDisplayName("ตัวชี้วัดที่กำกับ")]
        public XPCollection<PointOfEvaluation> AuditPoEs
        {
            get
            {
                return GetCollection<PointOfEvaluation>(nameof(AuditPoEs));
            }
        }
    }
}