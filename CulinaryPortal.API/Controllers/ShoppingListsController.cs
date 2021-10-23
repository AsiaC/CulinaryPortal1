using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CulinaryPortal.API.Entities;
using CulinaryPortal.API.Models;
using CulinaryPortal.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CulinaryPortal.API.Controllers
{
    [Route("api/shoppingLists")]
    [Authorize]
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
            try
            {
                var shoppingListsFromRepo = await _culinaryPortalRepository.GetShoppingListsAsync();
                return Ok(_mapper.Map<IEnumerable<ShoppingListDto>>(shoppingListsFromRepo));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }                    
        }        

        // GET: api/shoppingLists/5
        [HttpGet("{shoppingListId}", Name = "GetShoppingList")]
        public async Task<ActionResult<ShoppingListDto>> GetShoppingList([FromRoute] int shoppingListId)
        {
            try
            {
                var ingredinetFromRepo = await _culinaryPortalRepository.GetShoppingListAsync(shoppingListId);
                if (ingredinetFromRepo == null)
                {
                    return NotFound();
                }                
                return Ok(_mapper.Map<ShoppingListDto>(ingredinetFromRepo));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }            
        }

        //POST: api/shoppinglists
        [HttpPost]
        public async Task<ActionResult<ShoppingList>> CreateShoppingList([FromBody] ShoppingListDto shoppingListDto)
        {
            try
            {
                var shoppingList = _mapper.Map<ShoppingList>(shoppingListDto);
                await _culinaryPortalRepository.AddShoppingListAsync(shoppingList);
                
                return CreatedAtAction(nameof(GetShoppingList), new { shoppingListId = shoppingList.Id }, shoppingList);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }            
        }              

        // PUT: api/shoppingLists/5
        [HttpPut("{shoppingListId}")]
        public async Task<ActionResult> UpdateShoppingList([FromRoute] int shoppingListId, [FromBody] ShoppingListDto shoppingListDto)
        {            
            if (shoppingListId != shoppingListDto.Id)
            {
                return BadRequest();
            }
            try
            {
                var existingShoppingList = await _culinaryPortalRepository.GetShoppingListAsync(shoppingListId);
                if (existingShoppingList == null)
                {
                    return NotFound();
                }                
                if(existingShoppingList.Name != shoppingListDto.Name )
                    existingShoppingList.Name = shoppingListDto.Name;                               

                //Items
                List<ListItem> copyExItems = new List<ListItem>();

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
                
                var newItems = shoppingListDto.Items.Where(x => x.Id == null);
                foreach (var newItem in newItems)
                {
                    var newToAdd = _mapper.Map<ListItem>(newItem);
                    existingShoppingList.Items.Add(newToAdd);
                }                
                await _culinaryPortalRepository.SaveChangesAsync();
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        // DELETE: api/shoppingLists/1
        [HttpDelete("{shoppingListId}")]
        public async Task<ActionResult> DeleteShoppingList([FromRoute] int shoppingListId)
        {
            try
            {
                var shoppingListFromRepo = await _culinaryPortalRepository.GetShoppingListAsync(shoppingListId);            
                if (shoppingListFromRepo == null)
                {
                    return NotFound();
                }
                await _culinaryPortalRepository.DeleteShoppingListAsync(shoppingListFromRepo);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }
    }
}
