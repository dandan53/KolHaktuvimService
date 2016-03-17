using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KolHaktuvimService.Models;

namespace KolHaktuvimService.Controllers
{
    public class DBDAL
    {
        private KolHaktuvimServiceContext context = new KolHaktuvimServiceContext();

        private static DBDAL instance = null;

        private DBDAL()
        {
        }

        public static DBDAL Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DBDAL();

                    //Init();
                }
                return instance;
            }
        }

        public List<string> GetPersonList(string type)
        {
            return (from member in context.People where member.Type.Equals(type) select member.Name).ToList();
        }

        public bool AddPerson(string person, string type)
        {
            if (IsPersonExist(person, type) == false)
            {
                var newMember = new Person() {Name = person};
                context.People.Add(newMember);
                context.SaveChanges();
            }

            return true;
        }

        public void RemovePerson(string person, string type)
        {
            if (IsPersonExist(person, type))
            {
                Person member = context.People.Single(x => x.Name.Equals(person) && x.Type.Equals(type));
                context.People.Remove(member);
                context.SaveChanges();
            }
        }

        public bool IsPersonExist(string person, string type)
        {
            var members = context.People.ToList();
            var retVal = members.Exists(x => x.Name.Equals(person) && x.Type.Equals(type));
            return retVal;
        }
        

        //private int CreateMemberId()
        //{
        //    var maxMemberId = context.Members.ToList().Max(x => x.MemberId);

        //    return ++maxMemberId;
        //}

    }
}