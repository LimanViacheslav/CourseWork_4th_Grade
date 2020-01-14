using SkinShop.BLL.Identity.IdentityDTO;
using SkinShop.BLL.SkinShop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SkinShop.Filters
{
    public class BanUserAttribute: AuthorizeAttribute
    {
        Service _service = new Service();

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var user = httpContext.User;
            UserDTO userDTO = _service.StoreService.GetUserByName(user.Identity.Name);
            if (userDTO != null && userDTO.IsBanned)
            {
                return httpContext.Request.IsAuthenticated;
            }
            else
            {
                return true;
            }
        }
    }
}