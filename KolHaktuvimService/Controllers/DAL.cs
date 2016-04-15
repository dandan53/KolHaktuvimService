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

        private List<string> personList = new List<string>();

        private static DAL instance = null;

        public enum MessageCode
        {
            Error,
            PersonAdded,
            PersonRemoved,
            PersonExists,
            PersonDoesntExist
        };

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

        public List<string> GetPersonList(string type, int start, int pageSize)
        {
            List<string> retVal = null;

            if (type.Equals(REFUA))
            {
                if (start + pageSize < refuaPersonList.Count())
                {
                    retVal = refuaPersonList.GetRange(start, pageSize);                    
                }
                else if (start < refuaPersonList.Count())
                {
                    retVal = refuaPersonList.GetRange(start, refuaPersonList.Count() - start);                    
                }
            }
            else if (type.Equals(ILUI))
            {
                if (start + pageSize < iluiPersonList.Count())
                {
                    retVal = iluiPersonList.GetRange(start, pageSize);
                }
                else if (start < iluiPersonList.Count())
                {
                    retVal = iluiPersonList.GetRange(start, iluiPersonList.Count() - start);
                }
            }

            return retVal;
        }

        public MessageCode AddPerson(string person, string type)
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

                personList = iluiPersonList.Concat(refuaPersonList).ToList();
                personList.Sort();

                DBDAL.Instance.AddPerson(person, type);

                return MessageCode.PersonAdded;
            }
            else
            {
                return MessageCode.PersonExists;
            }
        }

        public MessageCode RemovePerson(string person, string type)
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

                personList = iluiPersonList.Concat(refuaPersonList).ToList();
                personList.Sort();

                DBDAL.Instance.RemovePerson(person, type);

                return MessageCode.PersonRemoved;
            }
            else
            {
                return MessageCode.PersonDoesntExist;
            }
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


        public List<string> Search(string searchText, int start, int pageSize)
        {
            List<string> retVal = null;

            retVal = personList.FindAll(s => s.Contains(searchText));

            if (start + pageSize < retVal.Count())
            {
                retVal = retVal.GetRange(start, pageSize);
            }
            else if (start < retVal.Count())
            {
                retVal = retVal.GetRange(start, retVal.Count() - start);
            }
            
            return retVal;
        }

        public bool RemoveAll()
        {
            refuaPersonList = new List<string>();
            iluiPersonList = new List<string>();
            personList = new List<string>();
            
            DBDAL.Instance.RemoveAll();
        
            return true;
        }


        public void Init()
        {
            iluiPersonList = DBDAL.Instance.GetPersonList(ILUI);
            iluiPersonList.Sort();
            refuaPersonList = DBDAL.Instance.GetPersonList(REFUA);
            refuaPersonList.Sort();

            personList = iluiPersonList.Concat(refuaPersonList).ToList();
            personList.Sort();
        }

    }
}