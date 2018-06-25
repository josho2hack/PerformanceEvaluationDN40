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
                //CreateAll(oss, owner);
                Score oscore = ObjectSpace.FindObject<Score>(CriteriaOperator.Parse("Office= ? AND ERound = ?", owner.Office, oss.ERound));
                if (oscore == null)
                {
                    bool adminR = false;
                    foreach (PermissionPolicyRole p in owner.Roles)
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
            decimal sum = 0;

            string ofid;
            if ((sc.Office.OfficeId.Substring(0, 3) == "000" && sc.Office.OfficeId.Substring(5, 3) == "000") || (sc.Office.OfficeId.Substring(2, 6) == "000000"))
            {
                ofid = "00000000";
            }
            else
            {
                ofid = sc.Office.OfficeId.Substring(0, 2) + "000000";
            }

            foreach (PointOfEvaluation poe in oss.ERound.PoEs)
            {
                OwnScore oc = ObjectSpace.CreateObject<OwnScore>(); // PoE
                oc.MainScore = sc;
                oc.PointOfEvaluation = poe;
                if (oc.PointOfEvaluation.SPoEs != null)
                {
                    foreach (SubPointOfEvaluation soe in poe.SPoEs)
                    {
                        OwnScore soc = ObjectSpace.CreateObject<OwnScore>(); // SubPoE
                        soc.MainScore = sc;
                        soc.PointOfEvaluation = poe;
                        soc.SubPointOfEvaluation = soe;

                        AuditScore asoc = ObjectSpace.CreateObject<AuditScore>(); //Audit Score of SubPoE
                        asoc.OwnScore = soc;
                        asoc.Owner = owner;

                        DetailExpect sexpect = ObjectSpace.FindObject<DetailExpect>(CriteriaOperator.Parse("Office=? AND PoE=? AND SPoE=? AND Year=?", owner.Office, poe, soe, oss.ERound.Year));
                        if (sexpect == null)
                        {
                            sum = 0;
                            for (Emonth i = oss.ERound.MonthStart; i <= oss.ERound.MonthStop; i++)
                            {
                                sexpect = ObjectSpace.CreateObject<DetailExpect>();
                                sexpect.Office = owner.Office;
                                sexpect.PoE = poe;
                                sexpect.SPoE = soe;
                                sexpect.ExpectMonth = i;
                                if (poe.No == 1)
                                {
                                    Emonth j;
                                    if (i == Emonth.ตุลาคม || i == Emonth.พฤศจิกายน || i == Emonth.ธันวาคม)
                                    {
                                        j = i + 10;
                                    }
                                    else
                                    {
                                        j = i - 2;
                                    }
                                    var url = "http://10.20.37.11:7072/serviceTier/webapi/All/officeId/" + ofid + "/year/" + oss.ERound.Year.ToString("D4") + "/month/" + ((int)j).ToString("D2");//DateTime.Now.Month.ToString("D2");
                                    var tax = _download_serialized_json_data<Rootobject>(url);
                                    var taxOwn = tax.taxCollection.FirstOrDefault(t => t.officeCode == sc.Office.OfficeId);
                                    if (taxOwn != null)
                                    {
                                        sexpect.ExpectMonthValue = taxOwn.CMCYforcast;
                                    }
                                }
                                //sexpect.ExpectMonthValue = ;
                                sexpect.Year = oss.ERound.Year;
                                sexpect.AuditScores.Add(asoc);
                                sexpect.Save();
                                asoc.DetailExpects.Add(sexpect);
                                sum += sexpect.ExpectMonthValue;
                            }
                            asoc.Expect = sum;
                        }
                        else
                        {
                            sum = 0;
                            for (Emonth i = oss.ERound.MonthStart; i <= oss.ERound.MonthStop; i++)
                            {
                                sexpect = ObjectSpace.FindObject<DetailExpect>(CriteriaOperator.Parse("Office=? AND PoE=? AND SPoE=? AND Year=? AND ExpectMonth=?", owner.Office, poe, soe, oss.ERound.Year, i));

                                asoc.DetailExpects.Add(sexpect);
                                sum += sexpect.ExpectMonthValue;
                            }
                            asoc.Expect = sum;
                        }//End Crete Sub DetailExpect.
                        ObjectSpace.CommitChanges();

                        OwnResult sor = ObjectSpace.FindObject<OwnResult>(CriteriaOperator.Parse("Office=? AND PoE=? AND SPoE=? AND Year=?", owner.Office, poe, soe, oss.ERound.Year));
                        if (sor == null)
                        {
                            sum = 0;
                            for (Emonth i = oss.ERound.MonthStart; i <= oss.ERound.MonthStop; i++)
                            {
                                sor = ObjectSpace.CreateObject<OwnResult>();
                                sor.Office = owner.Office;
                                sor.PoE = poe;
                                sor.SPoE = soe;
                                sor.ResultMonth = i;
                                if (poe.No == 1)
                                {
                                    Emonth j;
                                    if (i == Emonth.ตุลาคม || i == Emonth.พฤศจิกายน || i == Emonth.ธันวาคม)
                                    {
                                        j = i + 10;
                                    }
                                    else
                                    {
                                        j = i - 2;
                                    }
                                    var url = "http://10.20.37.11:7072/serviceTier/webapi/All/officeId/" + ofid + "/year/" + oss.ERound.Year.ToString("D4") + "/month/" + ((int)j).ToString("D2");//DateTime.Now.Month.ToString("D2");
                                    var tax = _download_serialized_json_data<Rootobject>(url);
                                    var taxOwn = tax.taxCollection.FirstOrDefault(t => t.officeCode == sc.Office.OfficeId);
                                    if (taxOwn != null)
                                    {
                                        sor.ResultMonthValue = taxOwn.CMcurrentYear;
                                    }
                                }
                                //sor.ResultMonthValue = ;
                                sor.Year = oss.ERound.Year;
                                sor.OwnScores.Add(soc);
                                sor.Save();
                                soc.OwnResults.Add(sor);
                                sum += sor.ResultMonthValue;
                            }
                            soc.Result = sum;
                        }
                        else
                        {
                            sum = 0;
                            for (Emonth i = oss.ERound.MonthStart; i <= oss.ERound.MonthStop; i++)
                            {
                                sor = ObjectSpace.FindObject<OwnResult>(CriteriaOperator.Parse("Office=? AND PoE=? AND SPoE=? AND Year=? AND ResultMonth=?", owner.Office, poe, soe, oss.ERound.Year, i));
                                sum += sor.ResultMonthValue;
                                soc.OwnResults.Add(sor);
                            }
                            soc.Result = sum;
                        }//End Crete Sub OwnerResult.



                        ObjectSpace.CommitChanges();



                        AuditResult asResult = ObjectSpace.FindObject<AuditResult>(CriteriaOperator.Parse("Office=? AND PoE=? AND SPoE=? AND Year=?", owner.Office, poe, soe, oss.ERound.Year));
                        if (asResult == null)
                        {
                            sum = 0;
                            for (Emonth i = oss.ERound.MonthStart; i <= oss.ERound.MonthStop; i++)
                            {
                                asResult = ObjectSpace.CreateObject<AuditResult>();
                                asResult.Office = owner.Office;
                                asResult.PoE = poe;
                                asResult.SPoE = soe;
                                asResult.ResultMonth = i;
                                if (poe.No == 1)
                                {
                                    Emonth j;
                                    if (i == Emonth.ตุลาคม || i == Emonth.พฤศจิกายน || i == Emonth.ธันวาคม)
                                    {
                                        j = i + 10;
                                    }
                                    else
                                    {
                                        j = i - 2;
                                    }
                                    var url = "http://10.20.37.11:7072/serviceTier/webapi/All/officeId/" + ofid + "/year/" + oss.ERound.Year.ToString("D4") + "/month/" + ((int)j).ToString("D2");//DateTime.Now.Month.ToString("D2");
                                    var tax = _download_serialized_json_data<Rootobject>(url);
                                    var taxOwn = tax.taxCollection.FirstOrDefault(t => t.officeCode == sc.Office.OfficeId);
                                    if (taxOwn != null)
                                    {
                                        asResult.ResultMonthValue = taxOwn.CMcurrentYear;
                                    }
                                }
                                //asResult.ResultMonthValue = ;
                                asResult.Year = oss.ERound.Year;
                                asResult.AuditScores.Add(asoc);
                                asResult.Save();
                                asoc.AuditResults.Add(asResult);
                                sum += asResult.ResultMonthValue;
                            }
                            asoc.Result = sum;
                        }
                        else
                        {
                            sum = 0;
                            for (Emonth i = oss.ERound.MonthStart; i <= oss.ERound.MonthStop; i++)
                            {
                                asResult = ObjectSpace.FindObject<AuditResult>(CriteriaOperator.Parse("Office=? AND PoE=? AND SPoE=? AND Year=? AND ResultMonth=?", owner.Office, poe, soe, oss.ERound.Year, i));

                                asoc.AuditResults.Add(asResult);
                                sum += asResult.ResultMonthValue;
                            }
                            asoc.Result = sum;
                        }//End Crete Sub AuditResult.

                        asoc.Save();

                        //Add Audit of Sub Owner Score
                        soc.AuditScore = asoc;
                        soc.Save();

                        //Add Sub Owner Score to Main Score
                        sc.OwnScores.Add(soc);
                        ObjectSpace.CommitChanges();
                    }
                }//End SupPointOfEvaluation

                AuditScore aoc = ObjectSpace.CreateObject<AuditScore>();
                aoc.OwnScore = oc;
                aoc.Owner = owner;

                DetailExpect expect = ObjectSpace.FindObject<DetailExpect>(CriteriaOperator.Parse("Office=? AND PoE=? AND SPoE=? AND Year=?", owner.Office, poe, null, oss.ERound.Year));
                if (expect == null)
                {
                    sum = 0;
                    for (Emonth i = oss.ERound.MonthStart; i <= oss.ERound.MonthStop; i++)
                    {
                        expect = ObjectSpace.CreateObject<DetailExpect>();
                        expect.Office = owner.Office;
                        expect.PoE = poe;
                        expect.ExpectMonth = i;
                        if (poe.No == 1)
                        {
                            Emonth j;
                            if (i == Emonth.ตุลาคม || i == Emonth.พฤศจิกายน || i == Emonth.ธันวาคม)
                            {
                                j = i + 10;
                            }
                            else
                            {
                                j = i - 2;
                            }
                            var url = "http://10.20.37.11:7072/serviceTier/webapi/All/officeId/" + ofid + "/year/" + oss.ERound.Year.ToString("D4") + "/month/" + ((int)j).ToString("D2");//DateTime.Now.Month.ToString("D2");
                            var tax = _download_serialized_json_data<Rootobject>(url);
                            var taxOwn = tax.taxCollection.FirstOrDefault(t => t.officeCode == sc.Office.OfficeId);
                            if (taxOwn != null)
                            {
                                expect.ExpectMonthValue = taxOwn.CMCYforcast;
                            }
                        }
                        expect.Year = oss.ERound.Year;
                        expect.AuditScores.Add(aoc);
                        expect.Save();
                        aoc.DetailExpects.Add(expect);
                        sum += expect.ExpectMonthValue;
                    }
                    aoc.Expect = sum;
                }
                else
                {
                    sum = 0;
                    for (Emonth i = oss.ERound.MonthStart; i <= oss.ERound.MonthStop; i++)
                    {
                        expect = ObjectSpace.FindObject<DetailExpect>(CriteriaOperator.Parse("Office=? AND PoE=? AND SPoE=? AND Year=? AND ExpectMonth=?", owner.Office, poe, null, oss.ERound.Year, i));

                        aoc.DetailExpects.Add(expect);
                        sum += expect.ExpectMonthValue;
                    }
                    aoc.Expect = sum;
                }//End Crete DetailExpect.

                ObjectSpace.CommitChanges();

                OwnResult or = ObjectSpace.FindObject<OwnResult>(CriteriaOperator.Parse("Office=? AND PoE=? AND SPoE=? AND Year=?", owner.Office, poe, null, oss.ERound.Year));
                if (or == null)
                {
                    sum = 0;
                    for (Emonth i = oss.ERound.MonthStart; i <= oss.ERound.MonthStop; i++)
                    {
                        or = ObjectSpace.CreateObject<OwnResult>();
                        or.Office = owner.Office;
                        or.PoE = poe;
                        or.ResultMonth = i;

                        if (poe.No == 1)
                        {
                            Emonth j;
                            if (i == Emonth.ตุลาคม || i == Emonth.พฤศจิกายน || i == Emonth.ธันวาคม)
                            {
                                j = i + 10;
                            }
                            else
                            {
                                j = i - 2;
                            }
                            var url = "http://10.20.37.11:7072/serviceTier/webapi/All/officeId/" + ofid + "/year/" + oss.ERound.Year.ToString("D4") + "/month/" + ((int)j).ToString("D2");//DateTime.Now.Month.ToString("D2");
                            var tax = _download_serialized_json_data<Rootobject>(url);
                            var taxOwn = tax.taxCollection.FirstOrDefault(t => t.officeCode == sc.Office.OfficeId);
                            if (taxOwn != null)
                            {
                                or.ResultMonthValue = taxOwn.CMcurrentYear;
                            }
                        }

                        or.Year = oss.ERound.Year;
                        or.OwnScores.Add(oc);
                        or.Save();
                        oc.OwnResults.Add(or);
                        sum += or.ResultMonthValue;
                    }
                    oc.Result = sum;
                }
                else
                {
                    sum = 0;
                    for (Emonth i = oss.ERound.MonthStart; i <= oss.ERound.MonthStop; i++)
                    {
                        or = ObjectSpace.FindObject<OwnResult>(CriteriaOperator.Parse("Office=? AND PoE=? AND SPoE=? AND Year=? AND ResultMonth=?", owner.Office, poe, null, oss.ERound.Year, i));

                        oc.OwnResults.Add(or);
                        sum += or.ResultMonthValue;
                    }
                    oc.Result = sum;
                }//End Crete OwnerResult.

                ObjectSpace.CommitChanges();

                AuditResult aResult = ObjectSpace.FindObject<AuditResult>(CriteriaOperator.Parse("Office=? AND PoE=? AND SPoE=? AND Year=?", owner.Office, poe, null, oss.ERound.Year));
                if (aResult == null)
                {
                    sum = 0;
                    for (Emonth i = oss.ERound.MonthStart; i <= oss.ERound.MonthStop; i++)
                    {
                        aResult = ObjectSpace.CreateObject<AuditResult>();
                        aResult.Office = owner.Office;
                        aResult.PoE = poe;
                        aResult.ResultMonth = i;
                        if (poe.No == 1)
                        {
                            Emonth j;
                            if (i == Emonth.ตุลาคม || i == Emonth.พฤศจิกายน || i == Emonth.ธันวาคม)
                            {
                                j = i + 10;
                            }
                            else
                            {
                                j = i - 2;
                            }
                            var url = "http://10.20.37.11:7072/serviceTier/webapi/All/officeId/" + ofid + "/year/" + oss.ERound.Year.ToString("D4") + "/month/" + ((int)j).ToString("D2");//DateTime.Now.Month.ToString("D2");
                            var tax = _download_serialized_json_data<Rootobject>(url);
                            var taxOwn = tax.taxCollection.FirstOrDefault(t => t.officeCode == sc.Office.OfficeId);
                            if (taxOwn != null)
                            {
                                aResult.ResultMonthValue = taxOwn.CMcurrentYear;
                            }
                        }
                        aResult.Year = oss.ERound.Year;
                        aResult.AuditScores.Add(aoc);
                        aResult.Save();
                        aoc.AuditResults.Add(aResult);
                        sum += aResult.ResultMonthValue;
                    }
                    aoc.Result = sum;
                }
                else
                {
                    sum = 0;
                    for (Emonth i = oss.ERound.MonthStart; i <= oss.ERound.MonthStop; i++)
                    {
                        aResult = ObjectSpace.FindObject<AuditResult>(CriteriaOperator.Parse("Office=? AND PoE=? AND SPoE=? AND Year=? AND ResultMonth=?", owner.Office, poe, null, oss.ERound.Year, i));

                        aoc.AuditResults.Add(aResult);
                        sum += aResult.ResultMonthValue;
                    }
                    aoc.Result = sum;
                }//End Crete AuditResult.

                aoc.Save();
                oc.AuditScore = aoc;

                oc.Save();
                sc.OwnScores.Add(oc);
            }//End PointOfEvaluation Loop

            sc.Save(); //Save Score
            ObjectSpace.CommitChanges();
        }//End Create Score by Owner--------------------------------------------------------------------------------------------

        private void CreateAll(OpenSendScore oss,Employee owner)
        {
            var listOffice = ObjectSpace.GetObjects<Office>(CriteriaOperator.Parse("OfficeId like '%000'"));
            foreach(Office office in listOffice)
            {
                Score sc = ObjectSpace.CreateObject<Score>();
                sc.Office = office;
                sc.ERound = oss.ERound;
                sc.Owner = owner;
                decimal sum = 0;

                string ofid;
                if ((sc.Office.OfficeId.Substring(0, 3) == "000" && sc.Office.OfficeId.Substring(5, 3) == "000") || (sc.Office.OfficeId.Substring(2, 6) == "000000"))
                {
                    ofid = "00000000";
                }
                else
                {
                    ofid = sc.Office.OfficeId.Substring(0, 2) + "000000";
                }

                foreach (PointOfEvaluation poe in oss.ERound.PoEs)
                {
                    OwnScore oc = ObjectSpace.CreateObject<OwnScore>(); // PoE
                    oc.MainScore = sc;
                    oc.PointOfEvaluation = poe;
                    if (oc.PointOfEvaluation.SPoEs != null)
                    {
                        foreach (SubPointOfEvaluation soe in poe.SPoEs)
                        {
                            OwnScore soc = ObjectSpace.CreateObject<OwnScore>(); // SubPoE
                            soc.MainScore = sc;
                            soc.PointOfEvaluation = poe;
                            soc.SubPointOfEvaluation = soe;

                            AuditScore asoc = ObjectSpace.CreateObject<AuditScore>(); //Audit Score of SubPoE
                            asoc.OwnScore = soc;
                            asoc.Owner = owner;

                            DetailExpect sexpect = ObjectSpace.FindObject<DetailExpect>(CriteriaOperator.Parse("Office=? AND PoE=? AND SPoE=? AND Year=?", office, poe, soe, oss.ERound.Year));
                            if (sexpect == null)
                            {
                                sum = 0;
                                for (Emonth i = oss.ERound.MonthStart; i <= oss.ERound.MonthStop; i++)
                                {
                                    sexpect = ObjectSpace.CreateObject<DetailExpect>();
                                    sexpect.Office = office;
                                    sexpect.PoE = poe;
                                    sexpect.SPoE = soe;
                                    sexpect.ExpectMonth = i;
                                    if (poe.No == 1)
                                    {
                                        Emonth j;
                                        if (i == Emonth.ตุลาคม || i == Emonth.พฤศจิกายน || i == Emonth.ธันวาคม)
                                        {
                                            j = i + 10;
                                        }
                                        else
                                        {
                                            j = i - 2;
                                        }
                                        var url = "http://10.20.37.11:7072/serviceTier/webapi/All/officeId/" + ofid + "/year/" + oss.ERound.Year.ToString("D4") + "/month/" + ((int)j).ToString("D2");//DateTime.Now.Month.ToString("D2");
                                        var tax = _download_serialized_json_data<Rootobject>(url);
                                        var taxOwn = tax.taxCollection.FirstOrDefault(t => t.officeCode == sc.Office.OfficeId);
                                        if (taxOwn != null)
                                        {
                                            sexpect.ExpectMonthValue = taxOwn.CMCYforcast;
                                        }
                                    }
                                    //sexpect.ExpectMonthValue = ;
                                    sexpect.Year = oss.ERound.Year;
                                    sexpect.AuditScores.Add(asoc);
                                    sexpect.Save();
                                    asoc.DetailExpects.Add(sexpect);
                                    sum += sexpect.ExpectMonthValue;
                                }
                                asoc.Expect = sum;
                            }
                            else
                            {
                                sum = 0;
                                for (Emonth i = oss.ERound.MonthStart; i <= oss.ERound.MonthStop; i++)
                                {
                                    sexpect = ObjectSpace.FindObject<DetailExpect>(CriteriaOperator.Parse("Office=? AND PoE=? AND SPoE=? AND Year=? AND ExpectMonth=?", office, poe, soe, oss.ERound.Year, i));

                                    asoc.DetailExpects.Add(sexpect);
                                    sum += sexpect.ExpectMonthValue;
                                }
                                asoc.Expect = sum;
                            }//End Crete Sub DetailExpect.
                            ObjectSpace.CommitChanges();

                            OwnResult sor = ObjectSpace.FindObject<OwnResult>(CriteriaOperator.Parse("Office=? AND PoE=? AND SPoE=? AND Year=?", office, poe, soe, oss.ERound.Year));
                            if (sor == null)
                            {
                                sum = 0;
                                for (Emonth i = oss.ERound.MonthStart; i <= oss.ERound.MonthStop; i++)
                                {
                                    sor = ObjectSpace.CreateObject<OwnResult>();
                                    sor.Office = owner.Office;
                                    sor.PoE = poe;
                                    sor.SPoE = soe;
                                    sor.ResultMonth = i;
                                    if (poe.No == 1)
                                    {
                                        Emonth j;
                                        if (i == Emonth.ตุลาคม || i == Emonth.พฤศจิกายน || i == Emonth.ธันวาคม)
                                        {
                                            j = i + 10;
                                        }
                                        else
                                        {
                                            j = i - 2;
                                        }
                                        var url = "http://10.20.37.11:7072/serviceTier/webapi/All/officeId/" + ofid + "/year/" + oss.ERound.Year.ToString("D4") + "/month/" + ((int)j).ToString("D2");//DateTime.Now.Month.ToString("D2");
                                        var tax = _download_serialized_json_data<Rootobject>(url);
                                        var taxOwn = tax.taxCollection.FirstOrDefault(t => t.officeCode == sc.Office.OfficeId);
                                        if (taxOwn != null)
                                        {
                                            sor.ResultMonthValue = taxOwn.CMcurrentYear;
                                        }
                                    }
                                    //sor.ResultMonthValue = ;
                                    sor.Year = oss.ERound.Year;
                                    sor.OwnScores.Add(soc);
                                    sor.Save();
                                    soc.OwnResults.Add(sor);
                                    sum += sor.ResultMonthValue;
                                }
                                soc.Result = sum;
                            }
                            else
                            {
                                sum = 0;
                                for (Emonth i = oss.ERound.MonthStart; i <= oss.ERound.MonthStop; i++)
                                {
                                    sor = ObjectSpace.FindObject<OwnResult>(CriteriaOperator.Parse("Office=? AND PoE=? AND SPoE=? AND Year=? AND ResultMonth=?", office, poe, soe, oss.ERound.Year, i));
                                    sum += sor.ResultMonthValue;
                                    soc.OwnResults.Add(sor);
                                }
                                soc.Result = sum;
                            }//End Crete Sub OwnerResult.

                            

                            ObjectSpace.CommitChanges();

                            

                            AuditResult asResult = ObjectSpace.FindObject<AuditResult>(CriteriaOperator.Parse("Office=? AND PoE=? AND SPoE=? AND Year=?", office, poe, soe, oss.ERound.Year));
                            if (asResult == null)
                            {
                                sum = 0;
                                for (Emonth i = oss.ERound.MonthStart; i <= oss.ERound.MonthStop; i++)
                                {
                                    asResult = ObjectSpace.CreateObject<AuditResult>();
                                    asResult.Office = office;
                                    asResult.PoE = poe;
                                    asResult.SPoE = soe;
                                    asResult.ResultMonth = i;
                                    if (poe.No == 1)
                                    {
                                        Emonth j;
                                        if (i == Emonth.ตุลาคม || i == Emonth.พฤศจิกายน || i == Emonth.ธันวาคม)
                                        {
                                            j = i + 10;
                                        }
                                        else
                                        {
                                            j = i - 2;
                                        }
                                        var url = "http://10.20.37.11:7072/serviceTier/webapi/All/officeId/" + ofid + "/year/" + oss.ERound.Year.ToString("D4") + "/month/" + ((int)j).ToString("D2");//DateTime.Now.Month.ToString("D2");
                                        var tax = _download_serialized_json_data<Rootobject>(url);
                                        var taxOwn = tax.taxCollection.FirstOrDefault(t => t.officeCode == sc.Office.OfficeId);
                                        if (taxOwn != null)
                                        {
                                            asResult.ResultMonthValue = taxOwn.CMcurrentYear;
                                        }
                                    }
                                    //asResult.ResultMonthValue = ;
                                    asResult.Year = oss.ERound.Year;
                                    asResult.AuditScores.Add(asoc);
                                    asResult.Save();
                                    asoc.AuditResults.Add(asResult);
                                    sum += asResult.ResultMonthValue;
                                }
                                asoc.Result = sum;
                            }
                            else
                            {
                                sum = 0;
                                for (Emonth i = oss.ERound.MonthStart; i <= oss.ERound.MonthStop; i++)
                                {
                                    asResult = ObjectSpace.FindObject<AuditResult>(CriteriaOperator.Parse("Office=? AND PoE=? AND SPoE=? AND Year=? AND ResultMonth=?", office, poe, soe, oss.ERound.Year, i));
                                    
                                    asoc.AuditResults.Add(asResult);
                                    sum += asResult.ResultMonthValue;
                                }
                                asoc.Result = sum;
                            }//End Crete Sub AuditResult.

                            asoc.Save();

                            //Add Audit of Sub Owner Score
                            soc.AuditScore = asoc;
                            soc.Save();

                            //Add Sub Owner Score to Main Score
                            sc.OwnScores.Add(soc);
                            ObjectSpace.CommitChanges();
                        }
                    }//End SupPointOfEvaluation

                    AuditScore aoc = ObjectSpace.CreateObject<AuditScore>();
                    aoc.OwnScore = oc;
                    aoc.Owner = owner;

                    DetailExpect expect = ObjectSpace.FindObject<DetailExpect>(CriteriaOperator.Parse("Office=? AND PoE=? AND SPoE=? AND Year=?", office, poe, null, oss.ERound.Year));
                    if (expect == null)
                    {
                        sum = 0;
                        for (Emonth i = oss.ERound.MonthStart; i <= oss.ERound.MonthStop; i++)
                        {
                            expect = ObjectSpace.CreateObject<DetailExpect>();
                            expect.Office = office;
                            expect.PoE = poe;
                            expect.ExpectMonth = i;
                            if (poe.No == 1)
                            {
                                Emonth j;
                                if (i == Emonth.ตุลาคม || i == Emonth.พฤศจิกายน || i == Emonth.ธันวาคม)
                                {
                                    j = i + 10;
                                }
                                else
                                {
                                    j = i - 2;
                                }
                                var url = "http://10.20.37.11:7072/serviceTier/webapi/All/officeId/" + ofid + "/year/" + oss.ERound.Year.ToString("D4") + "/month/" + ((int)j).ToString("D2");//DateTime.Now.Month.ToString("D2");
                                var tax = _download_serialized_json_data<Rootobject>(url);
                                var taxOwn = tax.taxCollection.FirstOrDefault(t => t.officeCode == sc.Office.OfficeId);
                                if (taxOwn != null)
                                {
                                    expect.ExpectMonthValue = taxOwn.CMCYforcast;
                                }
                            }
                            expect.Year = oss.ERound.Year;
                            expect.AuditScores.Add(aoc);
                            expect.Save();
                            aoc.DetailExpects.Add(expect);
                            sum += expect.ExpectMonthValue;
                        }
                        aoc.Expect = sum;
                    }
                    else
                    {
                        sum = 0;
                        for (Emonth i = oss.ERound.MonthStart; i <= oss.ERound.MonthStop; i++)
                        {
                            expect = ObjectSpace.FindObject<DetailExpect>(CriteriaOperator.Parse("Office=? AND PoE=? AND SPoE=? AND Year=? AND ExpectMonth=?", office, poe, null, oss.ERound.Year, i));

                            aoc.DetailExpects.Add(expect);
                            sum += expect.ExpectMonthValue;
                        }
                        aoc.Expect = sum;
                    }//End Crete DetailExpect.

                    ObjectSpace.CommitChanges();

                    OwnResult or = ObjectSpace.FindObject<OwnResult>(CriteriaOperator.Parse("Office=? AND PoE=? AND SPoE=? AND Year=?", office, poe, null, oss.ERound.Year));
                    if (or == null)
                    {
                        sum = 0;
                        for (Emonth i = oss.ERound.MonthStart; i <= oss.ERound.MonthStop; i++)
                        {
                            or = ObjectSpace.CreateObject<OwnResult>();
                            or.Office = office;
                            or.PoE = poe;
                            or.ResultMonth = i;

                            if (poe.No == 1)
                            {
                                Emonth j;
                                if (i == Emonth.ตุลาคม || i == Emonth.พฤศจิกายน || i == Emonth.ธันวาคม)
                                {
                                    j = i + 10;
                                }
                                else
                                {
                                    j = i - 2;
                                }
                                var url = "http://10.20.37.11:7072/serviceTier/webapi/All/officeId/" + ofid + "/year/" + oss.ERound.Year.ToString("D4") + "/month/" + ((int)j).ToString("D2");//DateTime.Now.Month.ToString("D2");
                                var tax = _download_serialized_json_data<Rootobject>(url);
                                var taxOwn = tax.taxCollection.FirstOrDefault(t => t.officeCode == sc.Office.OfficeId);
                                if (taxOwn != null)
                                {
                                    or.ResultMonthValue = taxOwn.CMcurrentYear;
                                }
                            }

                            or.Year = oss.ERound.Year;
                            or.OwnScores.Add(oc);
                            or.Save();
                            oc.OwnResults.Add(or);
                            sum += or.ResultMonthValue;
                        }
                        oc.Result = sum;
                    }
                    else
                    {
                        sum = 0;
                        for (Emonth i = oss.ERound.MonthStart; i <= oss.ERound.MonthStop; i++)
                        {
                            or = ObjectSpace.FindObject<OwnResult>(CriteriaOperator.Parse("Office=? AND PoE=? AND SPoE=? AND Year=? AND ResultMonth=?", office, poe, null, oss.ERound.Year, i));
                            
                            oc.OwnResults.Add(or);
                            sum += or.ResultMonthValue;
                        }
                        oc.Result = sum;
                    }//End Crete OwnerResult.

                    ObjectSpace.CommitChanges();

                    AuditResult aResult = ObjectSpace.FindObject<AuditResult>(CriteriaOperator.Parse("Office=? AND PoE=? AND SPoE=? AND Year=?", office, poe, null, oss.ERound.Year));
                    if (aResult == null)
                    {
                        sum = 0;
                        for (Emonth i = oss.ERound.MonthStart; i <= oss.ERound.MonthStop; i++)
                        {
                            aResult = ObjectSpace.CreateObject<AuditResult>();
                            aResult.Office = office;
                            aResult.PoE = poe;
                            aResult.ResultMonth = i;
                            if (poe.No == 1)
                            {
                                Emonth j;
                                if (i == Emonth.ตุลาคม || i == Emonth.พฤศจิกายน || i == Emonth.ธันวาคม)
                                {
                                    j = i + 10;
                                }
                                else
                                {
                                    j = i - 2;
                                }
                                var url = "http://10.20.37.11:7072/serviceTier/webapi/All/officeId/" + ofid + "/year/" + oss.ERound.Year.ToString("D4") + "/month/" + ((int)j).ToString("D2");//DateTime.Now.Month.ToString("D2");
                                var tax = _download_serialized_json_data<Rootobject>(url);
                                var taxOwn = tax.taxCollection.FirstOrDefault(t => t.officeCode == sc.Office.OfficeId);
                                if (taxOwn != null)
                                {
                                    aResult.ResultMonthValue = taxOwn.CMcurrentYear;
                                }
                            }
                            aResult.Year = oss.ERound.Year;
                            aResult.AuditScores.Add(aoc);
                            aResult.Save();
                            aoc.AuditResults.Add(aResult);
                            sum += aResult.ResultMonthValue;
                        }
                        aoc.Result = sum;
                    }
                    else
                    {
                        sum = 0;
                        for (Emonth i = oss.ERound.MonthStart; i <= oss.ERound.MonthStop; i++)
                        {
                            aResult = ObjectSpace.FindObject<AuditResult>(CriteriaOperator.Parse("Office=? AND PoE=? AND SPoE=? AND Year=? AND ResultMonth=?", office, poe, null, oss.ERound.Year, i));
                            
                            aoc.AuditResults.Add(aResult);
                            sum += aResult.ResultMonthValue;
                        }
                        aoc.Result = sum;
                    }//End Crete AuditResult.

                    aoc.Save();
                    oc.AuditScore = aoc;

                    oc.Save();
                    sc.OwnScores.Add(oc);
                }//End PointOfEvaluation Loop

                sc.Save(); //Save Score
                ObjectSpace.CommitChanges();
            }
            //ObjectSpace.CommitChanges();
        }
    }
}
