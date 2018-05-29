using System;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using System.Runtime.Serialization;

namespace pes.Module.BusinessObjects
{
    [DomainComponent, Serializable]
    [System.ComponentModel.DisplayName("เข้าสู่ระบบ")]
    public class CustomLogonParameters : INotifyPropertyChanged, ISerializable
    {
        private string password;
        public CustomLogonParameters() { }
        // ISerializable  
        public CustomLogonParameters(SerializationInfo info, StreamingContext context)
        {
            if (info.MemberCount > 0)
            {
                UserName = info.GetString("UserName");
                Password = info.GetString("Password");
            }
        }
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        [System.Security.SecurityCritical]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("UserName", UserName);
            info.AddValue("Password", Password);
        }
        //[Browsable(false)]
        [XafDisplayName("รหัสผู้ใช้"),ToolTip("รหัสผู้ใช้เช่นเดียวกับ e-Office")]
        public String UserName { get; set; }
        [PasswordPropertyText(true)]
        [XafDisplayName("รหัสผ่าน"), ToolTip("รหัสผ่านเช่นเดียวกับ e-Office")]
        public string Password
        {
            get { return password; }
            set
            {
                if (password == value) return;
                password = value;
            }
        }
    }
}