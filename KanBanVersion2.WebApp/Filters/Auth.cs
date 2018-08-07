using KanBanVersion2.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KanBanVersion2.WebApp.Filters
{
    public class Auth : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
         if(CurrentSession.kullanici == null)
            {
                filterContext.Result = new RedirectResult("/Home/Login");
            }
        }
    }
}