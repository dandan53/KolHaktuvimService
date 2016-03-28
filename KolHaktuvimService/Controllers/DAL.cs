using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Web;
using System.Web.UI;

namespace KolHaktuvimService.Controllers
{
    public class DAL
    {
        private const string REFUA = "refua";

        private const string ILUI = "ilui";
        
        private List<string> iluiPersonList = new List<string>();

        private List<string> refuaPersonList = new List<string>();

        private static DAL instance = null;

        private DAL()
        {
        }

        public static DAL Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DAL();
                }
                return instance;
            }
        }

        public List<string> GetPersonList(string type)
        {
            List<string> retVal = null;

            if (type.Equals(REFUA))
            {
                retVal = refuaPersonList;
            }
            else if (type.Equals(ILUI))
            {
                retVal = iluiPersonList;
            }

            return retVal;
        }

        public List<string> GetPersonList(string type, int pageNumber, int pageSize)
        {
            List<string> retVal = null;

            if (type.Equals(REFUA))
            {
                retVal = refuaPersonList.GetRange(pageNumber, pageSize);
            }
            else if (type.Equals(ILUI))
            {
                retVal = iluiPersonList.GetRange(pageNumber, pageSize);
            }

            return retVal;
        }

        public bool AddPerson(string person, string type)
        {
            if (IsPersonExist(person, type) == false)
            {
                if (type.Equals(REFUA))
                {
                    refuaPersonList.Add(person);
                    refuaPersonList.Sort();
                }
                else if (type.Equals(ILUI))
                {
                    iluiPersonList.Add(person);
                    iluiPersonList.Sort();
                }
                
                DBDAL.Instance.AddPerson(person, type);
            }

            return true;
        }

        public bool RemovePerson(string person, string type)
        {
            if (IsPersonExist(person, type))
            {
                if (type.Equals(REFUA))
                {
                    refuaPersonList.Remove(person);
                }
                else if (type.Equals(ILUI))
                {
                    iluiPersonList.Remove(person);
                }

                DBDAL.Instance.RemovePerson(person, type);
            }

            return true;
        }

        public bool IsPersonExist(string person, string type)
        {
            bool retVal = false;

            if (type.Equals(REFUA))
            {
                if (refuaPersonList != null)
                {
                    retVal = refuaPersonList.Exists(item => item.Equals(person));                    
                }
            }
            else if (type.Equals(ILUI))
            {
                if (iluiPersonList != null)
                {
                    retVal = iluiPersonList.Exists(item => item.Equals(person));                    
                }
            }
                
            return retVal;
        }

        public void Init()
        {
            iluiPersonList = DBDAL.Instance.GetPersonList(ILUI);
            iluiPersonList.Sort();
            refuaPersonList = DBDAL.Instance.GetPersonList(REFUA);
            refuaPersonList.Sort();
        }

    }
}