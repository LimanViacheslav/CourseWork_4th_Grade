﻿using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinShop.DL.Entities.Identity
{
    public class User : IdentityUser
    {
        public string Name { get; set; }

        public string Adres { get; set; }

        public bool IsDeleted { get; set; }
    }
}
