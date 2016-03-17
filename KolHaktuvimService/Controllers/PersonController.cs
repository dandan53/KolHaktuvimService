using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace KolHaktuvimService.Controllers
{
    public class PersonController : ApiController
    {
        public HttpResponseMessage Get(string type)
        {
            var resp = Request.CreateResponse<List<string>>(
          HttpStatusCode.OK, DAL.Instance.GetPersonList(type));

            return resp;
        }

        public HttpResponseMessage Post(string person, string type)
        {
            var retVal = DAL.Instance.AddPerson(person, type);
            return new HttpResponseMessage()
            {
                Content = new StringContent(retVal.ToString())
            };
        }
        
        // GET api/person
        //public List<string> Get()
        //{
        //   return DAL.Instance.GetPersonList();
        //}

        // GET api/values/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/values
        //public bool Post(string person)
        //{
        //    return DAL.Instance.AddPerson(person);
        //}

        // PUT api/values/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        // DELETE api/values/5
        //public void Delete([FromBody]string person)
        //{
        //    DAL.Instance.RemovePerson(person);
        //}
    }
}