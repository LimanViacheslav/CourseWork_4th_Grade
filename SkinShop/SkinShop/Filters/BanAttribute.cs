using SkinShop.BLL.Identity.IdentityDTO;
using SkinShop.BLL.SkinShop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace SkinShop.Filters
{
    public class BanAttribute: FilterAttribute, IAuthenticationFilter
    {
        Service _service = new Service();
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            var user = filterContext.HttpContext.User;
            UserDTO userDTO = _service.StoreService.GetUserByName(user.Identity.Name);
            if (userDTO != null && userDTO.IsBanned)
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            var user = filterContext.HttpContext.User;
            UserDTO userDTO = _service.StoreService.GetUserByName(user.Identity.Name);
            if (userDTO != null && userDTO.IsBanned)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new System.Web.Routing.RouteValueDictionary {
                    { "controller", "Account" }, { "action", "Logout" }
                   });
            }
        }
    }
}