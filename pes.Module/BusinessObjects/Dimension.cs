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
    [XafDisplayName("มิติ")]
    [DefaultProperty("Title")]
    public class Dimension : XPObject
    { 
        public Dimension(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        private string _title;
        [XafDisplayName("มิติ"), ToolTip("ชนิดมิติ, ชื่อมิติ")]
        [Persistent("Title"), RuleRequiredField(DefaultContexts.Save)]
        public string Title
        {
            get { return _title; }
            set { SetPropertyValue("Title", ref _title, value); }
        }

        [Association("Dimension-Evaluations")]
        [XafDisplayName("การประเมิน")]
        public XPCollection<Evaluation> Evaluations
        {
            get
            {
                return GetCollection<Evaluation>(nameof(Evaluations));
            }
        }
    }
}