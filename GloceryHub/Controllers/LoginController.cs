using GloceryHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GloceryHub.Controllers
{

    public class LoginController : ApiController
    {
        [Authorize]
        [HttpPost]
        [Route("Login")]
        public HttpResponseMessage Login(LoginDetailsModel opLoginDetailsModel)
        {
            beClass.ImpLogin opLogin = new beClass.ImpLogin();
            opLogin.beLogin(opLoginDetailsModel);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
