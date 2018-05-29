using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using DevExpress.Xpo;
using Newtonsoft.Json;
using pes.Module.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;


namespace pes.Module.Controllers
{
    public partial class CreateFormController : ViewController
    {
        public CreateFormController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
        private static T _download_serialized_json_data<T>(string url) where T : new()
        {
            using (var w = new WebClient())
            {
                var json_data = string.Empty;
                // attempt to download JSON data as a string
                try
                {
                    json_data = w.DownloadString(url);
                }
                catch (Exception) { }
                // if string with JSON data is not empty, deserialize it to class and return its instance 
                return !string.IsNullOrEmpty(json_data) ? JsonConvert.DeserializeObject<T>(json_data) : new T();
            }
        }
        private void CreateFormController_Activated(object sender, EventArgs e)
        {
            OpenSendScore oss = ObjectSpace.FindObject<OpenSendScore>(CriteriaOperator.Parse("Open = ?", true));
            if (oss != null && oss.Open)
            {
                Employee owner = ObjectSpace.GetObjectByKey<Employee>(SecuritySystem.CurrentUserId);
                Score oscore = ObjectSpace.FindObject<Score>(CriteriaOperator.Parse("Office= ? AND VRound = ?", owner.Office,oss.ERound));
                if(oscore == null)
                {
                    bool adminR = false;
                    foreach(PermissionPolicyRole p in owner.Roles)
                    {
                        if (p.IsAdministrative) adminR = true;
                    }
                    if (adminR)
                    {
                        CreateAll(oss, owner);
                    }
                    else
                    {
                        CreateOwn(oss, owner);
                    }
                }
            }
        }

        private void CreateOwn(OpenSendScore oss, Employee owner)
        {
            Score sc = ObjectSpace.CreateObject<Score>();
            sc.Office = owner.Office;
            sc.ERound = oss.ERound;
            sc.Owner = owner;
            foreach (PointOfEvaluation poe in oss.ERound.PoEs)
            {
                OwnScore oc = ObjectSpace.CreateObject<OwnScore>(); //Score of PoE
                oc.MainScore = sc;
                oc.PointOfEvaluation = poe;
                if (oc.PointOfEvaluation.SPoEs != null)
                {
                    foreach (SubPointOfEvaluation soe in poe.SPoEs)
                    {
                        OwnScore soc = ObjectSpace.CreateObject<OwnScore>(); // Score of SubPoE
                        soc.MainScore = sc;
                        soc.PointOfEvaluation = poe;
                        soc.SubPointOfEvaluation = soe;
                        OwnResult sor = ObjectSpace.FindObject<OwnResult>(CriteriaOperator.Parse("OwnScores=?", soc));
                        if(sor == null)
                        {
                            for (int i = (int)oss.ERound.MonthStart; i <= (int)oss.ERound.MonthStop; i++)
                            {
                                sor = ObjectSpace.CreateObject<OwnResult>();
                                sor.Office = owner.Office;
                                sor.PoE = poe;
                                sor.SPoE = soe;
                                sor.ResultMonth = (Emonth)i;
                                //sor.ResultMonthValue = ;
                                sor.Year = oss.ERound.Year;
                                sor.OwnScores.Add(soc);
                                sor.Save();
                            }
                        }
                        //soc.OwnResults;

                        AuditScore asoc = ObjectSpace.CreateObject<AuditScore>(); //Audit Score of SubPoE
                        asoc.OwnScore = soc;
                        asoc.Owner = owner;
                        asoc.Save();

                        soc.AuditScore = asoc;
                        soc.Save();

                        sc.OwnScores.Add(soc);
                    }
                }

                AuditScore aoc = ObjectSpace.CreateObject<AuditScore>();
                aoc.OwnScore = oc;
                aoc.Owner = owner;

                if (aoc.OwnScore.PointOfEvaluation.No == 1)
                {
                    string ofid;
                    if ((aoc.OwnScore.MainScore.Office.OfficeId.Substring(0, 3) == "000" && aoc.OwnScore.MainScore.Office.OfficeId.Substring(5, 3) == "000") || (aoc.OwnScore.MainScore.Office.OfficeId.Substring(2, 6) == "000000"))
                    { 
                        ofid = "00000000";
                    }
                    else
                    {
                        ofid = aoc.OwnScore.MainScore.Office.OfficeId.Substring(0, 2) + "000000";
                    }
                    for (Emonth i = aoc.OwnScore.MainScore.ERound.MonthStart; i <= aoc.OwnScore.MainScore.ERound.MonthStop; i++)
                    {
                        var url = "http://10.20.37.11:7072/serviceTier/webapi/All/officeId/" + ofid + "/year/" + (DateTime.Now.Year + 543).ToString() + "/month/" + (i + 1).ToString("D2");//DateTime.Now.Month.ToString("D2");
                        var tax = _download_serialized_json_data<Rootobject>(url);
                        var taxOwn = tax.taxCollection.FirstOrDefault(t => t.officeCode == aoc.OwnScore.MainScore.Office.OfficeId);
                        if (taxOwn != null)
                        {
                            DetailExpect expect = ObjectSpace.CreateObject<DetailExpect>();
                            expect.PoE = 1;
                            expect.ExpectYear = aoc.OwnScore.MainScore.VRound.Year;
                            expect.ExpectMonth = i;
                            expect.ExpectMonthValue = taxOwn.CMCYforcast;
                            expect.AuditScore = aoc;
                            expect.ExpectOffice = aoc.OwnScore.MainScore.Office;
                            expect.Save();

                            aoc.DetailExpects.Add(expect);

                            if (i == aoc.OwnScore.MainScore.VRound.MonthStop)
                            {
                                aoc.Result = taxOwn.CYcurrentYear;
                                oc.Result = taxOwn.CYcurrentYear;
                            }
                        }
                    }
                }

                aoc.Save();
                oc.AuditScore = aoc;

                oc.Save();
                sc.OwnScores.Add(oc);
            }
            sc.Save();
            ObjectSpace.CommitChanges();
        }

        private void CreateAll(OpenSendScore oss,Employee owner)
        {
            var listOffice = ObjectSpace.GetObjects<Office>(CriteriaOperator.Parse("OfficeId like '%000'"));
            foreach(Office office in listOffice)
            {
                Score sc = ObjectSpace.CreateObject<Score>();
                sc.Office = owner.Office;
                sc.ERound = oss.ERound;
                sc.Owner = owner;
                //var listPoE = ObjectSpace.GetObjects<PointOfEvaluation>(CriteriaOperator.Parse("VRound = ?",oss.ERound));
                foreach (PointOfEvaluation poe in oss.ERound.PoEs)
                {
                    OwnScore oc = ObjectSpace.CreateObject<OwnScore>(); //Score of PoE
                    oc.MainScore = sc;
                    oc.PointOfEvaluation = poe;
                    if (oc.PointOfEvaluation.SPoEs != null)
                    {
                        foreach (SubPointOfEvaluation soe in poe.SPoEs)
                        {
                            OwnScore soc = ObjectSpace.CreateObject<OwnScore>(); // Score of SubPoE
                            soc.MainScore = sc;
                            soc.PointOfEvaluation = poe;
                            soc.SubPointOfEvaluation = soe;

                            AuditScore asoc = ObjectSpace.CreateObject<AuditScore>(); //Audit Score of SubPoE
                            asoc.OwnScore = soc;
                            asoc.Owner = owner;
                            asoc.Save();

                            soc.AuditScore = asoc;
                            soc.Save();

                            sc.OwnScores.Add(soc);
                        }
                    }

                    AuditScore aoc = ObjectSpace.CreateObject<AuditScore>();
                    aoc.OwnScore = oc;
                    aoc.Owner = owner;

                    if (aoc.OwnScore.PointOfEvaluation.No == 1)
                    {
                        string ofid;
                        if ((aoc.OwnScore.MainScore.Office.OfficeId.Substring(0, 3) == "000" && aoc.OwnScore.MainScore.Office.OfficeId.Substring(5, 3) == "000") || (aoc.OwnScore.MainScore.Office.OfficeId.Substring(2, 6) == "000000"))
                        {
                            ofid = "00000000";
                        }
                        else
                        {
                            ofid = aoc.OwnScore.MainScore.Office.OfficeId.Substring(0, 2) + "000000";
                        }
                        for (Emonth i = aoc.OwnScore.MainScore.ERound.MonthStart; i <= aoc.OwnScore.MainScore.ERound.MonthStop; i++)
                        {
                            var url = "http://10.20.37.11:7072/serviceTier/webapi/All/officeId/" + ofid + "/year/" + (DateTime.Now.Year + 543).ToString() + "/month/" + (i + 1).ToString("D2");//DateTime.Now.Month.ToString("D2");
                            var tax = _download_serialized_json_data<Rootobject>(url);
                            var taxOwn = tax.taxCollection.FirstOrDefault(t => t.officeCode == aoc.OwnScore.MainScore.Office.OfficeId);
                            if (taxOwn != null)
                            {
                                DetailExpect expect = ObjectSpace.CreateObject<DetailExpect>();
                                expect.PoE = 1;
                                expect.ExpectYear = aoc.OwnScore.MainScore.VRound.Year;
                                expect.ExpectMonth = i;
                                expect.ExpectMonthValue = taxOwn.CMCYforcast;
                                expect.AuditScore = aoc;
                                expect.ExpectOffice = aoc.OwnScore.MainScore.Office;
                                expect.Save();

                                aoc.DetailExpects.Add(expect);

                                if (i == aoc.OwnScore.MainScore.VRound.MonthStop)
                                {
                                    aoc.Result = taxOwn.CYcurrentYear;
                                    oc.Result = taxOwn.CYcurrentYear;
                                }
                            }
                        }
                    }

                    aoc.Save();
                    oc.AuditScore = aoc;

                    oc.Save();
                    sc.OwnScores.Add(oc);
                }
                sc.Save();
            }
            ObjectSpace.CommitChanges();
        }
    }
}
