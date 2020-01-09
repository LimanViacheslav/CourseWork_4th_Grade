using SkinShop.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkinShop.Models.ViewModels
{
    public class UsersVM
    {
        public List<UserDM> Users { get; set; }

        public List<ClientProfileDM> Clients { get; set; }
    }
}