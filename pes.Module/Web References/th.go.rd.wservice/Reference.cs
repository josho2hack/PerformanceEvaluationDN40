﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.42000.
// 
#pragma warning disable 1591

namespace pes.Module.th.go.rd.wservice {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="AuthenUserEoffice1Soap", Namespace="http://wservice.rd.go.th/ServiceEoffice/AuthenUserEoffice1/")]
    public partial class AuthenUserEoffice1 : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback AuthenUserOperationCompleted;
        
        private System.Threading.SendOrPostCallback HeadGrpofExpertOperationCompleted;
        
        private System.Threading.SendOrPostCallback AuthenbyNameOperationCompleted;
        
        private System.Threading.SendOrPostCallback AuthenbyGrpNameOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public AuthenUserEoffice1() {
            this.Url = global::pes.Module.Properties.Settings.Default.pes_Module_th_go_rd_wservice_AuthenUserEoffice1;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event AuthenUserCompletedEventHandler AuthenUserCompleted;
        
        /// <remarks/>
        public event HeadGrpofExpertCompletedEventHandler HeadGrpofExpertCompleted;
        
        /// <remarks/>
        public event AuthenbyNameCompletedEventHandler AuthenbyNameCompleted;
        
        /// <remarks/>
        public event AuthenbyGrpNameCompletedEventHandler AuthenbyGrpNameCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://wservice.rd.go.th/ServiceEoffice/AuthenUserEoffice1/AuthenUser", RequestNamespace="http://wservice.rd.go.th/ServiceEoffice/AuthenUserEoffice1/", ResponseNamespace="http://wservice.rd.go.th/ServiceEoffice/AuthenUserEoffice1/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("DataUser", IsNullable=true)]
        public DataUser AuthenUser(string CheckUser, string CheckPass, string UID, string Password) {
            object[] results = this.Invoke("AuthenUser", new object[] {
                        CheckUser,
                        CheckPass,
                        UID,
                        Password});
            return ((DataUser)(results[0]));
        }
        
        /// <remarks/>
        public void AuthenUserAsync(string CheckUser, string CheckPass, string UID, string Password) {
            this.AuthenUserAsync(CheckUser, CheckPass, UID, Password, null);
        }
        
        /// <remarks/>
        public void AuthenUserAsync(string CheckUser, string CheckPass, string UID, string Password, object userState) {
            if ((this.AuthenUserOperationCompleted == null)) {
                this.AuthenUserOperationCompleted = new System.Threading.SendOrPostCallback(this.OnAuthenUserOperationCompleted);
            }
            this.InvokeAsync("AuthenUser", new object[] {
                        CheckUser,
                        CheckPass,
                        UID,
                        Password}, this.AuthenUserOperationCompleted, userState);
        }
        
        private void OnAuthenUserOperationCompleted(object arg) {
            if ((this.AuthenUserCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.AuthenUserCompleted(this, new AuthenUserCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://wservice.rd.go.th/ServiceEoffice/AuthenUserEoffice1/HeadGrpofExpert", RequestNamespace="http://wservice.rd.go.th/ServiceEoffice/AuthenUserEoffice1/", ResponseNamespace="http://wservice.rd.go.th/ServiceEoffice/AuthenUserEoffice1/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public DataUser[] HeadGrpofExpert(string CheckUser, string CheckPass, string FName, string LName) {
            object[] results = this.Invoke("HeadGrpofExpert", new object[] {
                        CheckUser,
                        CheckPass,
                        FName,
                        LName});
            return ((DataUser[])(results[0]));
        }
        
        /// <remarks/>
        public void HeadGrpofExpertAsync(string CheckUser, string CheckPass, string FName, string LName) {
            this.HeadGrpofExpertAsync(CheckUser, CheckPass, FName, LName, null);
        }
        
        /// <remarks/>
        public void HeadGrpofExpertAsync(string CheckUser, string CheckPass, string FName, string LName, object userState) {
            if ((this.HeadGrpofExpertOperationCompleted == null)) {
                this.HeadGrpofExpertOperationCompleted = new System.Threading.SendOrPostCallback(this.OnHeadGrpofExpertOperationCompleted);
            }
            this.InvokeAsync("HeadGrpofExpert", new object[] {
                        CheckUser,
                        CheckPass,
                        FName,
                        LName}, this.HeadGrpofExpertOperationCompleted, userState);
        }
        
        private void OnHeadGrpofExpertOperationCompleted(object arg) {
            if ((this.HeadGrpofExpertCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.HeadGrpofExpertCompleted(this, new HeadGrpofExpertCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://wservice.rd.go.th/ServiceEoffice/AuthenUserEoffice1/AuthenbyName", RequestNamespace="http://wservice.rd.go.th/ServiceEoffice/AuthenUserEoffice1/", ResponseNamespace="http://wservice.rd.go.th/ServiceEoffice/AuthenUserEoffice1/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public DataUser[] AuthenbyName(string CheckUser, string CheckPass, string FName, string LName) {
            object[] results = this.Invoke("AuthenbyName", new object[] {
                        CheckUser,
                        CheckPass,
                        FName,
                        LName});
            return ((DataUser[])(results[0]));
        }
        
        /// <remarks/>
        public void AuthenbyNameAsync(string CheckUser, string CheckPass, string FName, string LName) {
            this.AuthenbyNameAsync(CheckUser, CheckPass, FName, LName, null);
        }
        
        /// <remarks/>
        public void AuthenbyNameAsync(string CheckUser, string CheckPass, string FName, string LName, object userState) {
            if ((this.AuthenbyNameOperationCompleted == null)) {
                this.AuthenbyNameOperationCompleted = new System.Threading.SendOrPostCallback(this.OnAuthenbyNameOperationCompleted);
            }
            this.InvokeAsync("AuthenbyName", new object[] {
                        CheckUser,
                        CheckPass,
                        FName,
                        LName}, this.AuthenbyNameOperationCompleted, userState);
        }
        
        private void OnAuthenbyNameOperationCompleted(object arg) {
            if ((this.AuthenbyNameCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.AuthenbyNameCompleted(this, new AuthenbyNameCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://wservice.rd.go.th/ServiceEoffice/AuthenUserEoffice1/AuthenbyGrpName", RequestNamespace="http://wservice.rd.go.th/ServiceEoffice/AuthenUserEoffice1/", ResponseNamespace="http://wservice.rd.go.th/ServiceEoffice/AuthenUserEoffice1/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public DataUser[] AuthenbyGrpName(string CheckUser, string CheckPass, string GrpName) {
            object[] results = this.Invoke("AuthenbyGrpName", new object[] {
                        CheckUser,
                        CheckPass,
                        GrpName});
            return ((DataUser[])(results[0]));
        }
        
        /// <remarks/>
        public void AuthenbyGrpNameAsync(string CheckUser, string CheckPass, string GrpName) {
            this.AuthenbyGrpNameAsync(CheckUser, CheckPass, GrpName, null);
        }
        
        /// <remarks/>
        public void AuthenbyGrpNameAsync(string CheckUser, string CheckPass, string GrpName, object userState) {
            if ((this.AuthenbyGrpNameOperationCompleted == null)) {
                this.AuthenbyGrpNameOperationCompleted = new System.Threading.SendOrPostCallback(this.OnAuthenbyGrpNameOperationCompleted);
            }
            this.InvokeAsync("AuthenbyGrpName", new object[] {
                        CheckUser,
                        CheckPass,
                        GrpName}, this.AuthenbyGrpNameOperationCompleted, userState);
        }
        
        private void OnAuthenbyGrpNameOperationCompleted(object arg) {
            if ((this.AuthenbyGrpNameCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.AuthenbyGrpNameCompleted(this, new AuthenbyGrpNameCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1586.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://wservice.rd.go.th/ServiceEoffice/AuthenUserEoffice1/")]
    public partial class DataUser {
        
        private bool authenField;
        
        private string idField;
        
        private string tITLEField;
        
        private string fNAMEField;
        
        private string lNAMEField;
        
        private string uIDField;
        
        private string pASSWORDField;
        
        private string eMAILField;
        
        private string pOSNAMEField;
        
        private string eMPSTATUSField;
        
        private string bIRTHField;
        
        private string cLASS_dataField;
        
        private string sKILLIDField;
        
        private string eMPTYPEField;
        
        private string oFFICEIDField;
        
        private string oFFICENAMEField;
        
        private string pINField;
        
        private string pOSITION_MField;
        
        private string cLASS_NEWField;
        
        private string lEVELField;
        
        private string pOSACTField;
        
        private string gROUPNAMEField;
        
        /// <remarks/>
        public bool Authen {
            get {
                return this.authenField;
            }
            set {
                this.authenField = value;
            }
        }
        
        /// <remarks/>
        public string ID {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }
        
        /// <remarks/>
        public string TITLE {
            get {
                return this.tITLEField;
            }
            set {
                this.tITLEField = value;
            }
        }
        
        /// <remarks/>
        public string FNAME {
            get {
                return this.fNAMEField;
            }
            set {
                this.fNAMEField = value;
            }
        }
        
        /// <remarks/>
        public string LNAME {
            get {
                return this.lNAMEField;
            }
            set {
                this.lNAMEField = value;
            }
        }
        
        /// <remarks/>
        public string UID {
            get {
                return this.uIDField;
            }
            set {
                this.uIDField = value;
            }
        }
        
        /// <remarks/>
        public string PASSWORD {
            get {
                return this.pASSWORDField;
            }
            set {
                this.pASSWORDField = value;
            }
        }
        
        /// <remarks/>
        public string EMAIL {
            get {
                return this.eMAILField;
            }
            set {
                this.eMAILField = value;
            }
        }
        
        /// <remarks/>
        public string POSNAME {
            get {
                return this.pOSNAMEField;
            }
            set {
                this.pOSNAMEField = value;
            }
        }
        
        /// <remarks/>
        public string EMPSTATUS {
            get {
                return this.eMPSTATUSField;
            }
            set {
                this.eMPSTATUSField = value;
            }
        }
        
        /// <remarks/>
        public string BIRTH {
            get {
                return this.bIRTHField;
            }
            set {
                this.bIRTHField = value;
            }
        }
        
        /// <remarks/>
        public string CLASS_data {
            get {
                return this.cLASS_dataField;
            }
            set {
                this.cLASS_dataField = value;
            }
        }
        
        /// <remarks/>
        public string SKILLID {
            get {
                return this.sKILLIDField;
            }
            set {
                this.sKILLIDField = value;
            }
        }
        
        /// <remarks/>
        public string EMPTYPE {
            get {
                return this.eMPTYPEField;
            }
            set {
                this.eMPTYPEField = value;
            }
        }
        
        /// <remarks/>
        public string OFFICEID {
            get {
                return this.oFFICEIDField;
            }
            set {
                this.oFFICEIDField = value;
            }
        }
        
        /// <remarks/>
        public string OFFICENAME {
            get {
                return this.oFFICENAMEField;
            }
            set {
                this.oFFICENAMEField = value;
            }
        }
        
        /// <remarks/>
        public string PIN {
            get {
                return this.pINField;
            }
            set {
                this.pINField = value;
            }
        }
        
        /// <remarks/>
        public string POSITION_M {
            get {
                return this.pOSITION_MField;
            }
            set {
                this.pOSITION_MField = value;
            }
        }
        
        /// <remarks/>
        public string CLASS_NEW {
            get {
                return this.cLASS_NEWField;
            }
            set {
                this.cLASS_NEWField = value;
            }
        }
        
        /// <remarks/>
        public string LEVEL {
            get {
                return this.lEVELField;
            }
            set {
                this.lEVELField = value;
            }
        }
        
        /// <remarks/>
        public string POSACT {
            get {
                return this.pOSACTField;
            }
            set {
                this.pOSACTField = value;
            }
        }
        
        /// <remarks/>
        public string GROUPNAME {
            get {
                return this.gROUPNAMEField;
            }
            set {
                this.gROUPNAMEField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    public delegate void AuthenUserCompletedEventHandler(object sender, AuthenUserCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class AuthenUserCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal AuthenUserCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public DataUser Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((DataUser)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    public delegate void HeadGrpofExpertCompletedEventHandler(object sender, HeadGrpofExpertCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class HeadGrpofExpertCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal HeadGrpofExpertCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public DataUser[] Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((DataUser[])(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    public delegate void AuthenbyNameCompletedEventHandler(object sender, AuthenbyNameCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class AuthenbyNameCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal AuthenbyNameCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public DataUser[] Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((DataUser[])(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    public delegate void AuthenbyGrpNameCompletedEventHandler(object sender, AuthenbyGrpNameCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class AuthenbyGrpNameCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal AuthenbyGrpNameCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public DataUser[] Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((DataUser[])(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591