using CulinaryPortal.Application.Persistence;
using CulinaryPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CulinaryPortal.Persistence.Repositories
{
    public class ShoppingListRepository : BaseRepository<ShoppingList>, IShoppingListRepository
    {
        public ShoppingListRepository(CulinaryPortalDbContext dbContext) : base(dbContext)
        {

        }
        public async Task<List<ShoppingList>> GetShoppingListsWithDetailsAsync(int? userId)
        {
            IEnumerable<ShoppingList> shoppingLists = await _dbContext.ShoppingLists
                .Include(l => l.Items)
                .Include(u => u.User)
                .ToListAsync();

            if (userId != null)
            {
                shoppingLists = shoppingLists.Where(r => r.UserId == userId);
            }

            return shoppingLists.ToList();           
        }

        public async Task<ShoppingList> GetShoppingListWithDetailsAsync(int shoppingListId)
        {
            var shoppingList = await _dbContext.ShoppingLists
                .Include(l => l.Items)
                .FirstOrDefaultAsync(u => u.Id == shoppingListId);
            return shoppingList;
        }

    }
}
