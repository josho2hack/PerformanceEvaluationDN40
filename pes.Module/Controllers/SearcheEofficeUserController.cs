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
using pes.Module.BusinessObjects;
using pes.Module.th.go.rd.wservice1;

namespace pes.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class SearcheEofficeUserController : ViewController
    {
        public SearcheEofficeUserController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
            if (View is DetailView)
            {
                DetailView dv = (DetailView)View;
                dv.ViewEditModeChanged += new EventHandler<EventArgs>(ViewController1_ViewEditModeChanged);
            }
            UpdateActionState();
        }
        private void UpdateActionState()
        {
            searchOfficeUserAction.Enabled["MyKey"] = (View is DetailView && ((DetailView)View).ViewEditMode == DevExpress.ExpressApp.Editors.ViewEditMode.Edit);
        }
        void ViewController1_ViewEditModeChanged(object sender, EventArgs e)
        {
            UpdateActionState();
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

        private void searchOfficeUserAction_Execute(object sender, ParametrizedActionExecuteEventArgs e)
        {
            string paramValue = e.ParameterCurrentValue as string;
            AuthenUserEofficebyID ws = new AuthenUserEofficebyID();
            DataUser user = new DataUser();
            user = ws.AuthenUser("InternetUser", "InternetPass", paramValue);

            if (user is null || !user.Authen)
            {
                throw new UserFriendlyException("ลสก. ไม่ถูกต้อง");
            }

            ((Employee)View.CurrentObject).UserName = user.EMAIL.Substring(0, 1) + user.EMAIL.Split('.')[1].Substring(0, 1) + user.ID;
            ((Employee)View.CurrentObject).Title = user.TITLE;
            ((Employee)View.CurrentObject).FirstName = user.FNAME;
            ((Employee)View.CurrentObject).LastName = user.LNAME;

            //IObjectSpace objectSpace = Application.CreateObjectSpace();
            var obj = ObjectSpace.FindObject<Office>(new BinaryOperator("OfficeId", user.OFFICEID));
            ((Employee)View.CurrentObject).Office = obj;
            ((Employee)View.CurrentObject).Position = user.POSITION_M;
            ((Employee)View.CurrentObject).Email = user.EMAIL;
        }
    }
}
