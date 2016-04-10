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
        public HttpResponseMessage Get(string type, int start, int pageSize, string searchText)
        {
            List<string> list = (searchText != null && type == null) ? 
                DAL.Instance.Search(searchText, start, pageSize) : 
                DAL.Instance.GetPersonList(type, start, pageSize);
            
            var resp = Request.CreateResponse<List<string>>(HttpStatusCode.OK, list);

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

        // search
        //public HttpResponseMessage Search(string searchText, string type, int start, int pageSize)
        //{
        //    var resp = Request.CreateResponse<List<string>>(
        //  HttpStatusCode.OK, DAL.Instance.Search(searchText, type));

        //    return resp;
        //}

        // search
        /*public HttpResponseMessage Get(string searchText, string type)
        {
            var resp = Request.CreateResponse<List<string>>(
          HttpStatusCode.OK, DAL.Instance.Search(searchText, type));

            return resp;
        }*/

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

        public HttpResponseMessage Delete(string person, string type)
        {
            var retVal = DAL.Instance.RemovePerson(person, type);
            return new HttpResponseMessage()
            {
                Content = new StringContent(retVal.ToString())
            };
        }

    }
}