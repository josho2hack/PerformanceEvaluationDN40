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
    [XafDisplayName("เปิดการส่งคะแนน")]
    [RuleObjectExists("AnotherSingletonExists", DefaultContexts.Save, "True", InvertResult = true,
    CustomMessageTemplate = "Another Singleton already exists.")]
    [RuleCriteria("CannotDeleteSingleton", DefaultContexts.Delete, "False",
    CustomMessageTemplate = "Cannot delete Singleton.")]
    public class OpenSendScore : XPObject
    { 
        public OpenSendScore(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        //protected override void OnSaved()
        //{
        //    base.OnSaved();
        //    if (Open)
        //    {
        //        //Creat Score and Audit Score
        //    }
        //}

        //protected override void OnSaving()
        //{
        //    base.OnSaving();
        //}

        bool open;
        [XafDisplayName("เปิด")]
        public bool Open
        {
            get
            {
                return open;
            }
            set
            {
                SetPropertyValue("Open", ref open, value);
                //if (SetPropertyValue("Open", ref open, value))
                //{
                //    if (Open)
                //    {
                //        int Year = (int)Session.Evaluate<EvaluationRound>(CriteriaOperator.Parse("Max(Year)"), null);
                //        int maxR = (int)Session.Evaluate<EvaluationRound>(CriteriaOperator.Parse("Max(MonthStop)"), CriteriaOperator.Parse("Year = ?", Year));
                //        if (!IsSaving && !IsLoading)
                //        {
                //            ERound = Session.FindObject<EvaluationRound>(CriteriaOperator.Parse("Year = ? AND MonthStop = ?", Year, maxR));
                //        }
                //    }
                //}
            }
        }

        EvaluationRound eRound;
        [XafDisplayName("รอบการประเมิน")]
        //[Association("EvaluationRound-OpenSendScores")]
        public EvaluationRound ERound
        {
            get => eRound;
            set => SetPropertyValue(nameof(ERound), ref eRound, value);
        }
    }
}