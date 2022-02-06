﻿using CulinaryPortal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CulinaryPortal.Application.Persistence
{
    public interface ICookbookRepository : IAsyncRepository<Cookbook>
    {
        
    }
}
