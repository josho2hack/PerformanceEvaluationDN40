using System;
using ImportData.Controllers;
using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Actions;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Templates;
using pes.Module.BusinessObjects;

namespace pes.Module {
    /// <summary>
    /// Dennis: This ViewController adds the ImportData action and allows you to import data in ListView.
    /// </summary>
    public class ImportDataListViewController : ViewController {
        public const int MaxImportedRecordsCount = 30;
        public const string ActiveKeyImportAction = "ActiveKeyImportAction";
        public const string ActiveKeyImportActionItemRootListViewForPerson = "Item is active only in the root ListView for OwnScore";
        //public const string ActiveKeyImportActionItemNestedListViewForPhoneNumber = "Item is active only in nested ListView for PhoneNumber";
        private SingleChoiceAction importDataActionCore = null;

        public ImportDataListViewController() {
            TargetViewType = ViewType.ListView;
            importDataActionCore = CreateImportAction();
        }
        private SingleChoiceAction CreateImportAction() {
            SingleChoiceAction importDataAction = new SingleChoiceAction(this, "ImportData", PredefinedCategory.RecordEdit);
            importDataAction.Execute += ImportDataAction_Execute;
            ChoiceActionItem item1 = new ChoiceActionItem();
            //ChoiceActionItem item2 = new ChoiceActionItem();
            importDataAction.Caption = "Import Data";
            importDataAction.ImageName = "Attention";
            item1.Caption = "In root ListView";
            item1.Data = "OwnScore_ListView";
            //item2.Caption = "In nested ListView";
            //item2.Data = "Party_PhoneNumbers_ListView";
            importDataAction.Items.Add(item1);
            //importDataAction.Items.Add(item2);
            importDataAction.PaintStyle = ActionItemPaintStyle.CaptionAndImage;
            importDataAction.ItemType = SingleChoiceActionItemType.ItemIsOperation;
            return importDataAction;
        }
        private void ImportDataAction_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
        {
            ImportData(e);
        }
        protected virtual void ImportData(SingleChoiceActionExecuteEventArgs e) {
            ChoiceActionItem activeItem = e.SelectedChoiceActionItem;
            ListView lv = (ListView)View;
            ImportDataManager importManager = new ImportDataManager(Application);
            switch (activeItem.Data.ToString()) {
                case "OwnScore_ListView":
                    importManager.ImportData<OwnScore>(MaxImportedRecordsCount, ImportDataLogic.CreateCoolOwnScoreImportDataFromExcelDelegate(), null, lv, true);
                    //Dennis: This line won't be executed unless you handle the exception thrown from the previus ImportData call.
                    //ImportDataManager.ImportData<Person>(MaxImportedRecordsCount, ImportDataLogic.CreateDummyPersonImportDataDelegate(), ImportDataLogic.CreateDummyPersonValidateDataDelegate(), lv, true);
                    break;
                //case "Party_PhoneNumbers_ListView":
                //    PropertyCollectionSource pcs = lv.CollectionSource as PropertyCollectionSource;
                //    if (pcs != null) {
                //        importManager.ImportData<PhoneNumber>(MaxImportedRecordsCount, ImportDataLogic.CreateDummyPhoneNumberImportDataDelegate(), ImportDataLogic.CreateDummyPhoneNumberValidateDataDelegate(), lv, true);
                //    }
                //    break;
            }
        }
        protected override void UpdateActionActivity(ActionBase action) {
            base.UpdateActionActivity(action);
            bool rootListViewForPersonCondition = View.IsRoot && View is ListView && View.ObjectTypeInfo.Type == typeof(OwnScore);
            //bool nestedListViewForPhoneNumberCondition = !View.IsRoot && View is ListView && View.ObjectTypeInfo.Type == typeof(PhoneNumber);
            ImportDataAction.Active[ActiveKeyImportAction] = rootListViewForPersonCondition;// || nestedListViewForPhoneNumberCondition;
            ImportDataAction.Items[0].Active[ActiveKeyImportActionItemRootListViewForPerson] = rootListViewForPersonCondition;
            //ImportDataAction.Items[1].Active[ActiveKeyImportActionItemNestedListViewForPhoneNumber] = nestedListViewForPhoneNumberCondition;
        }
        public SingleChoiceAction ImportDataAction {
            get {
                return importDataActionCore;
            }
        }
    }
}