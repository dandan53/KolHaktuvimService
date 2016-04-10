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
    public class BackOfficeController : ApiController
    {
        public HttpResponseMessage Get(string user)
        {
            var retVal = false;

            if (user != null && user.Equals("dandan53"))
            {
                retVal = DAL.Instance.RemoveAll();
                
            }
            return new HttpResponseMessage()
            {
                Content = new StringContent(retVal.ToString())
            };
            
        }
    }
}