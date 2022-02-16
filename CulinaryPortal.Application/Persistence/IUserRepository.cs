﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CulinaryPortal.Domain.Entities;

namespace CulinaryPortal.Application.Persistence
{
    public interface IUserRepository : IAsyncRepository<User>
    {
        ////Task<IEnumerable<ShoppingList>> GetUserShoppingListsAsync(int userId);
        Task<List<User>> GetUsersWithDetailsAsync();
    }
}