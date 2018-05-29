using System;
using System.Collections.Generic;
using System.Linq;
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
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Web.SystemModule;
using DevExpress.Web;
using DevExpress.XtraPrinting;

namespace pes.Module.Web.Controllers
{
    public partial class CustomExport : ViewController
    {
        public CustomExport()
        {
            InitializeComponent();
            TargetViewType = ViewType.ListView;
        }

        private WebExportController webExportController;
        protected override void OnActivated()
        {
            base.OnActivated();
            webExportController = Frame.GetController<WebExportController>();
            webExportController.CustomExport += webExportController_CustomExport;
        }

        void webExportController_CustomExport(object sender, CustomExportEventArgs e)
        {
            if (e.ExportTarget == ExportTarget.Pdf)
            {
                ASPxGridViewExporter exporter = e.Printable as ASPxGridViewExporter;
                exporter.Styles.Default.Font.Name = "Angsana"; //TH SarabunPSK
            }
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
        }
        protected override void OnDeactivated()
        {
            webExportController.CustomExport -= webExportController_CustomExport;
            base.OnDeactivated();
        }
    }
}
