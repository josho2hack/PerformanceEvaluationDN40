namespace pes.Module.Controllers
{
    partial class SearcheEofficeUserController
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.searchOfficeUserAction = new DevExpress.ExpressApp.Actions.ParametrizedAction(this.components);
            // 
            // searchOfficeUserAction
            // 
            this.searchOfficeUserAction.Caption = "ค้นหาจาก ลสก.";
            this.searchOfficeUserAction.ConfirmationMessage = null;
            this.searchOfficeUserAction.Id = "searchOfficeUserAction";
            this.searchOfficeUserAction.NullValuePrompt = null;
            this.searchOfficeUserAction.ShortCaption = null;
            this.searchOfficeUserAction.TargetObjectType = typeof(pes.Module.BusinessObjects.Employee);
            this.searchOfficeUserAction.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.searchOfficeUserAction.ToolTip = "ค้าหาด้วยเลข ลสก. !";
            this.searchOfficeUserAction.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.searchOfficeUserAction.Execute += new DevExpress.ExpressApp.Actions.ParametrizedActionExecuteEventHandler(this.searchOfficeUserAction_Execute);
            // 
            // SearcheEofficeUserController
            // 
            this.Actions.Add(this.searchOfficeUserAction);
            this.TargetObjectType = typeof(pes.Module.BusinessObjects.Employee);
            this.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.ParametrizedAction searchOfficeUserAction;
    }
}
