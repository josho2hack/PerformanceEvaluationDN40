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
using DevExpress.ExpressApp.Web.Editors.ASPx;
using DevExpress.Web;
using DevExpress.ExpressApp.Web.Utils;
using System.Web.UI.WebControls;
using DevExpress.ExpressApp.Model;
using System.Web.UI;

namespace pes.Module.Web.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class OwnScoreCustomListView : ViewController
    {
        public OwnScoreCustomListView()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
        }

        private string GetAdjustSizeScript(ASPxGridView gridView)
        {
            return String.Format(" var height = Math.max(0, document.documentElement.clientHeight) - 170; {0}.SetHeight(height);", gridView.ClientInstanceName);
        }
        private void gridView_Load(object sender, EventArgs e)
        {
            ASPxGridView gridView = (ASPxGridView)sender;
            string AdjustSize = String.Format(" var height = Math.max(0, document.documentElement.clientHeight) - 170; {0}.SetHeight(height);", gridView.ClientInstanceName);
            ClientSideEventsHelper.AssignClientHandlerSafe(gridView, "Init", string.Format("function(s,e){{{0}}}", GetAdjustSizeScript(gridView)), "GridViewInitEventHandler");
        }

        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            //ASPxGridListEditor listEditor = ((ListView)View).Editor as ASPxGridListEditor;
            //if (listEditor != null)
            //{
            //    listEditor.Grid.SettingsPager.Mode = DevExpress.Web.GridViewPagerMode.ShowAllRecords;
            //    listEditor.Grid.Settings.VerticalScrollBarMode = DevExpress.Web.ScrollBarMode.Auto;
            //    listEditor.Grid.Width = new System.Web.UI.WebControls.Unit("100%");
            //    //listEditor.Grid.
            //}

            ASPxGridListEditor gridListEditor = ((ListView)View).Editor as ASPxGridListEditor;
            if (gridListEditor != null)
            {
                ASPxGridView gridView = gridListEditor.Grid;
                gridView.Load += gridView_Load;
                gridView.Width = Unit.Percentage(100);
                gridView.Settings.VerticalScrollBarMode = ScrollBarMode.Auto;
                gridView.ClientInstanceName = "grid_" + View.Id;
                //foreach (WebColumnBase column in gridView.Columns)
                //{
                //    IColumnInfo columnInfo = ((IDataItemTemplateInfoProvider)gridListEditor).GetColumnInfo(column);
                //    if (columnInfo != null)
                //    {
                //        IModelColumn modelColumn = (IModelColumn)columnInfo.Model;
                //        column.Width = Unit.Percentage(modelColumn.Width);
                //    }
                //}
                ASPxGlobalEvents globalEvents = new ASPxGlobalEvents();
                globalEvents.ID = "GE1";
                string adjustSizeScript = GetAdjustSizeScript(gridView);
                globalEvents.ClientSideEvents.ControlsInitialized = string.Format("function(s,e){{ ASPxClientUtils.AttachEventToElement(window, 'resize', function(evt) {{{0}}}); }}", adjustSizeScript);
                ClientSideEventsHelper.AssignClientHandlerSafe(gridView, "EndCallback", string.Format("function(s,e){{{0}}}", adjustSizeScript), "EndCallbackEventHandler");
                ((Control)View.Control).Controls.Add(globalEvents);
            }
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
    }
}
