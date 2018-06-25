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
using DevExpress.Persistent.BaseImpl.PermissionPolicy;

namespace pes.Module.BusinessObjects
{
    [NavigationItem("User")]
    [CurrentUserDisplayImage("Image")]
    [DefaultProperty("FullName")]
    [XafDisplayName("ผู้ใช้งาน")]
    public class Employee : PermissionPolicyUser
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public Employee(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        string title;
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [XafDisplayName("คำนำหน้าชื่อ")]
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                SetPropertyValue("Title", ref title, value);
            }
        }

        string firstName;
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [XafDisplayName("ชื่อ")]
        public string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                SetPropertyValue("FirstName", ref firstName, value);
            }
        }

        string lastName;
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [XafDisplayName("นามสกุล")]
        public string LastName
        {
            get
            {
                return lastName;
            }
            set
            {
                SetPropertyValue("LastName", ref lastName, value);
            }
        }

        string email;
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [XafDisplayName("อีเมล์")]
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                SetPropertyValue("Email", ref email, value);
            }
        }

        string position;
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [XafDisplayName("ตำแหน่ง")]
        public string Position
        {
            get
            {
                return position;
            }
            set
            {
                SetPropertyValue("Position", ref position, value);
            }
        }

        string positionLevel;
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [XafDisplayName("ระดับ")]
        public string PositionLevel
        {
            get
            {
                return positionLevel;
            }
            set
            {
                SetPropertyValue("PositionLevel", ref positionLevel, value);
            }
        }

        private MediaDataObject image;
        [XafDisplayName("รูปภาพ")]
        public MediaDataObject Image
        {
            get { return image; }
            set { SetPropertyValue("Image", ref image, value); }
        }

        Office office;
        [Association("Office-Employees")]
        [XafDisplayName("หน่วยงาน")]
        public Office Office
        {
            get
            {
                return office;
            }
            set
            {
                SetPropertyValue("Office", ref office, value);
            }
        }

        [XafDisplayName("ชื่อเต็ม")]
        public string FullName
        {
            get
            {
                string namePart = string.Format("{0} {1}", FirstName, LastName);
                return Title != null ? string.Format("{0}{1}", Title, namePart) : namePart;
            }
        }

        //[XafDisplayName("ตำแหน่งเต็ม")]
        //public string FullPosition
        //{
        //    get
        //    {
        //        string namePart = string.Format("{0} {1}", Position, PoditionLevel);
        //        return namePart;
        //    }
        //}
    }
}