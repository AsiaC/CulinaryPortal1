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
        {
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

                existingShoppingList.Name = shoppingListDto.Name;                               

                //Instructions
                List<ListItem> copyExItems = new List<ListItem>();
                copyExItems.AddRange(existingShoppingList.Items);
                foreach (var exItem in copyExItems)
                {
                    var checkIfItemExist = shoppingListDto.Items.Any(i => i.Id == exItem.Id);
                    if (checkIfItemExist)
                    {
                        var instructionDto = shoppingListDto.Items.FirstOrDefault(i => i.Id == exItem.Id);

                        exItem.Name = instructionDto.Name;
                    }
                    else
                    {
                        existingShoppingList.Items.Remove(exItem); 
                    }
                }
                var existingItemIds = existingShoppingList.Items.Select(i => i.Id);
                var newItems = shoppingListDto.Items.Where(i => !existingItemIds.Contains(i.Id));
                foreach (var newItem in newItems)
                {//todo moze mozna uzywać mapera?
                    var newToAdd = new ListItem()
                    {
                        Name = newItem.Name
                    };
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


    }
}
