using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace JobFinderAPI.Controllers
{
    public class BaseApiController : ApiController
    {
        //private ApplicationUser _member;

        //public Microsoft.AspNet.Identity.UserManager UserManager
        //{
        //    get { return HttpContext.Current.GetOwinContext().GetUserManager<Microsoft.AspNet.Identity.UserManager>(); }
        //}

        //public string UserIdentityId
        //{
        //    get
        //    {
        //        var user = UserManager.FindByName(User.Identity.Name);
        //        return user.Id;
        //    }
        //}

    }
}
