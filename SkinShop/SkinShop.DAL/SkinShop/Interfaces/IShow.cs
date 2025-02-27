﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinShop.DAL.SkinShop.Interfaces
{
    public interface IShow<T> where T : class
    {
        ICollection<T> Show();

        T Get(int id);
    }
}
