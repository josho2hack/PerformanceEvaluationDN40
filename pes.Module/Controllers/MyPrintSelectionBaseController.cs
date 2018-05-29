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
using DevExpress.ExpressApp.ReportsV2;

namespace pes.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class MyPrintSelectionBaseController : DevExpress.ExpressApp.ReportsV2.PrintSelectionBaseController
    {
        public MyPrintSelectionBaseController()
        {
            InitializeComponent();
            ShowInReportAction.Execute += ShowInReportAction_Execute;
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            ShowInReportAction.SelectionDependencyType = SelectionDependencyType.Independent;
            ShowInReportAction.Enabled.RemoveItem(ActionBase.RequireMultipleObjectsContext);
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

        void ShowInReportAction_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
        {
            if (e.SelectedObjects.Count == 0)
            {
                string reportContainerHandle = ((ReportDataInfo)e.SelectedChoiceActionItem.Data).ReportContainerHandle;
                Frame.GetController<ReportServiceController>().ShowPreview(reportContainerHandle);
            }
        }
    }
}
