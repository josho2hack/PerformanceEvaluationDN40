namespace pes.Module.Controllers
{
    partial class InitialDataEntryController
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
            this.CreateERoundInitialAction = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // CreateERoundInitialAction
            // 
            this.CreateERoundInitialAction.Caption = "สร้างตารางสำหรับบันทึกข้อมูลในรอบปัจจุบัน";
            this.CreateERoundInitialAction.Category = "Save";
            this.CreateERoundInitialAction.ConfirmationMessage = "สร้างตารางสำหรับบันทึก ใช่หรือไม่ ?";
            this.CreateERoundInitialAction.Id = "CreateERoundInitialAction";
            this.CreateERoundInitialAction.ImageName = "Action_SimpleAction";
            this.CreateERoundInitialAction.TargetObjectsCriteria = "Open = true";
            this.CreateERoundInitialAction.TargetObjectType = typeof(pes.Module.BusinessObjects.OpenSendScore);
            this.CreateERoundInitialAction.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.CreateERoundInitialAction.ToolTip = null;
            this.CreateERoundInitialAction.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.CreateERoundInitialAction.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.CreateERoundInitialAction_Execute);
            // 
            // InitialDataEntryController
            // 
            this.Actions.Add(this.CreateERoundInitialAction);
            this.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction CreateERoundInitialAction;
    }
}
