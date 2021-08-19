using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CulinaryPortal.API.Entities;
using CulinaryPortal.API.Models;
using CulinaryPortal.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CulinaryPortal.API.Controllers
{
    [Route("api/shoppingLists")]
    [ApiController]
    public class ShoppingListsController : ControllerBase
    {
        private readonly ICulinaryPortalRepository _culinaryPortalRepository;
        private readonly IMapper _mapper;

        public ShoppingListsController(ICulinaryPortalRepository culinaryPortalRepository, IMapper mapper)
        {
            _culinaryPortalRepository = culinaryPortalRepository ?? throw new ArgumentNullException(nameof(culinaryPortalRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        // GET: api/shoppingLists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShoppingListDto>>> GetShoppingLists()
        {
            var shoppingListsFromRepo = await _culinaryPortalRepository.GetShoppingListsAsync();
            return Ok(_mapper.Map<IEnumerable<ShoppingListDto>>(shoppingListsFromRepo));            
        }        

        // GET: api/shoppingLists/5
        [HttpGet("{shoppingListId}", Name = "GetShoppingList")]
        public async Task<ActionResult<ShoppingListDto>> GetShoppingList(int shoppingListId)
        {
            var checkIfShoppingListExists = await _culinaryPortalRepository.ShoppingListExistsAsync(shoppingListId);

            if (checkIfShoppingListExists == false)
            {
                return NotFound();
            }
            var ingredinetFromRepo = await _culinaryPortalRepository.GetShoppingListAsync(shoppingListId);
            var ingredient = _mapper.Map<ShoppingListDto>(ingredinetFromRepo);
            return Ok(ingredient);
        }

        [HttpPost]
        public async Task<ActionResult<ShoppingList>> CreateShoppingList([FromBody] ShoppingListDto shoppingListDto)
        {//TODO try catch
            var shoppingList = _mapper.Map<ShoppingList>(shoppingListDto);
            await _culinaryPortalRepository.AddShoppingListAsync(shoppingList); 
            await _culinaryPortalRepository.SaveChangesAsync(); 
            //TODO spr WYNIK i zwróć błąd jesli nie udało sie utworzyc
            return CreatedAtAction("GetShoppingList", new { shoppingListId = shoppingList.Id }, shoppingList);
        }              

        //// DELETE: api/shoppingLists/5
        //[HttpDelete("{shoppingListId}")]
        //public ActionResult DeleteShoppingList(int shoppingListId)
        //{
        //    var shoppingListFromRepo = _culinaryPortalRepository.GetShoppingList(shoppingListId);
        //    if (shoppingListFromRepo == null)
        //    {
        //        return NotFound();
        //    }

        //    _culinaryPortalRepository.DeleteShoppingList(shoppingListFromRepo);
        //    _culinaryPortalRepository.Save();

        //    return NoContent();
        //}

        // PUT: api/shoppingLists/5
        [HttpPut("{shoppingListId}")]
        public async Task<IActionResult> UpdateShoppingList([FromRoute] int shoppingListId, [FromBody] ShoppingListDto shoppingListDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (shoppingListId != shoppingListDto.Id)
            {
                return BadRequest();
            }
            try
            {
                var checkIfShoppingListExists = await _culinaryPortalRepository.ShoppingListExistsAsync(shoppingListId);
                if (checkIfShoppingListExists == false)
                {
                    return NotFound();
                }
                var existingShoppingList = await _culinaryPortalRepository.GetShoppingListAsync(shoppingListId);
                if(existingShoppingList.Name != shoppingListDto.Name )
                    existingShoppingList.Name = shoppingListDto.Name;                               

                //Items
                List<ListItem> copyExItems = new List<ListItem>();

                //var allSentItems = shoppingListDto.Items.ToList();
                //var allSentItemIds = allSentItems.Select(x=>x.Id);
                //var allSentItemIdsWithoutNulls = allSentItems.Where(y=>y.Id != null).Select(x => (int)x.Id);

                //var previouItems = existingShoppingList.Items.ToList();
                //var previousItemIds = previouItems.Select(x => x.Id);

                //var newAddedItems = allSentItems.Where(x => x.Id == null);

                //var itemsToRemove = previousItemIds.Except(allSentItemIdsWithoutNulls);
                //var possibleChangedItems = 

                copyExItems.AddRange(existingShoppingList.Items);
                foreach (var exItem in copyExItems)
                {
                    var checkIfItemExist = shoppingListDto.Items.Any(i => i.Id == exItem.Id);
                    if (checkIfItemExist)
                    {
                        var itemDto = shoppingListDto.Items.FirstOrDefault(i => i.Id == exItem.Id);
                        if (exItem.Name != itemDto.Name) 
                            exItem.Name = itemDto.Name;                        
                    }
                    else
                    {
                        existingShoppingList.Items.Remove(exItem); 
                    }
                }
                //var existingItemIds = existingShoppingList.Items.Select(i => i.Id);
                //var newItems0 = shoppingListDto.Items.Where(i => !existingItemIds.Contains((int)i.Id));
                //var newItems = shoppingListDto.Items.Where(i => !existingItemIds.Contains((int)i.Id)).ToList();

                //var newItems = shoppingListDto.Items.Except(copyExItems);
                var newItems = shoppingListDto.Items.Where(x => x.Id == null);
                foreach (var newItem in newItems)
                {//todo moze mozna uzywać mapera?
                    //var newToAdd = new ListItem()
                    //{
                    //    Name = newItem.Name
                    //};
                    var newToAdd = _mapper.Map<ListItem>(newItem);
                    existingShoppingList.Items.Add(newToAdd);
                }                
                await _culinaryPortalRepository.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw;
            }
            return NoContent();
        }

        //// PUT: api/recipes/5 -> jak zmienic link do wywołania, czy to w tym kontrolerze czy w kontrolerze listy zakupowej
        //[Route("api/shoppingLists/addRecipeIngredients")]
        [HttpPut("{shoppingListId}/addrecipeingredients")]
        public async Task<IActionResult> AddRecipeIngredients([FromRoute] int shoppingListId, [FromBody] ShoppingListDto shoppingListDto)
        {
            var existingShoppingList = await _culinaryPortalRepository.GetShoppingListAsync(shoppingListId);
            foreach (var newItem in shoppingListDto.Items)
            {//todo moze mozna uzywać mapera?
                var newToAdd = new ListItem()
                {
                    Name = newItem.Name
                };
                existingShoppingList.Items.Add(newToAdd);
            }
            //await _culinaryPortalRepository.SaveChangesAsync();

            //foreach (var recipeIngredient in shoppingListDto.Items)
            //{
            //    var newListItem = new ListItem()
            //    {
            //        Name = recipeIngredient.Name,
            //        ShoppingListId = (int)shoppingListDto.Id,
            //    };
            // _culinaryPortalRepository.AddListItemsAsync(newListItem);

            //}
            //await _culinaryPortalRepository.SaveChangesAsync();


            //TODO spr WYNIK i zwróć błąd jesli nie udało sie utworzyc

            //    //one user only one cookbook   
            //    var user = await _culinaryPortalRepository.GetUserAsync(cookbookRecipeDto.UserId);
            //    var cookbook = await _culinaryPortalRepository.GetCookbookAsync(user.Cookbook.Id);

            //    var recipeToAdd = new CookbookRecipe()
            //    {
            //        CookbookId = cookbook.Id,
            //        RecipeId = cookbookRecipeDto.RecipeId
            //    };
            //    //var cookbook0 = await _culinaryPortalRepository.GetCookbookAsync(cookbookId);
            //    cookbook.CookbookRecipes.Add(recipeToAdd);
            //    await _culinaryPortalRepository.SaveChangesAsync();
            return Ok();
        }

        // DELETE: api/shoppingLists
        [HttpDelete()]
        public async Task<ActionResult> DeleteShoppingList([FromBody] int shoppingListId)
        {
            try
            {
                var shoppingListFromRepo = await _culinaryPortalRepository.GetShoppingListAsync(shoppingListId);            
                if (shoppingListFromRepo == null)
                {
                    return NotFound();
                }

                _culinaryPortalRepository.DeleteShoppingList(shoppingListFromRepo);
                await _culinaryPortalRepository.SaveChangesAsync();

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }
    }
}
