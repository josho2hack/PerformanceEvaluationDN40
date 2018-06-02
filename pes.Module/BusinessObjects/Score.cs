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
    [DefaultClassOptions]
    [XafDisplayName("บันทึกผลการประเมินหน่วยงาน")]
    [RuleCombinationOfPropertiesIsUnique("ScoreRule", DefaultContexts.Save, "Office,ERound")]
    public class Score : XPObject
    { 
        public Score(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            if (SecuritySystem.CurrentUser != null)
            {
                Owner = Session.GetObjectByKey<Employee>(SecuritySystem.CurrentUserId);
                Office = Owner.Office;
            }
        }

        Office office;
        [Association("Office-Scores")]
        [XafDisplayName("หน่วยงาน")]
        [RuleRequiredField(DefaultContexts.Save)]
        public Office Office
        {
            get
            {
                return office;
            }
            set
            {
                SetPropertyValue("Office", ref office, value);
            }
        }
        
        Employee owner;
        [XafDisplayName("ผู้บันทึก")]
        public Employee Owner
        {
            get { return owner; }
            set { SetPropertyValue("Owner", ref owner, value); }
        }

        EvaluationRound eRound;
        [Association("EvaluationRound-Scores")]
        [XafDisplayName("รอบการประเมิน")]
        public EvaluationRound ERound
        {
            get => eRound;
            set => SetPropertyValue(nameof(ERound), ref eRound, value);
        }

        [Association("Score-OwnScores"), DevExpress.Xpo.Aggregated]
        [XafDisplayName("รายละเอียด")]
        public XPCollection<OwnScore> OwnScores
        {
            get
            {
                return GetCollection<OwnScore>(nameof(OwnScores));
            }
        }
    }
}