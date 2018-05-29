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
    [XafDisplayName("หน่วยวัด")]
    [DefaultProperty("Title")]
    public class UnitOfPoint : XPObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public UnitOfPoint(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
        string title;
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [XafDisplayName("หน่วยวัด"), ToolTip("ร้อยละ, ระดับ, ราย, ราย/แบบ")]
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                SetPropertyValue("Title", ref title, value);
            }
        }
    }
}