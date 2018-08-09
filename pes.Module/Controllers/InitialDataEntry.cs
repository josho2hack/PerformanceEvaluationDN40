using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using DevExpress.Persistent.Validation;
using Newtonsoft.Json;
using pes.Module.BusinessObjects;

namespace pes.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class InitialDataEntryController : ViewController
    {
        public InitialDataEntryController()
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

        private void CreateAll(OpenSendScore oss, Employee owner)
        {
            var listOffice = ObjectSpace.GetObjects<Office>(CriteriaOperator.Parse("OfficeId like '%000'"));
            foreach (Office office in listOffice)
            {
                if (office.OfficeId.Substring(0, 5) == "00001" || office.OfficeId.Substring(0, 5) == "00002" || office.OfficeId.Substring(0, 5) == "00003" || office.OfficeId.Substring(0, 5) == "00004" || office.OfficeId.Substring(0, 5) == "00004" || office.OfficeId.Substring(0, 5) == "00005" || office.OfficeId.Substring(0, 5) == "00006" || office.OfficeId.Substring(0, 5) == "00007" || office.OfficeId.Substring(0, 5) == "00008" || office.OfficeId.Substring(0, 5) == "00009" || office.OfficeId.Substring(0, 4) == "0001" || office.OfficeId.Substring(0, 4) == "0002") continue;

                Score sc = ObjectSpace.FindObject<Score>(CriteriaOperator.Parse("ERound = ? AND Office=?", oss.ERound, office));
                if (sc == null)
                {
                    sc = ObjectSpace.CreateObject<Score>();
                    sc.Office = office;
                    sc.ERound = oss.ERound;
                    sc.Owner = owner;
                }

                decimal sum;

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
                    OwnScore oc = ObjectSpace.FindObject<OwnScore>(CriteriaOperator.Parse("MainScore = ? AND PointOfEvaluation=?", sc, poe));
                    AuditScore aoc;
                    if (oc == null)
                    {
                        oc = ObjectSpace.CreateObject<OwnScore>(); // PoE
                        oc.MainScore = sc;
                        oc.PointOfEvaluation = poe;

                        aoc = ObjectSpace.CreateObject<AuditScore>();
                        aoc.OwnScore = oc;
                        aoc.Owner = owner;
                    }
                    else
                    {
                        if (oc.AuditScore == null)
                        {
                            aoc = ObjectSpace.FindObject<AuditScore>(CriteriaOperator.Parse("OwnScore = ?", oc));
                            if (aoc == null)
                            {
                                aoc = ObjectSpace.CreateObject<AuditScore>(); //Audit Score of SubPoE
                                aoc.OwnScore = oc;
                                aoc.Owner = owner;
                            }
                            else
                            {
                                oc.AuditScore = aoc;
                            }
                        }
                        else
                        {
                            aoc = oc.AuditScore;
                        }
                    }

                    sum = 0m;
                    for (Emonth i = oss.ERound.MonthStart; i <= oss.ERound.MonthStop; i++)
                    {
                        DetailExpect expect = ObjectSpace.FindObject<DetailExpect>(CriteriaOperator.Parse("Office=? AND PoE=? AND SPoE is null AND Year=? AND ExpectMonth=?", office, poe, oss.ERound.Year, i));
                        if (expect == null)
                        {
                            expect = ObjectSpace.CreateObject<DetailExpect>();
                            expect.Office = office;
                            expect.PoE = poe;
                            expect.Year = oss.ERound.Year;
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

                            expect.AuditScores.Add(aoc);
                            expect.Save();

                            aoc.DetailExpects.Add(expect);
                            sum += expect.ExpectMonthValue;
                        }
                        else
                        {
                            bool expectHavedAudit = false;
                            foreach (AuditScore au in expect.AuditScores)
                            {
                                if (au == aoc) expectHavedAudit = true;
                            }
                            if (!expectHavedAudit)
                            {
                                expect.AuditScores.Add(aoc);
                            }
                            expect.Save();

                            bool aocHavedExpect = false;
                            foreach (DetailExpect dt in aoc.DetailExpects)
                            {
                                if (dt == expect) aocHavedExpect = true;
                            }
                            if (!aocHavedExpect)
                            {
                                aoc.DetailExpects.Add(expect);
                            }
                            sum += expect.ExpectMonthValue;
                        }
                    }
                    aoc.Expect = sum;//End Crete DetailExpect.
                                     //ObjectSpace.CommitChanges();

                    sum = 0m;
                    for (Emonth i = oss.ERound.MonthStart; i <= oss.ERound.MonthStop; i++)
                    {
                        OwnResult or = ObjectSpace.FindObject<OwnResult>(CriteriaOperator.Parse("Office=? AND PoE=? AND SPoE is null AND Year=? AND ResultMonth=?", office, poe, oss.ERound.Year, i));
                        if (or == null)
                        {
                            or = ObjectSpace.CreateObject<OwnResult>();
                            or.Office = office;
                            or.PoE = poe;
                            or.Year = oss.ERound.Year;
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
                            or.OwnScores.Add(oc);
                            or.Save();
                            oc.OwnResults.Add(or);
                            sum += or.ResultMonthValue;
                        }
                        else
                        {
                            bool orHavedOc = false;
                            foreach (OwnScore o in or.OwnScores)
                            {
                                if (o == oc) orHavedOc = true;
                            }
                            if (!orHavedOc)
                            {
                                or.OwnScores.Add(oc);
                            }
                            or.Save();

                            bool ocHavedOr = false;
                            foreach (OwnResult r in oc.OwnResults)
                            {

                            }
                            if (!ocHavedOr)
                            {
                                oc.OwnResults.Add(or);
                            }

                            sum += or.ResultMonthValue;
                        }
                    }
                    oc.Result = sum;//End Crete OwnerResult.
                                    //ObjectSpace.CommitChanges();

                    sum = 0m;
                    for (Emonth i = oss.ERound.MonthStart; i <= oss.ERound.MonthStop; i++)
                    {
                        AuditResult aResult = ObjectSpace.FindObject<AuditResult>(CriteriaOperator.Parse("Office=? AND PoE=? AND SPoE is null AND Year=? AND ResultMonth=?", office, poe, oss.ERound.Year, i));
                        if (aResult == null)
                        {

                            aResult = ObjectSpace.CreateObject<AuditResult>();
                            aResult.Office = office;
                            aResult.PoE = poe;
                            aResult.Year = oss.ERound.Year;
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
                            aResult.AuditScores.Add(aoc);
                            aResult.Save();

                            aoc.AuditResults.Add(aResult);
                            sum += aResult.ResultMonthValue;
                        }
                        else
                        {
                            bool arHevedAoc = false;
                            foreach (AuditScore a in aResult.AuditScores)
                            {
                                if (a == aoc) arHevedAoc = true;
                            }
                            if (!arHevedAoc)
                            {
                                aResult.AuditScores.Add(aoc);
                            }
                            aResult.Save();

                            bool aocHevedAr = false;
                            foreach (AuditResult ar in aoc.AuditResults)
                            {
                                if (ar == aResult) aocHevedAr = true;
                            }
                            if (!aocHevedAr)
                            {
                                aoc.AuditResults.Add(aResult);
                            }
                            sum += aResult.ResultMonthValue;
                        }
                    }
                    aoc.Result = sum;//End Crete AuditResult.
                    aoc.Save();

                    //oc.AuditScore = aoc;
                    oc.Save();

                    bool scHavedOc = false;
                    foreach (OwnScore o in sc.OwnScores)
                    {
                        if (o == oc) scHavedOc = true;
                    }
                    if (!scHavedOc)
                    {
                        sc.OwnScores.Add(oc);
                    }

                    if (oc.PointOfEvaluation.SPoEs != null)
                    {
                        foreach (SubPointOfEvaluation soe in poe.SPoEs)
                        {
                            OwnScore soc = ObjectSpace.FindObject<OwnScore>(CriteriaOperator.Parse("MainScore = ? AND PointOfEvaluation=? AND SubPointOfEvaluation=?", sc, poe, soe));
                            AuditScore asoc;
                            if (soc == null)
                            {
                                soc = ObjectSpace.CreateObject<OwnScore>(); // SubPoE
                                soc.MainScore = sc;
                                soc.PointOfEvaluation = poe;
                                soc.SubPointOfEvaluation = soe;

                                asoc = ObjectSpace.CreateObject<AuditScore>(); //Audit Score of SubPoE
                                asoc.OwnScore = soc;
                                asoc.Owner = owner;
                            }
                            else
                            {
                                if (soc.AuditScore == null)
                                {
                                    asoc = ObjectSpace.FindObject<AuditScore>(CriteriaOperator.Parse("OwnScore = ?", soc));
                                    if (asoc == null)
                                    {
                                        asoc = ObjectSpace.CreateObject<AuditScore>(); //Audit Score of SubPoE
                                        asoc.OwnScore = soc;
                                        asoc.Owner = owner;
                                    }
                                    else
                                    {
                                        soc.AuditScore = asoc;
                                    }
                                }
                                else
                                {
                                    asoc = soc.AuditScore;
                                }
                            }

                            sum = 0m;
                            for (Emonth i = oss.ERound.MonthStart; i <= oss.ERound.MonthStop; i++)
                            {
                                DetailExpect sexpect = ObjectSpace.FindObject<DetailExpect>(CriteriaOperator.Parse("Office=? AND PoE=? AND SPoE=? AND Year=? AND ExpectMonth=?", office, poe, soe, oss.ERound.Year, i));
                                if (sexpect == null) //สร้าง ข้อมูลประมาณการหากไม่มีเดือนในรอบ
                                {
                                    sexpect = ObjectSpace.CreateObject<DetailExpect>();
                                    sexpect.Office = office;
                                    sexpect.PoE = poe;
                                    sexpect.SPoE = soe;
                                    sexpect.Year = oss.ERound.Year;
                                    sexpect.ExpectMonth = i;
                                    sexpect.AuditScores.Add(asoc);
                                    sexpect.Save();

                                    asoc.DetailExpects.Add(sexpect);

                                    sum += sexpect.ExpectMonthValue;
                                }
                                else
                                {
                                    bool sexpectHavedAudit = false;
                                    foreach (AuditScore au in sexpect.AuditScores)
                                    {
                                        if (au == asoc) sexpectHavedAudit = true;
                                    }
                                    if (!sexpectHavedAudit)
                                    {
                                        sexpect.AuditScores.Add(asoc);
                                    }
                                    sexpect.Save();

                                    bool asocHavedExpect = false;
                                    foreach (DetailExpect de in asoc.DetailExpects)
                                    {
                                        if (de == sexpect) asocHavedExpect = true;
                                    }
                                    if (!asocHavedExpect)
                                    {
                                        asoc.DetailExpects.Add(sexpect);
                                    }

                                    sum += sexpect.ExpectMonthValue;
                                }
                            }
                            asoc.Expect = sum; //End Crete Sub DetailExpect.
                            //ObjectSpace.CommitChanges();

                            sum = 0m;
                            for (Emonth i = oss.ERound.MonthStart; i <= oss.ERound.MonthStop; i++)
                            {
                                OwnResult sor = ObjectSpace.FindObject<OwnResult>(CriteriaOperator.Parse("Office=? AND PoE=? AND SPoE=? AND Year=? AND ResultMonth=?", office, poe, soe, oss.ERound.Year, i));
                                if (sor == null)
                                {
                                    sor = ObjectSpace.CreateObject<OwnResult>();
                                    sor.Office = office;
                                    sor.PoE = poe;
                                    sor.SPoE = soe;
                                    sor.Year = oss.ERound.Year;
                                    sor.ResultMonth = i;

                                    sor.OwnScores.Add(soc);
                                    sor.Save();

                                    bool sorHavedsoc = false;
                                    foreach (OwnResult or in soc.OwnResults)
                                    {
                                        if (or == sor) sorHavedsoc = true;
                                    }
                                    if (!sorHavedsoc)
                                    {
                                        soc.OwnResults.Add(sor);
                                    }

                                    sum += sor.ResultMonthValue;
                                }
                                else
                                {
                                    if (sor.OwnScores == null)
                                    {
                                        sor.OwnScores.Add(soc);
                                        sor.Save();
                                    }

                                    bool sorHavedsoc = false;
                                    foreach (OwnResult or in soc.OwnResults)
                                    {
                                        if (or == sor) sorHavedsoc = true;
                                    }
                                    if (!sorHavedsoc)
                                    {
                                        soc.OwnResults.Add(sor);
                                    }

                                    sum += sor.ResultMonthValue;
                                }
                            }
                            soc.Result = sum;//End Crete Sub OwnerResult.
                            //ObjectSpace.CommitChanges();

                            sum = 0m;
                            for (Emonth i = oss.ERound.MonthStart; i <= oss.ERound.MonthStop; i++)
                            {
                                AuditResult asResult = ObjectSpace.FindObject<AuditResult>(CriteriaOperator.Parse("Office=? AND PoE=? AND SPoE=? AND Year=? AND ResultMonth=?", office, poe, soe, oss.ERound.Year, i));
                                if (asResult == null)
                                {
                                    asResult = ObjectSpace.CreateObject<AuditResult>();
                                    asResult.Office = office;
                                    asResult.PoE = poe;
                                    asResult.SPoE = soe;
                                    asResult.Year = oss.ERound.Year;
                                    asResult.ResultMonth = i;

                                    asResult.AuditScores.Add(asoc);
                                    asResult.Save();

                                    bool arHavedaoc = false;
                                    foreach (AuditResult ar in asoc.AuditResults)
                                    {
                                        if (ar == asResult) arHavedaoc = true;
                                    }
                                    if (!arHavedaoc)
                                    {
                                        asoc.AuditResults.Add(asResult);
                                    }
                                    sum += asResult.ResultMonthValue;
                                }
                                else
                                {
                                    if (asoc.AuditResults == null)
                                    {
                                        asResult.AuditScores.Add(asoc);
                                        asResult.Save();
                                    }

                                    bool arHavedaoc = false;
                                    foreach (AuditResult ar in asoc.AuditResults)
                                    {
                                        if (ar == asResult) arHavedaoc = true;
                                    }
                                    if (!arHavedaoc)
                                    {
                                        asoc.AuditResults.Add(asResult);
                                    }
                                    sum += asResult.ResultMonthValue;
                                }
                            }

                            asoc.Result = sum;//End Crete Sub AuditResult.
                            asoc.Save();

                            //Save Sub Owner Score

                            soc.Save();

                            //Add Sub Owner Score to Main Score
                            bool socHavedsc = false;
                            foreach (OwnScore o in sc.OwnScores)
                            {
                                if (o == soc) socHavedsc = true;
                            }
                            if (!socHavedsc)
                            {
                                sc.OwnScores.Add(soc);
                            }
                        }
                    }//End SupPointOfEvaluation

                }//End PointOfEvaluation Loop

                sc.Save(); //Save Score
                ObjectSpace.CommitChanges();
            }
        }

        private void InitialDataEntryController_Activated(object sender, EventArgs e)
        {
            //เริ่มสร้าง
            OpenSendScore oss = ObjectSpace.FindObject<OpenSendScore>(CriteriaOperator.Parse("Open = ?", true));
            if (oss != null)
            {
                if (oss.ERound != null)
                {
                    Employee owner = ObjectSpace.GetObjectByKey<Employee>(SecuritySystem.CurrentUserId);
                    Score oscore = ObjectSpace.FindObject<Score>(CriteriaOperator.Parse("ERound = ? AND Office=?", oss.ERound, 995));
                    if (oscore == null)
                    {
                        CreateAll(oss, owner);
                    }
                    else
                    {
                        MessageOptions options = new MessageOptions();
                        options.Duration = 2000;
                        options.Message = string.Format("{0} มีการสร้างตารางข้อมูลแล้ว!!!", oss.ERound.Title);
                        options.Type = InformationType.Error;
                        options.Web.Position = InformationPosition.Top;
                        options.Win.Caption = "Error";
                        options.Win.Type = WinMessageType.Flyout;
                        Application.ShowViewStrategy.ShowMessage(options);
                    }
                }
            }
            else
            {
                MessageOptions options = new MessageOptions();
                options.Duration = 2000;
                options.Message = string.Format("ยังไม่เปิดให้บันทึกข้อมูล หรือ ไม่มีรอบประเมินที่เปิด!!!");
                options.Type = InformationType.Error;
                options.Web.Position = InformationPosition.Top;
                options.Win.Caption = "Error";
                options.Win.Type = WinMessageType.Flyout;
                Application.ShowViewStrategy.ShowMessage(options);
            }
        }
    }
}
