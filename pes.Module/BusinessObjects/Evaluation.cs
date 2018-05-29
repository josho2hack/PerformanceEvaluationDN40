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
    [XafDisplayName("การประเมิน")]
    [DefaultProperty("Title")]
    
    public class Evaluation : XPObject
    { 
        public Evaluation(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        private string _title;
        [XafDisplayName("การประเมิน"), ToolTip("ชื่อของการประเมิน, ชนิดของการประเมิน")]
        [Persistent("Title"), RuleRequiredField(DefaultContexts.Save)]
        public string Title
        {
            get { return _title; }
            set { SetPropertyValue("Title", ref _title, value); }
        }

        Dimension dimension;
        [Association("Dimension-Evaluations")]
        [XafDisplayName("มิติ")]
        public Dimension Dimension
        {
            get
            {
                return dimension;
            }
            set
            {
                SetPropertyValue("Dimension", ref dimension, value);
            }
        }

        [Association("Evaluation-PointOfEvaluations")]
        public XPCollection<PointOfEvaluation> PointOfEvaluations
        {
            get
            {
                return GetCollection<PointOfEvaluation>(nameof(PointOfEvaluations));
            }
        }
    }
}