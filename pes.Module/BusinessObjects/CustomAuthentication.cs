using System;
using System.Collections.Generic;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;
using pes.Module.BusinessObjects;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using pes.Module.th.go.rd.wservice;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Base;

namespace CustomLogonParametersExample.Module
{
    public class CustomAuthentication : AuthenticationBase, IAuthenticationStandard
    {
        private CustomLogonParameters customLogonParameters;
        public CustomAuthentication()
        {
            customLogonParameters = new CustomLogonParameters();
        }
        public override void Logoff()
        {
            base.Logoff();
            customLogonParameters = new CustomLogonParameters();
        }
        public override void ClearSecuredLogonParameters()
        {
            customLogonParameters.Password = "";
            base.ClearSecuredLogonParameters();
        }
        public override object Authenticate(IObjectSpace objectSpace)
        {
            Employee employee = objectSpace.FindObject<Employee>(
            new BinaryOperator("UserName", customLogonParameters.UserName.ToLower()));

            if (customLogonParameters.UserName.ToLower() == "admin")
            {
                if (!employee.ComparePassword(customLogonParameters.Password))
                    throw new AuthenticationException(employee.UserName, "รหัสผ่านไม่ถูกต้อง");
            }
            else
            {
                AuthenUserEoffice1 ws = new AuthenUserEoffice1();
                DataUser user = new DataUser();
                //DataUser dataUser = new DataUser();
                //dataUser.Authen = false;
                user = ws.AuthenUser("InternetUser", "InternetPass", customLogonParameters.UserName.ToUpper(), customLogonParameters.Password.ToString());// ?? dataUser;

                if (!user.Authen || user is null)
                    throw new UserFriendlyException("รหัสผู้ใช้หรือรหัสผ่านไม่ถูกต้อง");
                else
                {
                    if (employee == null)
                    {
                        employee = objectSpace.CreateObject<Employee>();
                        employee.UserName = customLogonParameters.UserName.ToLower();
                        PermissionPolicyRole defaultRole = objectSpace.FindObject<PermissionPolicyRole>(new BinaryOperator("Name", "Default"));
                        employee.Roles.Add(defaultRole);

                        employee.Title = user.TITLE;
                        employee.FirstName = user.FNAME;
                        employee.LastName = user.LNAME;
                        employee.Email = user.EMAIL;
                        employee.Position = user.POSITION_M;
                        employee.PoditionLevel = user.CLASS_NEW;

                        Office office = objectSpace.FindObject<Office>(new BinaryOperator("OfficeId", user.OFFICEID));
                        employee.Office = office;

                        //employee.SetPassword("");

                        objectSpace.CommitChanges();
                    }
                }
            }
            return employee;
        }

        public override void SetLogonParameters(object logonParameters)
        {
            this.customLogonParameters = (CustomLogonParameters)logonParameters;
        }

        public override IList<Type> GetBusinessClasses()
        {
            return new Type[] { typeof(CustomLogonParameters) };
        }
        public override bool AskLogonParametersViaUI
        {
            get { return true; }
        }
        public override object LogonParameters
        {
            get { return customLogonParameters; }
        }
        public override bool IsLogoffEnabled
        {
            get { return true; }
        }
    }
}